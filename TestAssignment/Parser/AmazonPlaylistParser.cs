using HtmlAgilityPack;
using PuppeteerSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestAssignment.Models;

namespace TestAssignment.Parser
{
    public static class AmazonPlaylistParser
    {

        public async static Task<PlaylistModel?> ParsePlaylistAsync(string playlistUrl)
        {
            try
            {
                using var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();

                await using var browser = await Puppeteer.LaunchAsync(
                    new LaunchOptions
                    {
                        Headless = false
                    });

                await using var page = await browser.NewPageAsync();
                await page.GoToAsync(playlistUrl);

                await page.WaitForNetworkIdleAsync();
                var html = await page.GetContentAsync();
                browser.CloseAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var playlistInfoNodes = doc.DocumentNode.SelectNodes($"//{Constants.Amazon.PlaylistInfoSelector}");
                var playlistInfo = playlistInfoNodes.FirstOrDefault(x => x.Attributes.Contains(Constants.Amazon.PlatlistNameAttribute));

                if (playlistInfo != null)
                {
                    return new PlaylistModel
                    {
                        Type = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistTypeAttribute, string.Empty),
                        Icon = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistIconAttribute, string.Empty),
                        Name = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistNameAttribute, string.Empty),
                        Description = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistDescriptionAttribute, string.Empty)
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
