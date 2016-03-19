using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;
using Octokit.Internal;

namespace TileIconifier.Utilities
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
            var updateDetails = new UpdateDetails()
            {
                CurrentVersion = CurrentVersion,
                UpdateAvailable = false,
                LatestVersion = "",
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
        public bool UpdateAvailable;
        public string LatestVersion;
        public string CurrentVersion;
    }
}
