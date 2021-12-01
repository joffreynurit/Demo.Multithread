using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            WatchProcessDuration(Process);
            WatchProcessDuration(ProcessThreaded);
            WatchProcessDuration(ProcessLoopThreaded);
            WatchProcessDuration(ProcessAllThreaded);
        }

        static private int Process(List<Star> stars)
        {
            stars.ForEach(star =>
            {
                star.LoopIds.ForEach(loopId =>
                {
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - begin");
                    Thread.Sleep(star.WaitTime);
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - end");
                });
            });

            return 1;
        }

        /// <summary>
        /// Simulation of a process who need to use multithread
        /// </summary>
        static private int ProcessThreaded(List<Star> stars)
        {
            _ = Parallel.ForEach(stars, star =>
              {
                  star.LoopIds.ForEach(loopId =>
                  {
                      //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - begin");
                      Thread.Sleep(star.WaitTime);
                      //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - end");
                  });
              });

            return 1;
        }

        static private int ProcessLoopThreaded(List<Star> stars)
        {
            stars.ForEach(star =>
            {
                _ = Parallel.ForEach(star.LoopIds, loopId =>
                {
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - begin");
                    Thread.Sleep(star.WaitTime);
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - end");
                });
            });

            return 1;
        }

        static private int ProcessAllThreaded(List<Star> stars)
        {
            _ = Parallel.ForEach(stars, star =>
            {
                _ = Parallel.ForEach(star.LoopIds, loopId =>
                {
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - begin");
                    Thread.Sleep(star.WaitTime);
                    //Console.WriteLine("Process " + star.Name + " thread #" + loopId + " - end");
                });
            });

            return 1;
        }

        static private void WatchProcessDuration(Func<List<Star>, int> callback)
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

    /// <summary>
    /// A simple class, for demo purpose
    /// </summary>
    public class Star
    {
        /// <summary>
        /// Name of the process
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// In ms
        /// </summary>
        public int WaitTime { get; set; }

        /// <summary>
        /// Number of loop for our multithread demo
        /// </summary>
        public int NumberOfLoop { get; set; }

        public List<int> LoopIds { 
            get
            {
                var loops = new List<int>();
                for (var i = 0; i < NumberOfLoop; i++)
                {
                    loops.Add(i);
                }

                return loops;
            } 
        }
    }
}
