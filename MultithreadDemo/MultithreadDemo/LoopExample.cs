using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMultithreadDemo
{
    /// <summary>
    /// Class with test on enumerable datas
    /// </summary>
    public static class LoopExample
    {
        static public int Process(List<Star> stars)
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
        static public int ProcessThreaded(List<Star> stars)
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

        static public int ProcessLoopThreaded(List<Star> stars)
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

        static public int ProcessAllThreaded(List<Star> stars)
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
    }
}
