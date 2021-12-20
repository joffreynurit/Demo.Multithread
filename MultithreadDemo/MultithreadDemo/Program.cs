using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMultithreadDemo
{
    class Program
    {
        /// <summary>
        /// You can comment / uncomment call to function to see different exemples for sync / async works
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            //SimpleExemples.SimpleDemoSync();
            //SimpleExemples.SimpleDemoAsync();

            //FileExamples.FileDemo();
            //Si the function was awaited, the return type is a bool. If we don't wait, we cannot guss the result, so only a Task is return
            //Console.WriteLine(await FileExamples.AwaitedFileDemoAsync());

            //A simple demo with console log to watch exeptions on await or not await process
            //await ErrorsExamples.DemoCatchException();

            await ErrorsExamples.DemoAsyncProcessCutBeforeEnd();

            //PrepareAndWatchLoopProcessDuration(LoopExamples.Process);
            //PrepareAndWatchLoopProcessDuration(LoopExamples.ProcessThreaded);
            //PrepareAndWatchLoopProcessDuration(LoopExamples.ProcessLoopThreaded);
            //PrepareAndWatchLoopProcessDuration(LoopExamples.ProcessAllThreaded);

        }

        /// <summary>
        /// Global function to prepare data and watch execution time of the function
        /// </summary>
        /// <param name="callback"></param>
        static private void PrepareAndWatchLoopProcessDuration(Func<List<Star>, int> callback)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            var stars = new List<Star>()
            {
                new Star(){ Name = "Arcturus", NumberOfLoop = 12, WaitTime = 500},
                new Star(){ Name = "Betelgeuse", NumberOfLoop = 250, WaitTime = 10},
                new Star(){ Name = "Antares", NumberOfLoop = 24, WaitTime = 200},
                new Star(){ Name = "Rigel", NumberOfLoop = 200, WaitTime = 10}
            };

            _ = callback(stars);

            stopWatch.Stop();
            TimeSpan timespan = stopWatch.Elapsed;
            Console.WriteLine(callback.Method.Name + " => " + timespan.TotalSeconds.ToString("0.0###") + " sc");
        }
    }
}
