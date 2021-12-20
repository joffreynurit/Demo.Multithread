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
    /// </summary>
    public static class FileExamples
    {

        static public void FileDemo()
        {
            var path = "d:/WriteLinesNotAwaited.txt";
            WriteInFile(path, "First line");
            _ = Task.Run(() => { WriteInFile(path, "Second line"); });
            _ = WriteInFileAsync(path, "Third line");
        }

        static public async Task<bool> AwaitedFileDemoAsync()
        {
            var path = "d:/WriteLinesAwaited.txt";
            WriteInFile(path, "First line");
            await Task.Run(() => { WriteInFile(path, "Second line"); });
            await WriteInFileAsync(path, "Third line");

            return true;
        }

        /// <summary>
        /// Basic method to write in a file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="txt"></param>
        static private void WriteInFile(string path, string txt)
        {
            //We need to wait the process to be synchrone. We can't use "await" is the method isn't "async"
            WriteInFileAsync(path, txt).Wait();
        }

        /// <summary>
        /// Async method to write in a file on the filesystem
        /// </summary>
        /// <param name="path"></param>
        /// <param name="txt"></param>
        static public async Task WriteInFileAsync(string path, string txt)
        {
            //Simulation of a long process
            Thread.Sleep(1000);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(path, append: true))
            {
                await outputFile.WriteLineAsync(txt);
            }
        }
    }
}
