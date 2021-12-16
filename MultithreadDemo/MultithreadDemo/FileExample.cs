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
    public static class FileExample
    {
        static public void FileDemo()
        {
            var path = "d:/WriteLinesNotAwaited.txt";
            WriteInFile(path, "First line");
            var asyncTask = new Task(() => { WriteInFile(path, "Second line"); });
            asyncTask.Start();
        }

        static public void AwaitedFileDemo()
        {
            var path = "d:/WriteLinesAwaited.txt";
            WriteInFile(path, "First line");
            var asyncTask = new Task(() => { WriteInFile(path, "Second line"); });
            asyncTask.Start();

            asyncTask.Wait();
        }

        static private void WriteInFile(string path, string txt)
        {
            Thread.Sleep(1000);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(path, append: true))
            {
                outputFile.WriteLine(txt);
            }
        }
    }
}
