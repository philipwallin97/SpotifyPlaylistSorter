using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlaylistSorter
{
    public class Playlist
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class PlaylistExternalUrls
    {
        public string spotify { get; set; }
    }

    public class PlaylistExternalUrls2
    {
        public string spotify { get; set; }
    }

    public class PlaylistOwner
    {
        public string display_name { get; set; }
        public PlaylistExternalUrls2 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class PlaylistTracks
    {
        public string href { get; set; }
        public int total { get; set; }
    }

    public class PlaylistItem
    {
        public bool collaborative { get; set; }
        public PlaylistExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<object> images { get; set; }
        public string name { get; set; }
        public PlaylistOwner owner { get; set; }
        public object primary_color { get; set; }
        public bool @public { get; set; }
        public string snapshot_id { get; set; }
        public PlaylistTracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class PlaylistRootObject
    {
        public string href { get; set; }
        public List<PlaylistItem> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total { get; set; }
    }
}
