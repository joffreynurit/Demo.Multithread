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
    public static class LoopExamples
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

        /// <summary>
        /// This process work, cause Parallel.Foreach use sync process
        /// </summary>
        static public void LoopWithSyncProcess()
        {
            var numberList = new List<int>();
            for(int i = 0; i < 100; i++) numberList.Add(i);

            _ = Parallel.ForEach(numberList, async number =>
            {
                await SimpleExamples.ProcessWithTimer(number, useAsyncTimer: false);
            });
        }

        /// <summary>
        /// This process doesn't work, cause Parallel.foreach can't manage async process
        /// </summary>
        static public void LoopWithAsyncProcess()
        {
            var numberList = new List<int>();
            for(int i = 0; i < 100; i++) numberList.Add(i);

            _ = Parallel.ForEach(numberList, async number =>
            {
                await SimpleExamples.ProcessWithTimer(number, useAsyncTimer: true);
            });
        }

        /// <summary>
        /// Loop who can manage async function
        /// </summary>
        static public async Task LoopCanManagesAsyncProcessAsync()
        {
            var numberList = new List<int>();
            for(int i = 0; i < 100; i++) numberList.Add(i);

            var taskList = new List<Task>();

            numberList.ForEach(number =>
            {
                taskList.Add(SimpleExamples.ProcessWithTimer(number, useAsyncTimer: true));
            });

            await Task.WhenAll(taskList);
        }


        static public async Task LoopLockTest()
        {
            var loopEndedTasks = new List<int>();

            var numberList = new List<int>();
            for(int i = 0; i < 100; i++) numberList.Add(i);

            var taskList = new List<Task>();

            numberList.ForEach(number =>
            {
                taskList.Add(Task.Run(async () =>
                {
                    Console.WriteLine("begin : " + number);
                    await Task.Delay(1000);
                    Console.WriteLine("after : " + number);

                    lock(loopEndedTasks)
                    {
                        loopEndedTasks.Add(number);
                    }
                }));
            });

            await Task.WhenAll(taskList);

            Console.WriteLine("Nb loop : " + loopEndedTasks.Count);
        }
    }
}
