using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CSharp.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Stopwatch watch = Stopwatch.StartNew();
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                int nbCopy = 100;
                string fileName = "document.pdf";
                string inputDir = Path.Combine(currentDir, "workingdirectory");
                Directory.CreateDirectory(inputDir);
                List<string> files;

                files = FilesGenerator.CopyFile(fileName, nbCopy, inputDir);

                #region classic foreach

                Console.WriteLine("Starting classic foreach loop");

                watch.Restart();

                foreach (string filePath in files)
                {
                    File.Encrypt(filePath);
                }

                watch.Stop();

                Console.WriteLine("Encrypt foreach files : {0}s", watch.Elapsed.TotalSeconds);

                #endregion classic foreach

                files = FilesGenerator.CopyFile(fileName, nbCopy, inputDir);

                #region parallel loop

                Console.WriteLine("Starting parallel loop");
                watch.Restart();

                Parallel.ForEach(files, filePath =>
                {
                    File.Encrypt(filePath);
                });

                watch.Stop();
                Console.WriteLine("Encrypt Parallel files : {0}s", watch.Elapsed.TotalSeconds);

                #endregion parallel loop

                files = FilesGenerator.CopyFile(fileName, nbCopy, inputDir);

                #region tasks loop

                Console.WriteLine("Starting tasks loop");
                watch.Restart();

                List<Task> tasks = new List<Task>();
                foreach (string filePath in files)
                {
                    tasks.Add(Task.Run(() => File.Encrypt(filePath)));
                }

                Console.WriteLine("Extra work during encrypt ...");

                Task.WaitAll(tasks.ToArray());

                watch.Stop();
                Console.WriteLine("Encrypt async files : {0}s", watch.Elapsed.TotalSeconds);

                #endregion tasks loop
            }
            catch (System.Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ResetColor();
            }
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}