using Avalonia.Media.Imaging;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TestAssignment.Models;

namespace TestAssignment.ViewModels;

public class MainViewModel : ViewModelBase
{
    private HttpClient _httpClient;
    private List<PlaylistModel>? _playlists;

    public List<PlaylistModel>? Playlists
    {
        get => _playlists;
        set { this.RaiseAndSetIfChanged(ref _playlists, value); }
    }

    public MainViewModel()
    {
        _httpClient = new HttpClient();

        Playlists = new List<PlaylistModel>
        {
            new PlaylistModel()
            {
                Icon = new Bitmap(new MemoryStream(_httpClient.GetByteArrayAsync("https://m.media-amazon.com/images/I/51ktq-qEIcL.jpg").Result)),
                Type = "Test Type",
                Name = "Test Name",
                Description = "Test Description",
                Songs = new List<SongModel>(),
            },
        };
    }
}
