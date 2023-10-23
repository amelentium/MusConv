using HtmlAgilityPack;
using PuppeteerSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAssignment.Models;

namespace TestAssignment.Parser
{
    public static class AmazonPlaylistParser
    {
        public async static Task<PlaylistModel?> ParsePlaylistAsync(string playlistUrl)
        {
            PlaylistModel? playlist = null;

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

                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var playlistInfoNodes = doc.DocumentNode.SelectNodes(Constants.Amazon.PlaylistInfoXPath);
                var playlistInfo = playlistInfoNodes.FirstOrDefault(x => x.Attributes.Contains(Constants.Amazon.PlatlistNameAttribute));

                if (playlistInfo == null)
                {
                    return null;
                }

                playlist = new PlaylistModel
                {
                    Type = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistTypeAttribute, string.Empty),
                    Icon = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistIconAttribute, string.Empty),
                    Name = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistNameAttribute, string.Empty),
                    Description = playlistInfo.GetAttributeValue(Constants.Amazon.PlatlistDescriptionAttribute, string.Empty),
                };

                playlist.Songs = await ParsePlaylistSongs(page);

                await browser.CloseAsync();

                return playlist;
            }
            catch
            {
                return playlist;
            }
        }

        private async static Task<List<SongModel>> ParsePlaylistSongs(IPage page)
        {
            var repeatCount = 0;
            var doc = new HtmlDocument();
            var songs = new List<SongModel>();

            while (repeatCount < 3)
            {
                repeatCount++;
                var html = await page.GetContentAsync();
                doc.LoadHtml(html);
                var songInfoNodes = doc.DocumentNode.SelectNodes(Constants.Amazon.SongInfoXPath);
                var songInfos = songInfoNodes.Where(x => x.Attributes.Contains(Constants.Amazon.SongNameAttribute));

                foreach (var songInfo in songInfos)
                {
                    var outerDoc = new HtmlDocument();
                    outerDoc.LoadHtml(songInfo.OuterHtml);
                    var songName = songInfo.GetAttributeValue(Constants.Amazon.SongNameAttribute, string.Empty);

                    if (!string.IsNullOrWhiteSpace(songName) && !songs.Any(x => x.Name == songName))
                    {
                        var duration = outerDoc.DocumentNode
                            .SelectNodes(Constants.Amazon.SongDurationXPath)
                            .LastOrDefault()
                            ?.GetAttributeValue(Constants.Amazon.SongDurationAttribute, null) ?? string.Empty;

                        songs.Add(new SongModel
                        {
                            Name = songName,
                            Duration = duration,
                            AlbumName = songInfo.GetAttributeValue(Constants.Amazon.SongAlbumNameAttribute, string.Empty),
                            ArtistName = songInfo.GetAttributeValue(Constants.Amazon.SongArtistNameAttribute, string.Empty),
                        });

                        repeatCount = 0;
                    }
                }
            }

            return songs;
        }
    }
}
