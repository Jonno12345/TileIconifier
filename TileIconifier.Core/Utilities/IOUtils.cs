#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.IO;

namespace TileIconifier.Core.Utilities
{
    internal class IoUtils
    {
        public static string ProgramDataPath => Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\TileIconify\");

        public static void ForceDelete(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        public static void FileActionRetainingAttributes(string filePath, Action action, bool fileMustExist = false)
        {
            if (!File.Exists(filePath))
            {
                if (fileMustExist)
                {
                    throw new FileNotFoundException();
                }
                action();
                return;
            }

            var storedFileAttributes = File.GetAttributes(filePath);
            File.SetAttributes(filePath, FileAttributes.Normal);

            action();

            File.SetAttributes(filePath, storedFileAttributes);
        }
    }
}