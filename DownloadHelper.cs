using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace azure_iplookup
{
    internal class DownloadJson
    {
        public static async Task<string> DownloadServiceTagJsonFile()
        {
            string url = "https://www.microsoft.com/en-us/download/confirmation.aspx?id=56519";

            var config = Configuration.Default.WithDefaultLoader();
            var address = url;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cellSelector = "a";
            var cells = document.QuerySelectorAll(cellSelector).Attr("href");
            var serviceTagsJsonFileUrl = cells.Where(c => c.Contains("ServiceTag") && c.Contains(".json")).First();
            string serviceTagsJsonFileName = serviceTagsJsonFileUrl.Split("/").Last();
            if (File.Exists(serviceTagsJsonFileName))
            {
                Console.WriteLine("Latest file already exists. Skipping download.");
                return string.Empty;
            }

            WebClient client = new WebClient();
            client.DownloadFile(serviceTagsJsonFileUrl, serviceTagsJsonFileName);
            return serviceTagsJsonFileName;

        }

        internal static async Task<string> DownloadAndRenameServiceTagsAndIPJsonFile(string serviceTagJsonFileName)
        {
            var downloadedServiceTagsJsonFileName = await DownloadServiceTagJsonFile();
            if (!string.IsNullOrWhiteSpace(downloadedServiceTagsJsonFileName))
            {
                Console.WriteLine("Latest Service Tag Json file downloaded");
                var archiveJsonFileName = $"ServiceTags_Public_Arhive_{DateTime.Today.ToString("ddMMyyyy")}.json";
                if (File.Exists(serviceTagJsonFileName))
                {
                    File.Move(serviceTagJsonFileName, archiveJsonFileName);
                    Console.WriteLine($"Previous {serviceTagJsonFileName} renamed to {archiveJsonFileName}");
                }

                if (!File.Exists(serviceTagJsonFileName))
                {
                    File.Copy(downloadedServiceTagsJsonFileName, serviceTagJsonFileName);
                    Console.WriteLine(
                        $"Newly downloaded file {downloadedServiceTagsJsonFileName} renamed to {serviceTagJsonFileName}");
                }


                return downloadedServiceTagsJsonFileName;
            }

            return downloadedServiceTagsJsonFileName;
        }
    }
}