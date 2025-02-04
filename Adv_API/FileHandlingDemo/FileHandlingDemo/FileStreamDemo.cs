using System;
using System.IO;

namespace FileHandlingDemo
{
    /// <summary>
    /// Demonstrates the use of the FileStream class for file operations.
    /// </summary>
    public class FileStreamDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n### FileStream Class Demo ###\n");

            string filePath = "DemoFileStream.txt";

            // Ensure a clean start
            if (File.Exists(filePath))
                File.Delete(filePath);

            #region Write to File using FileStream
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes("This is a demo file using FileStream.");
                fs.Write(data, 0, data.Length);
            }
            Console.WriteLine("File 'DemoFileStream.txt' created and written using FileStream.");
            #endregion

            #region Read from File using FileStream
            Console.WriteLine("\n-- File Content --");
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
            }
            #endregion

            #region Delete File
            File.Delete(filePath);
            Console.WriteLine("\n-- File Deleted --");
            Console.WriteLine($"File '{filePath}' deleted.");
            #endregion
        }
    }
}
