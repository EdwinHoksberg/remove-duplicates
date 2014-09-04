using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace RemoveDuplicates {

    class DirectoryLooper {

        private List<string> allFiles = new List<string>();

        /// <summary>
        /// This function will get all files in a directory, recursively
        /// </summary>
        /// <param name="directory">The directory to list</param>
        /// <param name="extension">If provided, only files with the extension will be gathered</param>
        /// <returns>An array containing all files</returns>
        public string[] GetAllFiles(string directory, string extension = "") {
            if (extension != "") {
                allFiles.AddRange(Directory.GetFiles(directory, "*" + extension));
            } else {
                 allFiles.AddRange(Directory.GetFiles(directory));
            }

            foreach (string dir in Directory.GetDirectories(directory)) {
                this.GetAllFiles(dir);
            }

            return this.allFiles.ToArray();
        }
    }
}
