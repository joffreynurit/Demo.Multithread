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
            await SimpleExamples.ProcessWithTimer(1, useAsyncTimer);

            //Not awaited process, but the total sleep in this main process enough to let this async call write in the file
            _ = SimpleExamples.ProcessWithTimer(2, useAsyncTimer);

            await Task.Delay(500);

            //Not awaited process to see the bug
            _ = SimpleExamples.ProcessWithTimer(3, useAsyncTimer);

            //Need to be less than the total time of ProcessWithTimer, to see the process3 cut when running asynchrously,
            //but the total of 2 sleep need to be more enough to finish the process 2
            await Task.Delay(600);
        }

        public static async Task FalseAsyncFunction()
        {
            Console.WriteLine("i'm a async function, but i work like a classic function");
        }
    }
}
