using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMultithreadDemo
{
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

        public List<int> LoopIds
        {
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
