using ReactiveUI;
using System.Collections.Generic;
using TestAssignment.Models;

namespace TestAssignment.ViewModels;

public class MainViewModel : ViewModelBase
{
    private List<PlaylistModel>? _playlists;

    public List<PlaylistModel>? Playlists
    {
        get => _playlists;
        set { this.RaiseAndSetIfChanged(ref _playlists, value); }
    }

    public MainViewModel()
    {
        Playlists = new List<PlaylistModel>
        {
            new PlaylistModel
            {
                Icon = "https://m.media-amazon.com/images/I/51ktq-qEIcL.jpg",
                Type = "Test Playlist Type",
                Name = "Test Playlist Name",
                Description = "Test Playlist Description",
                Songs = new List<SongModel>
                {
                    new SongModel
                    {
                        Name = "Test Song Name 1",
                        Duration = "Test Song Duration 1",
                        AlbumName = "Test Song Album Name 1",
                        ArtistName = "Test Song Artist Name 1"
                    },
                    new SongModel
                    {
                        Name = "Test Song Name 2",
                        Duration = "Test Song Duration 2",
                        AlbumName = "Test Song Album Name 2",
                        ArtistName = "Test Song Artist Name 2"
                    },
                },
            },
        };
    }
}
