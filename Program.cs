using System;
using System.IO;

namespace FolderFileMover
{
    class Program
    {
        /// <summary>
        /// Main entry point of the application. It moves files from subdirectories of a source folder
        /// to a destination folder while keeping a log of copied files.
        /// </summary>
        static void Main(string[] args)
        {
            // Get the source directory path from the user.
            Console.Write("Enter the path of the Source Directory: ");
            string? sourceDirectory = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(sourceDirectory) || !Directory.Exists(sourceDirectory))
            {
                Console.WriteLine("Invalid source directory! Please provide a valid path.");
                return;
            }

            // Get the destination directory path from the user.
            Console.Write("Enter the path of the Destination Directory: ");
            string? destinationDirectory = Console.ReadLine();

            // Define file copy limit and initialize copied file counter.
            int fileCopyLimit = 5;
            int copiedFileCount = 0;
            string logFilePath = Path.Combine(destinationDirectory, "FileCopyLog.txt");

            // Ensure the source directory exists.
            if (!Directory.Exists(sourceDirectory))
            {
                Console.WriteLine("Source directory does not exist.");
                return;
            }

            // Create the destination directory if it does not exist.
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            Console.WriteLine("Initializing Load Testing File Generator...");

            // Open the log file for writing.
            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                try
                {
                    // Get all subdirectories in the source directory.
                    string[] subdirectories = Directory.GetDirectories(sourceDirectory);
                    foreach (var subdirectory in subdirectories)
                    {
                        Console.WriteLine($"Checking folder: {subdirectory}");
                        string[] files = Directory.GetFiles(subdirectory);

                        foreach (var file in files)
                        {
                            // Stop the process if the file copy limit is reached.
                            if (copiedFileCount >= fileCopyLimit)
                            {
                                Console.WriteLine("File copy limit reached. Stopping process.");
                                return;
                            }

                            string fileName = Path.GetFileName(file);
                            string destinationPath = Path.Combine(destinationDirectory, fileName);

                            // Skip if the file already exists in the destination.
                            if (File.Exists(destinationPath))
                            {
                                Console.WriteLine($"File {fileName} already exists.");
                                continue;
                            }

                            int retryCount = 3;
                            bool success = false;

                            // Retry copying the file up to 3 times in case of failures.
                            while (retryCount > 0 && !success)
                            {
                                try
                                {
                                    File.Copy(file, destinationPath);
                                    copiedFileCount++;
                                    logFile.WriteLine($"{DateTime.Now}: File copied - {fileName}");
                                    Console.WriteLine($"File {fileName} copied ({copiedFileCount}/{fileCopyLimit})");
                                    success = true;
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine($"Copy failed for {fileName}, retrying... ({retryCount} attempts left)");
                                    logFile.WriteLine($"{DateTime.Now}: Error copying {fileName} - {ex.Message}");
                                    retryCount--;
                                }
                            }
                        }
                    }

                    Console.WriteLine("File extraction and movement completed.");
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors.
                    Console.WriteLine($"Error: {ex.Message}");
                    logFile.WriteLine($"{DateTime.Now}: General Error - {ex.Message}");
                }
            }
        }
    }
}
