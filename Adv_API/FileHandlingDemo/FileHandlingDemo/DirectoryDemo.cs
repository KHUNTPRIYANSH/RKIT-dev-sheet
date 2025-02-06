using System;
using System.IO;

namespace FileHandlingDemo
{
    /// <summary>
    /// Demonstrates the use of the Directory class methods.
    /// </summary>
    public class DirectoryDemo
    {
        public static void Run()
        {
            Console.WriteLine("### Directory Class Demo ###\n");

            // Ensure a clean start
            if (Directory.Exists("DemoDirectory"))
                Directory.Delete("DemoDirectory", true);

            #region CreateDirectory
            Console.WriteLine("-- CreateDirectory --");
            Directory.CreateDirectory("DemoDirectory");
            Console.WriteLine("Directory 'DemoDirectory' created.");
            #endregion

            #region Exists
            Console.WriteLine("\n-- Exists --");
            if (Directory.Exists("DemoDirectory"))
            {
                Console.WriteLine("Directory 'DemoDirectory' exists.");
            }
            #endregion

            #region GetCurrentDirectory
            Console.WriteLine("\n-- GetCurrentDirectory --");
            Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
            #endregion

            #region SetCurrentDirectory
            Console.WriteLine("\n-- SetCurrentDirectory --");
            
            string originalDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory("DemoDirectory");
            
            Console.WriteLine("Changed Current Directory: " + Directory.GetCurrentDirectory());
            
            Directory.SetCurrentDirectory(originalDir); // Reset to original
            Console.WriteLine("Restored Current Directory: " + Directory.GetCurrentDirectory());
            #endregion

            #region GetFiles
            Console.WriteLine("\n-- GetFiles --");
            
            string[] files = Directory.GetFiles("DemoDirectory");
            
            Console.WriteLine("Files in 'DemoDirectory':");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
            #endregion

            #region GetDirectories
            Console.WriteLine("\n-- GetDirectories --");
            
            string[] directories = Directory.GetDirectories("DemoDirectory");
            Console.WriteLine("Subdirectories in 'DemoDirectory':");
            foreach (string dir in directories)
            {
                Console.WriteLine(dir);
            }
            #endregion
           

            #region GetParent
            Console.WriteLine("\n-- GetParent --");
            DirectoryInfo parent = Directory.GetParent("DemoDirectory");
            Console.WriteLine("Parent Directory: " + parent?.FullName ?? "No parent found.");
            #endregion

            #region GetDirectoryRoot
            Console.WriteLine("\n-- GetDirectoryRoot --");
            string root = Directory.GetDirectoryRoot("DemoDirectory");
            Console.WriteLine("Root Directory: " + root);
            #endregion

            #region GetLastAccessTime
            Console.WriteLine("\n-- GetLastAccessTime --");
            DateTime lastAccessTime = Directory.GetLastAccessTime("DemoDirectory");
            Console.WriteLine("Last Access Time: " + lastAccessTime);
            #endregion

            #region GetLastWriteTime
            Console.WriteLine("\n-- GetLastWriteTime --");
            DateTime lastWriteTime = Directory.GetLastWriteTime("DemoDirectory");
            Console.WriteLine("Last Write Time: " + lastWriteTime);
            #endregion

            #region GetCreationTime
            Console.WriteLine("\n-- GetCreationTime --");
            DateTime creationTime = Directory.GetCreationTime("DemoDirectory");
            Console.WriteLine("Creation Time: " + creationTime);
            #endregion


            #region Move
            Console.WriteLine("\n-- Move --");
            Directory.Move("DemoDirectory", "MovedDemoDirectory");
            Console.WriteLine("Directory moved to 'MovedDemoDirectory'.");
            #endregion

            #region Delete
            Console.WriteLine("\n-- Delete --");
            Directory.Delete("MovedDemoDirectory");
            Console.WriteLine("Directory 'MovedDemoDirectory' deleted.");
            #endregion
        }
    }
}
