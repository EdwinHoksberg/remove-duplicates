using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace RemoveDuplicates {

    class Program {

        static void Main(string[] args) {

            if (args.Length < 1 || args.Length > 2) {
                ShowHelp();
                Environment.Exit(1);
            }

            if (!Directory.Exists(args[0])) {
                ShowHelp();
                Environment.Exit(1);
            }

            try {
                string[] files = null;
                DirectoryLooper dir = new DirectoryLooper();
                if (args.Length > 1) {
                    files = dir.GetAllFiles(args[0], args[1]);

                    Console.WriteLine();
                    Console.WriteLine("Starting search in " + args[0] + " with extension of ." + args[1]);
                } else {
                    files = dir.GetAllFiles(args[0]);

                    Console.WriteLine();
                    Console.WriteLine("Starting search in " + args[0]);
                }

                MD5Hash md5 = new MD5Hash();

                List<string> fileHashes = new List<string>();
                List<string> duplicates = new List<string>();

                foreach (string file in files) {
                    string fileHash = md5.HashFile(file);

                    if (fileHashes.Contains(fileHash)) {

                        Console.WriteLine("Duplicate found: " + Path.GetFileName(file));
                        duplicates.Add(file);
                    } else {
                        fileHashes.Add(fileHash);
                    }
                }

                Console.WriteLine();
                if (duplicates.Count > 0) {

                    Console.WriteLine("Found " + duplicates.Count + " duplicate(s) in " + files.Length + " file(s)");
                    Console.WriteLine("Delete all the duplicates? Press y or n");
                    ConsoleKey keyPressed = Console.ReadKey(true).Key;

                    if (keyPressed == ConsoleKey.Y) {
                        foreach (string fileToDelete in duplicates.ToArray()) {
                            File.Delete(fileToDelete);
                        }
                        Console.WriteLine("Succesfully deleted " + duplicates.Count + " duplicate files");
                    } else {
                        Console.WriteLine("Duplicate(s) not removed, press any key to exit");
                    }
                } else {
                    Console.WriteLine("No duplicates found, press any key to exit");
                }

                Console.Read();

            } catch (Exception e) {
                Console.WriteLine("An error occured: " + e.StackTrace);
                Console.Read();
                Environment.Exit(1);
            }  
        }

        static void ShowHelp() {
            Console.WriteLine("How to use:");
            Console.WriteLine("RemoveDuplicates.exe <directory: C:\\Users\\> <optional extension: exe>");
        }
    }
}
