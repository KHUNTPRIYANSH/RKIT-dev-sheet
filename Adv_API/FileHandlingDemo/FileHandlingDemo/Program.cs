using System;

namespace FileHandlingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Handling Demo - C#");

            // Run Directory Demo
            DirectoryDemo.Run();

            // Run DirectoryInfo Demo
            DirectoryInfoDemo.Run();

            // Run DriveInfo Demo
            DriveInfoDemo.Run();

            // Run FileInfo Demo
            FileInfoDemo.Run();

        }
    }
}
