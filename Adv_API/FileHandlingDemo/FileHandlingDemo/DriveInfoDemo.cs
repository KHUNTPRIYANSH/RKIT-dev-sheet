using System;
using System.IO;
namespace FileHandlingDemo

{
    public class DriveInfoDemo
    {
        public static void Run()
        {
            Console.WriteLine("### DriveInfo Class Demo ###\n");

            // Get all drives on the system
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                #region Display Drive Information
                Console.WriteLine($"Drive Name: {drive.Name}");
                Console.WriteLine($"Drive Type: {drive.DriveType}");
                Console.WriteLine($"Drive Format: {drive.DriveFormat}");
                Console.WriteLine($"Volume Label: {drive.VolumeLabel}");
                Console.WriteLine($"Root Directory: {drive.RootDirectory}");
                Console.WriteLine($"Total Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB");// default bytes
                Console.WriteLine($"Available Free Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                Console.WriteLine($"Total Free Space: {drive.TotalFreeSpace / (1024 * 1024 * 1024)} GB");
                Console.WriteLine($"Is Ready: {drive.IsReady}");
               
                #endregion

                #region Additional Drive Operations
                // Example of checking if the drive is ready and performing actions accordingly
                if (drive.IsReady)
                {
                    // Show root directory details
                    Console.WriteLine($"Root Directory Path: {drive.RootDirectory.FullName}");
                }
                else
                {
                    Console.WriteLine($"Drive {drive.Name} is not ready.");
                }
                #endregion
            }

            Console.WriteLine("End of DriveInfo Demo");
        }
    }
}
