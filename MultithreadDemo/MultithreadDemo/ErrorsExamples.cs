using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMultithreadDemo
{
    public static class ErrorsExamples
    {
        /// <summary>
        /// A simple demo to see when an exception was raised or not on async process
        /// </summary>
        /// <returns></returns>
        public static async Task DemoCatchException()
        {
            try
            {
                _ = ProcessWithError();
                Console.WriteLine("No exception was raised for classic ProcessWithError");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was catch on classic process");
            }

            try
            {
                _ = await ProcessWithError();
                Console.WriteLine("No exception was raised for awaited ProcessWithError");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was catch on awaited process");
            }
        }

        /// <summary>
        /// Simulation of a process who throw an exception
        /// </summary>
        /// <returns>If the function work without problem (spoiler alert: nop)</returns>
        public static async Task<bool> ProcessWithError()
        {
            await FileExamples.WriteInFileAsync("d:/error.log", "before error");

            throw new ApplicationException("Throw an exception for demo purpose");

            await FileExamples.WriteInFileAsync("d:/error.log", "after error (not reached)");

            return true;
        }
        
        public static async Task DemoAsyncProcessCutBeforeEnd()
        {
            var useAsyncTimer = true;

            //Create an entry to wee than the process is correct
            await ProcessWithTimer(1, useAsyncTimer);

            //Not awaited process, but the total sleep in this main process enough to let this async call write in the file
            _ = ProcessWithTimer(2, useAsyncTimer);

            await Task.Delay(500);

            //Not awaited process to see the bug
            _ = ProcessWithTimer(3, useAsyncTimer);

            //Need to be less than the total time of ProcessWithTimer, to see the process3 cut when running asynchrously,
            //but the total of 2 sleep need to be more enough to finish the process 2
            await Task.Delay(600);
        }


        /// <summary>
        /// This function can be use as async when useAsyncTimer = true
        /// If useAsyncTimer = false, this function was always executed synchroniously, with or without await
        /// </summary>
        /// <param name="processNumber"></param>
        /// <param name="useAsyncTimer"></param>
        /// <returns></returns>
        public static async Task ProcessWithTimer(int processNumber, bool useAsyncTimer = true)
        {
            Console.WriteLine("Before sleep : " + processNumber + " - " + DateTime.Now.ToString("mm:ss"));

            if (useAsyncTimer)
                await Task.Delay(1000);
            else
                Thread.Sleep(1000);

            Console.WriteLine("After sleep : " + processNumber + " - " + DateTime.Now.ToString("mm:ss"));
        }

        public static async Task FalseAsyncFunction()
        {
            Console.WriteLine("i'm a async function, but i work like a classic function");
        }
    }
}
