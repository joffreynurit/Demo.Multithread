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
        static void Main(string[] args)
        {
            //SimpleExemple.SimpleDemoSync();
            //SimpleExemple.SimpleDemoAsync();

            //FileExample.FileDemo();
            //FileExemple.AwaitedFileDemo();

            PrepareAndWatchLoopProcessDuration(LoopExample.Process);
            PrepareAndWatchLoopProcessDuration(LoopExample.ProcessThreaded);
            PrepareAndWatchLoopProcessDuration(LoopExample.ProcessLoopThreaded);
            PrepareAndWatchLoopProcessDuration(LoopExample.ProcessAllThreaded);
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
