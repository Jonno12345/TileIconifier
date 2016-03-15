using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier
{
    [Serializable]
    public class UserCancellationException : Exception { public UserCancellationException() { } }
    [Serializable]
    public class PowershellException : Exception { public PowershellException() { } }
    [Serializable]
    public class InvalidCustomShortcutException : Exception {  public InvalidCustomShortcutException() { } }
    [Serializable]
    public class ValidationFailureException : Exception { public ValidationFailureException() { } }


    //steam exceptions
    [Serializable]
    public class SteamExecutableNotFoundException : Exception { public SteamExecutableNotFoundException() { } }
    [Serializable]
    public class SteamInstallationPathNotFoundException : Exception { public SteamInstallationPathNotFoundException() { } }
    [Serializable]
    public class SteamLibraryPathNotFoundException : Exception { public SteamLibraryPathNotFoundException() { } }

}
