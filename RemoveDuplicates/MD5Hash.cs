using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace RemoveDuplicates {

    /// <summary>
    /// A class with MD5 functions
    /// </summary>
    class MD5Hash {

        /// <summary>
        /// This function will hash a string and then return any string you input
        /// </summary>
        /// <param name="stringToHash">A string you want to hash</param>
        /// <returns>The hashed input string</returns>
        public string HashString(string stringToHash) {

            using (MD5 hasher = MD5.Create()) {

                byte[] hashedBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++) {
                    stringBuilder.Append(hashedBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public string HashFile(string fileName) {
            string fileContents = File.ReadAllText(fileName, Encoding.UTF8);

            return HashString(fileContents);
        }

        /// <summary>
        /// This function will compare a string and a hash with eachother, and return the result
        /// </summary>
        /// <param name="stringToCompare">The string to compare</param>
        /// <param name="hashToCompare">The hash to compare to</param>
        /// <returns>If the string compares or not</returns>
        public bool Compare(string stringToCompare, string hashToCompare) {

            stringToCompare = this.HashString(stringToCompare);

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            return Convert.ToBoolean(stringComparer.Compare(stringToCompare, hashToCompare));
        }
    }
}
