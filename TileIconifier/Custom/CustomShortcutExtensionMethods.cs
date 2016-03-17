using System;
using System.IO;

namespace TileIconifier.Custom
{
    internal static class CustomShortcutExtensionMethods
    {
        //These are potentially buggy!
        internal static string UnescapeVba(this string input)
        {
            return input.Replace("\"\"", "\"");
        }

        internal static string EscapeVba(this string input)
        {
            return input.Replace("\"", "\"\"");
        }

        /// <summary>
        ///     Extension method to wrap a string with double quotes
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Original string with " surrounding</returns>
        internal static string QuoteWrap(this string input)
        {
            return string.Format("{0}{1}{0}", "\"", input);
        }

        /// <summary>
        ///     Quick and dirty extension method to remove any invalid characters and replace with a space
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Cleaned string for paths/filenames</returns>
        internal static string CleanInvalidFilenameChars(this string input)
        {
            return
                string.Join("", input.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries))
                    .TrimEnd('.');
        }
    }
}