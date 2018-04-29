using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace SpotifyPlaylistSorter
{
    struct Globals
    {
        public static string Token { get; set; }
        public static string CurrentSongId { get; set; }
        public static string CurrentSongName { get; set; }
        public static string CurrentSongArtists { get; set; }
        public static string CurrentSongAlbum { get; set; }
        public static string User { get; set; }
        public static List<Playlist> Playlists { get; set; }
        public static string PlaylistId { get; set; }
    }
    class Program
    {
        internal static HttpClient httpClient = new HttpClient();

        [STAThread]
        static void Main(string[] args)
        {
            Globals.Token = "xxxxxxxxxxxx";
            Globals.PlaylistId = "1t2JjnEA5xRShiAjL7Ogz8";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
            kbh.OnKeyPressed += kbh_OnKeyPressed;
            kbh.HookKeyboard();

            Application.Run();

            kbh.UnHookKeyboard();
        }

        static async Task<bool> Asd()
        {
            Console.WriteLine(await httpClient.GetAsync("http://google.com"));
            return true;
        }

        static async void kbh_OnKeyPressed(object sender, Keys e)
        {
            if (e == Keys.LControlKey)
            {
                await Asd();
            }
            else if (e == Keys.F1)
            {
                Console.WriteLine("F1 - chill");
                await GetCurrentInfo();
                await AddToPlaylist("chill");
                await NextSong();
                await SeekInSong();
            }
            else if (e == Keys.F2)
            {
                Console.WriteLine("F2 - xanny");
                await GetCurrentInfo();
                await AddToPlaylist("xanny");
                await NextSong();
                await SeekInSong();
            }
            else if (e == Keys.F3)
            {
                Console.WriteLine("F3 - yah");
                await GetCurrentInfo();
                await AddToPlaylist("yah");
                await NextSong();
                await SeekInSong();
            }
            else if (e == Keys.F1)
            {
                Console.WriteLine("F4 - oldie");
                await GetCurrentInfo();
                await AddToPlaylist("oldie");
                await NextSong();
                await SeekInSong();
            }
            else if (e == Keys.F1)
            {
                Console.WriteLine("F5 - other");
                await GetCurrentInfo();
                await AddToPlaylist("other");
                await NextSong();
                await SeekInSong();
            }
            else if (e == Keys.F12)
            {
                Console.WriteLine("F12 - next");
                await NextSong();
                await SeekInSong();
            }
        }

        //static void Main(string[] args)
        //{
        //    Globals.Token = "Bearer BQBJQyCpopHc8DUW9PNMbnDzheQcQykGmx2ebyYfsDL--pFX3uGWnpKudlgkryZkYWz0hjBkOiIvb1kQ7y7dHByRq3ZGBKyhpz0JsiHWmgOHuU2C3gy83K7FCxagbWTzv8WTNsF66NIBUYq1TLO_W0HCjQ7WLRFv8WNzSwUGmxLWI7xIFOJpCPtFQsCGD5KOTcCfogItqv3_0sraTq-9oWhKST78C7cHcEk9mRoel_V3T3QSw3CaMU5nilQZ8hN4eGMdZCuOYOERQw";
        //    while (true)
        //    {
        //        GetCurrentInfo().GetAwaiter().GetResult();
        //        //GetPlaylistInfo().GetAwaiter().GetResult();
        //        Console.WriteLine("Playlist name:");
        //        string addTo = Console.ReadLine();
        //        AddToPlaylist(addTo).GetAwaiter().GetResult();
        //        NextSong().GetAwaiter().GetResult();
        //        SeekInSong().GetAwaiter().GetResult();
        //        // Delete previous song from playlist
        //    }
        //}

        private static async Task<bool> AddToPlaylist(string playlist)
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);

                var playlistid = "";
                if (playlist == "chill")
                {
                    playlistid = "2LuxsZH4RGHmqH8AtZkPl3";
                }
                else if (playlist == "yah")
                {
                    playlistid = "5z8pn80B2zgSj12fyv6URe";
                }
                else if (playlist == "xanny")
                {
                    playlistid = "2ZVEFMSDHKK0I0ZrqzuDLd";
                }
                else if (playlist == "oldies")
                {
                    playlistid = "0xe1wMDtwxWPcuc56ZzGqO";
                }
                else if (playlist == "other")
                {
                    playlistid = "4fRAORoQkazdiyRQy3EiW5";
                }

                using (var response = await httpClient.PostAsync("https://api.spotify.com/v1/users/" + Globals.User + "/playlists/" + playlistid + "/tracks?position=0&uris=spotify%3Atrack%3A" + Globals.CurrentSongId, null))
                {
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

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);

                using (var response = await httpClient.PostAsync("https://api.spotify.com/v1/me/player/next", null))
                {
                }
            }
            return true;
        }

        //private static async Task<bool> DeletePreviousSong()
        //{
        //    var baseAddress = new Uri("https://api.adtraction.com/");

        //    using (var httpClient = new HttpClient { BaseAddress = baseAddress })
        //    {
        //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

        //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);
                
        //        using (var response = await httpClient.DeleteAsync("https://api.spotify.com/v1/users/" + Globals.User + "/playlists/" + Globals.PlaylistId + "/tracks", ""))
        //        {
                    
        //        }
        //    }
        //    return true;
        //}

        private static async Task<bool> SeekInSong()
        {
            var baseAddress = new Uri("https://api.adtraction.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);

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

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);

                using (var response = await httpClient.GetAsync("https://api.spotify.com/v1/me/player"))
                {
                    Console.WriteLine(response);
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

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Globals.Token);

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
