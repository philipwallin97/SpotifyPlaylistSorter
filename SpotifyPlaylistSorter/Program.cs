using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace SpotifyPlaylistSorter
{
    struct Globals
    {
        public static string CurrentSongId { get; set; }
        public static string CurrentSongName { get; set; }
        public static string CurrentSongArtists { get; set; }
        public static string CurrentSongAlbum { get; set; }
        public static string User { get; set; }
        public static List<Playlist> Playlists { get; set; }
    }
    class Program
    { 
        static void Main(string[] args)
        {
            GetPlaylistInfo().GetAwaiter().GetResult();
            GetCurrentInfo().GetAwaiter().GetResult();
            Console.WriteLine("Playlist name:");
            string addTo = Console.ReadLine();
            AddToPlaylist(addTo).GetAwaiter().GetResult();
            NextSong().GetAwaiter().GetResult();
            SeekInSong().GetAwaiter().GetResult();
        }

        private static async Task<bool> AddToPlaylist(string playlist)
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "xxxxxxxxxxxxxxxxxxx");

                var playlistid = "";
                foreach (var p in Globals.Playlists)
                {
                    Console.WriteLine(p.Name);
                    if (p.Name == playlist)
                    {
                        playlistid = p.Id;
                        Console.WriteLine(p.Id);
                        break;
                    }
                }

                using (var response = await httpClient.PostAsync("https://api.spotify.com/v1/users/" + Globals.User + "/playlists/" + playlistid + "/tracks?position=0&uris=spotify%3Atrack%3A" + Globals.CurrentSongId, null))
                {
                    Console.WriteLine(response);
                    Console.ReadLine();
                }
            }
            return true;
        }

        private static async Task<bool> NextSong()
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "xxxxxxxxxxxxxxxxxxx");

                using (var response = await httpClient.PostAsync("https://api.spotify.com/v1/me/player/next", null))
                {
                }
            }
            return true;
        }

        private static async Task<bool> SeekInSong()
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "xxxxxxxxxxxxxxxxxxx");

                using (var response = await httpClient.PutAsync("https://api.spotify.com/v1/me/player/seek?position_ms=30000", null))
                {
                }
            }
            return true;
        }

        private static async Task<bool> GetCurrentInfo()
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "xxxxxxxxxxxxxxxxxxx");

                using (var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/player"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<RootObject>(responseData);

                    // User
                    string[] r = result.context.uri.Split(':');
                    Globals.User = r[2];

                    // Songname
                    Globals.CurrentSongName = result.item.name;

                    // Artists
                    var artists = "";
                    if (result.item.artists.Count > 1)
                    {
                        for (int i = 0; i < result.item.artists.Count; i++)
                        {
                            if (i == result.item.artists.Count - 1)
                            {
                                artists += result.item.artists[i].name;
                            }
                            else
                            {
                                artists += result.item.artists[i].name + ", ";
                            }
                        }
                    }
                    else
                    {
                        artists += result.item.artists[0].name;
                    }
                    Globals.CurrentSongArtists = artists;

                    // Album
                    Globals.CurrentSongAlbum = result.item.album.name;

                    // Id
                    Globals.CurrentSongId = result.item.id;
                }
            }
            return true;
        }

        private static async Task<bool> GetPlaylistInfo()
        {
            Globals.Playlists = new List<Playlist>();
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "xxxxxxxxxxxxxxxxxxx");

                using (var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/playlists?limit=50"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<PlaylistRootObject>(responseData);

                    for (int i = 0; i < result.items.Count; i++)
                    {
                        Playlist p = new Playlist
                        {
                            Name = result.items[i].name,
                            Id = result.items[i].id
                        };
                        Globals.Playlists.Add(p);

                    }
                }
                using (var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/playlists?limit=50&offset=50"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<PlaylistRootObject>(responseData);

                    for (int i = 0; i < result.items.Count; i++)
                    {
                        Playlist p = new Playlist
                        {
                            Name = result.items[i].name,
                            Id = result.items[i].id
                        };
                        Globals.Playlists.Add(p);
                    }
                }
                using (var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/playlists?limit=50&offset=100"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<PlaylistRootObject>(responseData);

                    for (int i = 0; i < result.items.Count; i++)
                    {
                        Playlist p = new Playlist
                        {
                            Name = result.items[i].name,
                            Id = result.items[i].id
                        };
                        Globals.Playlists.Add(p);
                    }
                }
            }
            return true;
        }
    }
}
