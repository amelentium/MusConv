using Avalonia.Media.Imaging;
using System.Collections.Generic;

namespace TestAssignment.Models
{
    public class PlaylistModel
    {
        public string? Type { get; set; }
        public Bitmap? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<SongModel>? Songs { get; set; }
    }
}
