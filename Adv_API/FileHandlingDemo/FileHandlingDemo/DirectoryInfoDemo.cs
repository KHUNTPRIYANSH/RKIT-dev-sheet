using System;
using System.IO;

namespace FileHandlingDemo
{
    /// <summary>
    /// Demonstrates the use of the DirectoryInfo class methods and Properties.
    /// </summary>
    public class DirectoryInfoDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n### DirectoryInfo Class Demo ###\n");

            // Ensure a clean start
            if (Directory.Exists("DemoDirectoryInfo"))
                Directory.Delete("DemoDirectoryInfo", true);

            #region Create Directory
            DirectoryInfo dirInfo = new DirectoryInfo("DemoDirectoryInfo");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                Console.WriteLine("Directory 'DemoDirectoryInfo' created.");
            }
            #endregion

            #region Check Directory Existence
            if (dirInfo.Exists)
            {
                Console.WriteLine("Directory 'DemoDirectoryInfo' exists.");
            }
            #endregion

            #region Get Directory Properties
            Console.WriteLine("\n-- Directory Properties --");
            Console.WriteLine($"Full Name: {dirInfo.FullName}");
            Console.WriteLine($"Name: {dirInfo.Name}");
            Console.WriteLine($"Parent: {dirInfo.Parent}");
            Console.WriteLine($"Root: {dirInfo.Root}");
            Console.WriteLine($"Creation Time: {dirInfo.CreationTime}");
            Console.WriteLine($"Last Access Time: {dirInfo.LastAccessTime}");
            Console.WriteLine($"Last Write Time: {dirInfo.LastWriteTime}");
            #endregion

            #region Enumerate Files
            string testFile = Path.Combine(dirInfo.FullName, "test.txt");
            File.WriteAllText(testFile, "This is a test file.");
            Console.WriteLine("\n-- Files in Directory --");
            foreach (var file in dirInfo.GetFiles())
            {
                Console.WriteLine($"File: {file.Name}");
            }
            #endregion

            #region Enumerate Subdirectories
            dirInfo.CreateSubdirectory("SubDir1");
            dirInfo.CreateSubdirectory("SubDir2");
            Console.WriteLine("\n-- Subdirectories --");
            foreach (var subDir in dirInfo.GetDirectories())
            {
                Console.WriteLine($"Subdirectory: {subDir.Name}");
            }
            #endregion

            #region Rename Directory
            string newPath = "RenamedDemoDirectoryInfo";
            dirInfo.MoveTo(newPath);
            Console.WriteLine("\n-- Directory Renamed --");
            Console.WriteLine($"Directory renamed to: {newPath}");
            dirInfo = new DirectoryInfo(newPath);
            #endregion

            #region Delete Directory
            dirInfo.Delete(true);
            Console.WriteLine("\n-- Directory Deleted --");
            Console.WriteLine($"Directory '{newPath}' deleted.");
            #endregion
        }
    }
}
