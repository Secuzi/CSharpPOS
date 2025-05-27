using System.IO;
using System;
using System.Text;
using System.Collections.Generic;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles read/write operations in application files.
    /// </summary>
    public static class FileManager
    {
        private static FileStream fs;
        private static StreamReader sr;


        /// <summary>
        /// Converts string text file format into string[]
        /// </summary>
        /// <param name="textFiles">String format of text files to be read.</param>
        /// <returns>string[] format of text files</returns>
        public static IEnumerable<string[]> IterateTextFiles(IEnumerable<string> textFiles)
        {
            foreach (var txtFile in textFiles)
            {
                sr = new StreamReader(txtFile);
                string lines = sr.ReadToEnd();

                var line = lines.Split(',');

                yield return line;

                sr.Close();
                sr.Dispose();

            }


        }
        /// <summary>
        /// Handles modifying of text file name
        /// </summary>
        /// <param name="path">Path of filename.</param>
        /// <param name="filename">The current name of file to modify.</param>
        /// <param name="newName">The new file name</param>
        public static void ChangingTxtFileName(string path, string fileName, string newName)
        {

            // Specify the current and new file names
            string newFileName = newName;

            string currentFilePathMain = GetFilePath.TextFilePath(InventorySystem.stockFolderPath, fileName);

            // Specify the path to the directory containing the new file

            string newFilePathMain = GetFilePath.TextFilePath(InventorySystem.stockFolderPath, newFileName);

            // Same algorithm but for cart
            string currentFilePathCart = GetFilePath.TextFilePath(MyCart.myCartFolderPath, fileName);

            string newFilePathCart = GetFilePath.TextFilePath(MyCart.myCartFolderPath, newFileName);

            try
            {
                // Check if the file exists before renaming
                if (File.Exists(currentFilePathMain))
                {
                    // Rename the file
                    File.Move(currentFilePathMain, newFilePathMain);

                    Console.WriteLine("File renamed successfully.");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {ex.Message}");
            }

            try
            {
                // Check if the file exists before renaming
                if (File.Exists(currentFilePathCart))
                {
                    // Rename the file
                    File.Move(currentFilePathCart, newFilePathCart);

                    Console.WriteLine("File renamed successfully.");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming file: {ex.Message}");
            }



        }
        
        /// <summary>
        /// Writes text file into file path.
        /// </summary>
        /// <param name="path">The file path.</param>
        public static void WriteTextFile(string path ,string pattern)
        {
        

            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                fs = File.Open(path, FileMode.Append);
                
                byte[] info = new UTF8Encoding(true).GetBytes(pattern);
                
                fs.Write(info, 0, info.Length);
                fs.Close();
                fs.Dispose();
            }
           
        }
        
        /// <summary>
        /// Creates text file in file path.
        /// </summary>
        /// <param name="path">The file path.</param>
        public static void CreateTextFile(string path)
        {
           
            if (!File.Exists(path))
            {
                fs = File.Create(path);
                fs.Close();
                fs.Dispose();

            }

        }
    }
}
