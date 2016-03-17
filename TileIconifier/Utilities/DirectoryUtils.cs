using System.IO;

namespace TileIconifier.Utilities
{
    public static class DirectoryUtils
    {
        public static string GetUniqueDirName(string sourcefolder)
        {
            var dir = sourcefolder;

            for (var i = 1;; ++i)
            {
                if (!Directory.Exists(dir))
                {
                    return dir;
                }

                dir = sourcefolder + "_" + i;
            }
        }
    }
}