// Utilities for the library
using System.Collections.Generic;
using System.IO;

namespace Callysto
{
    /// <summary>
    /// Just Utilis to KeyValues Class
    /// </summary>
    internal static class Utils
    {
        /*public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }*/
        public static List<string> ReadFileToList(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    List<string> fileLines = new List<string>();
                    while (!sr.EndOfStream)
                        fileLines.Add(sr.ReadLine());
                    return fileLines;
                }
            }catch
            {
                return null;
            }
        }

        public static void makeTabs(StreamWriter stream, uint num)
        {
            for (uint i = 0; i < num; i++)
                stream.Write("\t");
        }
    }
}
