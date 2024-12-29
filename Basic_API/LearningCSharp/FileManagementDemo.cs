using System;
using System.IO;

namespace LearningCSharp
{
    /// <summary>
    /// File Management demo file 
    /// </summary>
    internal class FileManagementDemo
    {
        /// <summary>
        /// Runs the file management demo.
        /// </summary>
        public static void RunFileManagementDemo()
        {
            // Get the current directory and store it into string
            string currentDirectoryName = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current directory name: {currentDirectoryName}");

            // Define the directory and file paths within the current directory
            string directoryPath = Path.Combine(currentDirectoryName, "dummyfolder");
            string filePath = Path.Combine(directoryPath, "dummy.txt");

            try
            {
                // Create a directory within the current directory
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory created successfully!\nName: {directoryPath}");
                }

                // Perform file operations on new file
                string content = "This is some dummy text to put into the newly created file called dummy.txt";
                File.WriteAllText(filePath, content);
                Console.WriteLine("\nContent written to dummy.txt file\n\n");

                string readContent = File.ReadAllText(filePath);
                Console.WriteLine("File content [dummy.txt]: " + readContent);

                File.AppendAllText(filePath, " ...... Appending more text.");
                Console.WriteLine("\nContent appended to file.");

                string appendedContent = File.ReadAllText(filePath);
                Console.WriteLine("\nAppended content: " + appendedContent);

                FileInfo fileInfo = new FileInfo(filePath);
                Console.WriteLine("\nFollowing are some file info\n");
                Console.WriteLine($"\t# Full name: {fileInfo.FullName}");
                Console.WriteLine($"\t# Name: {fileInfo.Name}");
                Console.WriteLine($"\t# Extension: {fileInfo.Extension}");
                Console.WriteLine($"\t# Created: {fileInfo.CreationTime}");
                Console.WriteLine($"\t# Last accessed: {fileInfo.LastAccessTime}");
                Console.WriteLine($"\t# Last modified: {fileInfo.LastWriteTime}");
                Console.WriteLine($"\t# Length: {fileInfo.Length} bytes");

                // Delete file / folder at last
                // if (File.Exists(filePath))
                // {
                //     File.Delete(filePath);
                //     Console.WriteLine($"dummy.txt file deleted: {filePath}");
                // }

                // if (Directory.Exists(directoryPath))
                // {
                //     Directory.Delete(directoryPath, recursive: true);
                //     Console.WriteLine($"Directory deleted: {directoryPath}");
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
