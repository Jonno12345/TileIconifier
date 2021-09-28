#region LICENCE

/****************************************************************************
 *  Library: KeyValues 
 *  Version: 1.0.0.0
 *  By: Callysto.net (http://www.callysto.net)
 *  Target: .NET FRAMEWORK 2.0
 *  
 * 
 *  This library is inspired on "Source Engine" (HL2SDK) KeyValues Class
 *  http://developer.valvesoftware.com/wiki/KeyValues_class (SourceSDK KeyValues Class)
 *  It was used to save data and for send data to a entity.
 *  It can have unlimited keys and subkeys inside a KeyValue
 *  SourceSDK KeyValues metods: http://svn.alliedmods.net/viewvc.cgi/hl2sdk/public/tier1/KeyValues.h?view=co&root=sourcemm
 *  Diferences: 
 *  * More Metods
 *  ** More easy to loop all keys
 *  *** Easy to update or add features
 *
 *  
 *  
 * ***************************************************************************
*/

#endregion

using System.Collections.Generic;
using System.IO;

namespace TileIconifier.Core.Custom.Steam.KeyValues
{
    /// <summary>
    ///     Just Utilis to KeyValues Class
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
                using (var sr = new StreamReader(filePath))
                {
                    var fileLines = new List<string>();
                    while (!sr.EndOfStream)
                        fileLines.Add(sr.ReadLine());
                    return fileLines;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void MakeTabs(StreamWriter stream, uint num)
        {
            for (uint i = 0; i < num; i++)
                stream.Write("\t");
        }
    }
}