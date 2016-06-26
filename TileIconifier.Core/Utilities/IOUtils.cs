using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Core.Utilities
{
    class IoUtils
    {
        public static string ProgramDataPath => Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\TileIconify\");
    }
}
