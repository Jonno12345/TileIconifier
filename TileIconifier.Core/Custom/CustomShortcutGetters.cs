#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
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
using System.Collections.Generic;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom
{
    public static class CustomShortcutGetters
    {
        public static string CustomShortcutVbsPath
            => IoUtils.ProgramDataPath;

        public static string CustomShortcutCurrentUserPath => Environment.ExpandEnvironmentVariables(
            @"%APPDATA%\Microsoft\Windows\Start Menu\Programs\TileIconify\Custom Shortcuts\");

        public static string CustomShortcutAllUsersPath => Environment.ExpandEnvironmentVariables(
            @"%PROGRAMDATA%\Microsoft\Windows\Start Menu\Programs\TileIconify\Custom Shortcuts\");

        public static string ExplorerPath => Environment.ExpandEnvironmentVariables(@"%SYSTEMROOT%\explorer.exe");

        public static string GoogleAppLibraryPath =>
            Environment.ExpandEnvironmentVariables(@"%localappdata%\Google\Chrome\User Data\Default\Extensions");

        public static List<string> DefaultChromeInstallationPaths =>
            new List<string>
            {
                @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            };

        public static List<string> ExcludedChromeAppIds =>
            new List<string>
            {
                @"ahjaciijnoiaklcomgnblndopackapon",
                @"bpficcollblmkmoofnbnfhibdhfolamm"
            };


        public static Dictionary<string, string> ExplorerGuids { get; } = new Dictionary<string, string>
        {
            {"Action Center", "{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}"},
            {"Add Features", "{BE122A0E-4503-11DA-8BDE-F66BAD1E3F3A}"},
            {"Add Network Location", "{D4480A50-BA28-11d1-8E75-00C04FA31A86}"},
            {"Administrative Tools", "{D20EA4E1-3957-11d2-A40B-0C5020524153}"},
            {"Advanced User Accounts (netplwiz)", "{7A9D77BD-5403-11d2-8785-2E0420524153}"},
            {"Applications", "{4234d49b-0245-4df3-b780-3893943456e1}"},
            {"AutoPlay", "{9C60DE1E-E5FC-40f4-A487-460851A8D915}"},
            {"Biometric Devices (Windows 8 only)", "{0142e4d0-fb7a-11dc-ba4a-000ffe7ab428}"},
            {"BitLocker Drive Encryption", "{D9EF8727-CAC2-4e60-809E-86F80A666C91}"},
            {"Bluetooth Devices", "{28803F59-3A75-4058-995F-4EE5503B023C}"},
            {"Briefcase", "{85BBD920-42AO-1069-A2E4-08002B30309D}"},
            {"Color Management", "{B2C761C6-29BC-4f19-9251-E6195265BAF1}"},
            {"Command Folder", "{437ff9c0-a07f-4fa0-af80-84b6c6440a16}"},
            {"Common Places FS Folder", "{d34a6ca6-62c2-4c34-8a7c-14709c1ad938}"},
            {"Computer (This PC)", "{20d04fe0-3aea-1069-a2d8-08002b30309d}"},
            {"Connect To", "{38A98528-6CBF-4CA9-8DC0-B1E1D10F7B1B}"},
            {"Control Panel", "{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}"},
            {"Control Panel (All Tasks)", "{ED7BA470-8E54-465E-825C-99712043E01C}"},
            {"Control Panel (Category view)", "{26EE0668-A00A-44D7-9371-BEB064C98683}"},
            {"Control Panel (Icons view)", "{21EC2020-3AEA-1069-A2DD-08002B30309D}"},
            {"Credential Manager", "{1206F5F1-0569-412C-8FEC-3204630DFB70}"},
            {"Date and Time", "{E2E7934B-DCE5-43C4-9576-7FE4F75E7480}"},
            {"Default Programs", "{17cd9488-1228-4b2f-88ce-4298e93e0966}"},
            {"delegate folder that appears in Computer", "{b155bdf8-02f0-451e-9a26-ae317cfd7779}"},
            {"Desktop folder", "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"},
            {"Device Manager", "{74246bfc-4c96-11d0-abef-0020af6b0b7a}"},
            {"Devices and Printers", "{A8A91A66-3A7D-4424-8D24-04E180695C7A}"},
            {"Display", "{C555438B-3C23-4769-A71F-B6D3D9B6053A}"},
            {"Documents folder", "{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}"},
            {"Downloads folder", "{374DE290-123F-4565-9164-39C4925E467B}"},
            {"Ease of Access Center", "{D555645E-D4F8-4c29-A827-D93C859C4F2A}"},
            {"E-mail (default program)", "{2559a1f5-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"Family Safety", "{96AE8D84-A250-4520-95A5-A47A7E3C548B}"},
            {"Favorites", "{323CA680-C24D-4099-B94D-446DD2D7249E}"},
            {"File History", "{F6B6E965-E9B2-444B-9286-10C9152EDBC5}"},
            {"Folder Options", "{6DFD7C5C-2451-11d3-A299-00C04F8EF6AF}"},
            {"Font Settings", "{93412589-74D4-4E4E-AD0E-E0CB621440FD}"},
            {"Fonts", "{BD84B380-8CA2-1069-AB1D-08000948534}"},
            {"Frequent folders", "{3936E9E4-D92C-4EEE-A85A-BC16D5EA0819}" },
            {"Games Explorer", "{ED228FDF-9EA8-4870-83b1-96b02CFE0D52}"},
            {"Get Programs", "{15eae92e-f17a-4431-9f28-805e482dafd4}"},
            {"Help and Support", "{2559a1f1-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"HomeGroup1", "{6785BFAC-9D2D-4be5-B7E2-59937E8FB80A}"},
            {"HomeGroup2", "{67CA7650-96E6-4FDD-BB43-A8E774F73A57}"},
            {"HomeGroup3", "{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}"},
            {"Hyper-V Remote File Browsing", "{0907616E-F5E6-48D8-9D61-A91C3D28106D}"},
            {"Indexing Options", "{87D66A43-7B11-4A28-9811-C86EE395ACF7}"},
            {"Infared", "{A0275511-0E86-4ECA-97C2-ECD8F1221D08}"},
            {"Installed Updates", "{d450a8a1-9568-45c7-9c0e-b4f9fb4537bd}"},
            {"Internet Options", "{A3DD4F92-658A-410F-84FD-6FBBBEF2FFFE}"},
            {"Keyboard Properties", "{725BE8F7-668E-4C7B-8F90-46BDB0936430}"},
            {"Language Settings", "{BF782CC9-5A52-4A17-806C-2A894FFEEAC5}"},
            {"Libraries", "{031E4825-7B94-4dc3-B131-E946B44C8DD5}"},
            {"Location Sensors", "{E9950154-C418-419e-A90A-20C5287AE24B}"},
            {"﻿﻿Media Servers", "{289AF617-1CC3-42A6-926C-E6A863F0E3BA}"},
            {"Mobility Center", "{5ea4f148-308c-46d7-98a9-49041b1dd468}"},
            {"Mouse Properties", "{6C8EEC18-8D75-41B2-A177-8831D59D2D50}"},
            {"Music folder", "{1CF1260C-4DD0-4ebb-811F-33C572699FDE}"},
            {"Network", "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}"},
            {"Network and Sharing Center", "{8E908FC9-BECC-40f6-915B-F4CA0E70D03D}"},
            {"Network Connections", "{7007ACC7-3202-11D1-AAD2-00805FC1270E}"},
            {"Networks Flyout (Connect To)", "{38A98528-6CBF-4CA9-8DC0-B1E1D10F7B1B}"},
            {"Network (WorkGroup)", "{208D2C60-3AEA-1069-A2D7-08002B30309D}"},
            {"Notification Area Icons", "{05d7b0f4-2121-4eff-bf6b-ed3f69b894d9}"},
            {"NVIDIA Control Panel (if installed)", "{0bbca823-e77d-419e-9a44-5adec2c8eeb0}"},
            {"Offline Files Folder", "{AFDB1F70-2A4C-11d2-9039-00C04F8EEB3E}"},
            {"OneDrive", "{018D5C66-4533-4307-9B53-224DE2ED1FE6}" },
            {"Pen and Touch", "{F82DF8F7-8B9F-442E-A48C-818EA735FF9B}"},
            {"Performance Information and Tools (WEI)", "{78F3955E-3B90-4184-BD14-5397C15F1EFC}"},
            {"Personalization", "{ED834ED6-4B5A-4bfe-8F11-A626DCB6A921}"},
            {"Phone & Modem \"Location Information\"", "{40419485-C444-4567-851A-2DD7BFA1684D}"},
            {"Pictures folder", "{3ADD1653-EB32-4cb0-BBD7-DFA0ABB5ACCA}"},
            {"Portable Devices", "{35786D3C-B075-49b9-88DD-029876E11C01}"},
            {"Power Options", "{025A5937-A6BE-4686-A844-36FE4BEC8B6D}"},
            {"Previous Versions Results Folder", "{f8c2ab3b-17bc-41da-9758-339d7dbf2d88}"},
            {"printhood delegate folder", "{ed50fc29-b964-48a9-afb3-15ebb9b97f36}"},
            {"Printers", "{2227A280-3AEA-1069-A2DE-08002B30309D}"},
            {"Programs and Features", "{7b81be6a-ce2b-4676-a29e-eb907a5126c5}"},
            {"Public Folder", "{4336a54d-038b-4685-ab02-99bb52d3fb8b}"},
            {"Quick Access", "{679f85cb-0220-4080-b29b-5540cc05aab6}" },
            {"Recent Places", "{22877a6d-37a1-461a-91b0-dbda5aaebc99}"},
            {"Recovery", "{9FE63AFD-59CF-4419-9775-ABCC3849F861}"},
            {"Recycle Bin", "{645FF040-5081-101B-9F08-00AA002F954E}"},
            {"Region and Language", "{62d8ed13-c9d0-4ce8-a914-47dd628fb1b0}"},
            {"RemoteApp and Desktop Connections", "{241D7C96-F8BF-4F85-B01F-E2B043341A4B}"},
            {"Removable Storage Devices", "{a6482830-08eb-41e2-84c1-73920c2badb9}"},
            {"﻿﻿Results Folder", "{2965e715-eb66-4719-b53f-1672673bbefa}"},
            {"Run", "{2559a1f3-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"Search Apps", "{2559a1f8-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"Search Files", "{2559a1f0-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"Search Results", "{9343812e-1c37-4a49-a12e-4b2d810d956b}"},
            {"Security and Maintenance", "{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}" },
            {"Set Program Access and Defaults", "{2559a1f7-21d7-11d4-bdaf-00c04f60b9f0}"},
            {"Show Desktop", "{3080F90D-D7AD-11D9-BD98-0000947B0257}"},
            {"Sound", "{F2DDFC82-8F12-4CDD-B7DC-D4FE1425AA4D}"},
            {"Speech Recognition", "{58E3C745-D971-4081-9034-86E34B30836A}"},
            {"Storage Spaces", "{F942C606-0914-47AB-BE56-1321B8035096}"},
            {"Switch Between Windows", "{3080F90E-D7AD-11D9-BD98-0000947B0257}"},
            {"Sync Center", "{9C73F5E5-7AE7-4E32-A8E8-8D23B85255BF}"},
            {"Sync Setup Folder", "{2E9E59C0-B437-4981-A647-9C34B9B90891}"},
            {"System", "{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}"},
            {"System Icons", "{05d7b0f4-2121-4eff-bf6b-ed3f69b894d9}"},
            {"Tablet PC Settings", "{80F3F1D5-FECA-45F3-BC32-752C152E456E}"},
            {"Taskbar Properties", "{0DF44EAA-FF21-4412-828E-260A8728E7F1}"},
            {"Text to Speech", "{D17D1D6D-CC3F-4815-8FE3-607E7D5D10B3}"},
            {"This PC (Computer)", "{20d04fe0-3aea-1069-a2d8-08002b30309d}"},
            {"Troubleshooting", "{C58C4893-3BE0-4B45-ABB5-A63E4B8C8651}"},
            {"User Accounts", "{60632754-c523-4b62-b45c-4172da012619}"},
            {"User Folder", "{59031a47-3f72-44a7-89c5-5595fe6b30ee}"},
            {"User Pinned", "{1f3427c8-5c10-4210-aa03-2ee45287d668}"},
            {"Videos folder", "{A0953C92-50DC-43bf-BE83-3742FED03C9C}"},
            {"Window Switcher (Flip-2D)", "{3080F90E-D7AD-11D9-BD98-0000947B0257}"},
            {"Windows Defender", "{D8559EB9-20C0-410E-BEDA-7ED416AECC2A}"},
            {"Windows Features", "{67718415-c450-4f3c-bf8a-b487642dc39b}"},
            {"Windows 7 File Recovery", "{B98A2BEA-7D42-4558-8BD1-832F41BAC6FD}"},
            {"Windows Firewall", "{4026492F-2F69-46B8-B9BF-5654FC07E423}"},
            {"Windows Mobility Center", "{5ea4f148-308c-46d7-98a9-49041b1dd468}"},
            {"Windows To Go", "{8E0C279D-0BD1-43C3-9EBD-31C3DC5B8A77}"},
            {"Windows Update", "{36eef7db-88ad-4e81-ad49-0e313f0c35f8}"},
            {"Work Folders", "{ECDB0924-4208-451E-8EE0-373C0956DE16}"}
        };
    }

    public enum CustomShortcutType
    {
        Explorer = 0,
        Steam = 1,
        Other = 2,
        ChromeApp = 3,
        WindowsStoreApp = 4
    }

    public enum ShortcutUser
    {
        Unknown = 0,
        AllUsers = 1,
        CurrentUser = 2
    }

    public enum WindowType
    {
        Hidden = 0,
        ActiveAndDisplayed = 1,
        ActiveAndMinimized = 2,
        ActiveAndMaximized = 3,
        InactiveAndRecent = 4,
        ActiveAndCurrent = 5,
        MinimizedNextActive = 6,
        MinimizedAndInactive = 7,
        InactiveAndCurrent = 8,
        ActiveAndRestore = 9,
        BasedOnStartingProgram = 10
    }
}