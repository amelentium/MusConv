using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public const string PlaylistInfoSelector = "music-detail-header";
            public const string PlatlistTypeAttribute = "label";
            public const string PlatlistIconAttribute = "image-src";
            public const string PlatlistNameAttribute = "headline";
            public const string PlatlistDescriptionAttribute = "secondary-text";

            public const string PlaylistSongsContainerSelector = "music-container";

        }
    }
}
