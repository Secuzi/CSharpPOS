using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FinalOutput
{
    /// <summary>
    /// Class that handles file path conversion.
    /// </summary>
    public class GetFilePath
    {

        /// <summary>
        /// Converts to string to complete file path.
        /// </summary>
        /// <returns>The string conversion of the file.</returns>
        public static string TextFilePath(string path, string fileName)
        {
            return Path.GetFullPath(path) + $"\\{fileName}.txt";
        }

        /// <summary>
        /// Returns the full path of the given argument
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <returns>The complete file path of a file</returns>
        public static string FilePath(string filename)
        {
            return Path.GetFullPath(filename);
        }

    }
}
