using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMultithreadDemo
{
    /// <summary>
    /// Class with 2 methods to easily compare sync and async 
    /// </summary>
    public static class SimpleExample
    {

        /// <summary>
        /// Execute ExecuteHeavyTask and ExecuteLighTask on a classic way
        /// </summary>
        static public void SimpleDemoSync()
        {
            Console.WriteLine("begin simple demo SYNC at " + DateTime.Now.TimeOfDay);
            ExecuteHeavyTask();
            ExecuteLightTask();
        }

        /// <summary>
        /// Execute ExecuteHeavyTask and ExecuteLighTask with an async process
        /// </summary>
        static public async void SimpleDemoAsync()
        {
            var tasks = new List<Task>();

            Console.WriteLine("begin simple demo ASYNC at " + DateTime.Now.TimeOfDay);

            /*
             * You can easily create a task and run it with Task.Run
             */
            var heavyTask = Task.Run(() => { ExecuteHeavyTask(); });
            tasks.Add(heavyTask);

            /*
             * I create a task, with function i want to execute in parameter
             * At this line, function was NOT executed
             * I save the result (task) in a variable to manage this later
             */
            var lightTask = new Task(ExecuteLightTask);

            //I set the Task in a list, to wait all task in one instruction
            tasks.Add(lightTask);

            /*
             * I execute the task. The task execute my function ExecuteHeavyTask. 
             * Without this line, my function was never executed.
             * I can use this to save all my futur process in a list, and fire all process in one shot
             */
            lightTask.Start();

            //I wait all task here. While all task are not ended, we are blocked here
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("All tasks ended");
        }

        /// <summary>
        /// Simulation for a heavy process un my app
        /// </summary>
        static private void ExecuteHeavyTask()
        {
            Console.WriteLine("Begin - Heavy task done at " + DateTime.Now.TimeOfDay);
            Thread.Sleep(10 * 1000);
            Console.WriteLine("End - Heavy task done at " + DateTime.Now.TimeOfDay);
        }

        /// <summary>
        /// Simulation of a process lighter than "ExecuteHeavyTask"
        /// </summary>
        static private void ExecuteLightTask()
        {
            Console.WriteLine("Begin - Light task done at " + DateTime.Now.TimeOfDay);
            Thread.Sleep(2 * 1000);
            Console.WriteLine("End - Light task done at " + DateTime.Now.TimeOfDay);
        }
    }
}
