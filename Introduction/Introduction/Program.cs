using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";
            ShowLargeFilesWithoutLink(path);
            Console.WriteLine("\n*******\n");
            ShowLargeFilesWithLink(path);

            Console.ReadKey();
        }

        private static void ShowLargeFilesWithLink(string path)
        {
            //var files = from file in new DirectoryInfo(path).GetFiles()
            //                  orderby file.Length descending
            //                  select file;
            var files = new DirectoryInfo(path).GetFiles()
                            .OrderByDescending(f => f.Length)
                            .Take(5);
            //foreach (var file in files.Take(5))            
            foreach (var file in files)
            {
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
            }
        }

        private static void ShowLargeFilesWithoutLink(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            for (int i = 0; i < 5; i++)
            {
                var file = files[i];
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
            }
        }
    }
    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
