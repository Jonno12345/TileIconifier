using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace TileIconifier.Utilities
{
    static class PowerShellUtils
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

        internal static void MarryAppIDs(List<ShortcutItem> _shortcutsList)
        {
            using (var powershellInstance = PowerShell.Create())
            {
                powershellInstance.AddCommand("Get-StartApps");
                var results = powershellInstance.Invoke();
                foreach (var result in results)
                {
                    PSMemberInfoCollection<PSPropertyInfo> properties = result.Properties;
                    try
                    {
                        var shortcutItem = _shortcutsList.Where(s => Path.GetFileNameWithoutExtension(s.ShortcutFileInfo.Name) == (string)properties["Name"].Value)
                                       .First();
                        shortcutItem.AppId = properties["AppID"].Value.ToString();

                    }
                    catch { }
                }
            }
        }
    }
}
