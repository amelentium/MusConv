using ReactiveUI;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAssignment.Models;
using TestAssignment.Parser;

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
        Playlists = new List<PlaylistModel>();
        LoadPlaylists();
    }

    private void LoadPlaylists()
    {
        foreach (var playlistUrl in Constants.PlaylistUrls)
        {
            var url = playlistUrl;

            _ = Task.Run(async () => 
            {
                await LoadPlaylistAsync(url);
            });
        }
    }

    private async Task LoadPlaylistAsync(string playlistUrl)
    {
        var playlist = await AmazonPlaylistParser.ParsePlaylistAsync(playlistUrl);
        if (playlist != null)
        {
            var playlists = new List<PlaylistModel>();
            if (Playlists != null)
            {
                playlists.AddRange(Playlists);
            }
            playlists?.Add(playlist);
            Playlists = playlists;
        }
    }
}
