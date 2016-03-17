using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using TileIconifier.Shortcut;

namespace TileIconifier.Utilities
{
    internal static class PowerShellUtils
    {
        internal static void DumpStartLayout(string outputPath)
        {
            using (var powershellInstance = PowerShell.Create())
            {
                powershellInstance.AddCommand("Export-StartLayout");
                powershellInstance.AddParameter("Path", outputPath);
                powershellInstance.Invoke();
            }

            if (!File.Exists(outputPath))
                throw new PowershellException();
        }

        internal static void MarryAppIDs(List<ShortcutItem> shortcutsList)
        {
            using (var powershellInstance = PowerShell.Create())
            {
                powershellInstance.AddCommand("Get-StartApps");
                var results = powershellInstance.Invoke();
                foreach (var properties in results.Select(result => result.Properties))
                {
                    try
                    {
                        var shortcutItem =
                            shortcutsList.First(s => Path.GetFileNameWithoutExtension(s.ShortcutFileInfo.Name) ==
                                                     (string) properties["Name"].Value);
                        shortcutItem.AppId = properties["AppID"].Value.ToString();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }
    }
}