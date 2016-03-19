using System;

namespace TileIconifier
{
    [Serializable]
    public class UserCancellationException : Exception { }
    [Serializable]
    public class PowershellException : Exception { }
    [Serializable]
    public class InvalidCustomShortcutException : Exception { }
    [Serializable]
    public class ValidationFailureException : Exception { }
    [Serializable]
    public class UnableToDetectAdministratorException : Exception { }
    
    //steam exceptions
    [Serializable]
    public class SteamExecutableNotFoundException : Exception { }
    [Serializable]
    public class SteamInstallationPathNotFoundException : Exception { }
    [Serializable]
    public class SteamLibraryPathNotFoundException : Exception { }

}
