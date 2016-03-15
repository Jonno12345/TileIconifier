using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Utilities
{
    public static class DirectoryUtils
    {
        public static string GetUniqueDirName(string sourcefolder)
        {
            string dir = sourcefolder;

            for (int i = 1; ; ++i)
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
