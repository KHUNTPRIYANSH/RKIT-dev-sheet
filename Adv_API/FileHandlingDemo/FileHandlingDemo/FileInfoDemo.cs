using System;
using System.IO;

namespace FileHandlingDemo
{
    /// <summary>
    /// Demonstrates the use of the FileInfo class methods and properties.
    /// </summary>
    public class FileInfoDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n### FileInfo Class Demo ###\n");

            string filePath = "DemoFile.txt";

            // Ensure a clean start
            if (File.Exists(filePath))
                File.Delete(filePath);

            #region Create File
           
            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamWriter writer = fileInfo.CreateText())
            {
                writer.WriteLine("This is a demo file.");
            }
            
            Console.WriteLine("File 'DemoFile.txt' created.");
            #endregion

            #region Check File Existence
            if (fileInfo.Exists)
            {
                Console.WriteLine("File 'DemoFile.txt' exists.");
            }
            #endregion

            #region Get File Properties
            Console.WriteLine("\n-- File Properties --");
            Console.WriteLine($"Full Name: {fileInfo.FullName}");
            Console.WriteLine($"Name: {fileInfo.Name}");
            Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
            Console.WriteLine($"Extension: {fileInfo.Extension}");
            Console.WriteLine($"Creation Time: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Access Time: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Write Time: {fileInfo.LastWriteTime}");
            Console.WriteLine($"Size: {fileInfo.Length} bytes");
            #endregion

            #region Read File Content
            Console.WriteLine("\n-- File Content --");
            using (StreamReader reader = fileInfo.OpenText())
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            #endregion

            #region Rename File
            string newFilePath = "RenamedDemoFile.txt";
            fileInfo.MoveTo(newFilePath);
            Console.WriteLine("\n-- File Renamed --");
            Console.WriteLine($"File renamed to: {newFilePath}");
            fileInfo = new FileInfo(newFilePath);
            #endregion

            #region Delete File
            fileInfo.Delete();
            Console.WriteLine("\n-- File Deleted --");
            Console.WriteLine($"File '{newFilePath}' deleted.");
            #endregion
        }
    }
}
