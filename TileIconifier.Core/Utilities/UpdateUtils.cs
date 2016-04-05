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

using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;

namespace TileIconifier.Core.Utilities
{
    public static class UpdateUtils
    {
        public static GitHubClient Client { get; } = new GitHubClient(new ProductHeaderValue("TileIconify"));

        public static string CurrentVersion => Assembly.GetExecutingAssembly()
            .GetName()
            .Version
            .ToString();


        public static async Task<UpdateDetails> CheckForUpdate()
        {
            var updateDetails = new UpdateDetails
            {
                CurrentVersion = CurrentVersion,
                UpdateAvailable = false,
                LatestVersion = ""
            };

            var releases = await Client.Repository.Release.GetAll("Jonno12345", "TileIconify");
            var latestRelease = releases[0];
            updateDetails.LatestVersion = Regex.Replace(latestRelease.TagName, @"[^0-9\.]", "");
            updateDetails.UpdateAvailable = LatestIsNewerThanCurrent(CurrentVersion, updateDetails.LatestVersion);

            return updateDetails;
        }

        private static bool LatestIsNewerThanCurrent(string currentVersion, string latestVersion)
        {
            var currentVersionNumbers = currentVersion.Split('.');
            var latestVersionNumbers = latestVersion.Split('.');

            for (var i = 0; i < 4; i++)
            {
                var currentSubNumber = int.Parse(currentVersionNumbers[i]);
                var latestSubNumber = int.Parse(latestVersionNumbers[i]);

                if (currentSubNumber > latestSubNumber)
                    return false;

                if (currentSubNumber < latestSubNumber)
                    return true;
            }
            return false;
        }
    }

    public class UpdateDetails
    {
        public string CurrentVersion;
        public string LatestVersion;
        public bool UpdateAvailable;
    }
}