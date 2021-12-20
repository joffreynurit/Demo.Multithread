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
            //Create an entry to wee than the process is correct
            await ProcessWithTimer(1);

            //Not awaited process, but we sleep the main process enough to let this async call write in the file
            _ = ProcessWithTimer(2);

            await Task.Delay(1200);

            //Not awaited process to see the bug
            _ = ProcessWithTimer(3);
        }
        

        public static async Task ProcessWithTimer(int processNumber)
        {
            Console.WriteLine("Before sleep : " + processNumber + " - " + DateTime.Now.ToString("mm:ss"));

            await Task.Delay(1000);

            Console.WriteLine("After sleep : " + processNumber + " - " + DateTime.Now.ToString("mm:ss"));
        }
    }
}
