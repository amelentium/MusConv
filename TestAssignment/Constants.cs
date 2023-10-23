namespace TestAssignment
{
    public static class Constants
    {
        public static string[] PlaylistUrls = {
            "https://music.amazon.com/playlists/B01M11SBC8",
            "https://music.amazon.com/albums/B001230JXC",
        };

        public static class Amazon
        {
            public const string PlaylistInfoXPath = "//music-detail-header";
            public const string PlatlistTypeAttribute = "label";
            public const string PlatlistIconAttribute = "image-src";
            public const string PlatlistNameAttribute = "headline";
            public const string PlatlistDescriptionAttribute = "secondary-text";

            public const string SongInfoSelector = "music-text-row";
            public const string SongInfoXPath = $"//{SongInfoSelector}";
            public const string SongNameAttribute = "primary-text";
            public const string SongArtistNameAttribute = "secondary-text-1";
            public const string SongAlbumNameAttribute = "secondary-text-2";
            public const string SongDurationXPath = "//div/div/music-link";
            public const string SongDurationAttribute = "title";

        }
    }
}
