using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moss.ApiClient.SomaFm
{
    public class SomaFmApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public async Task<IList<Channel>> RetrieveChannels()
        {
            var xml = await GetXml("http://api.somafm.com/channels.xml").ConfigureAwait(false);

            return xml.Root
                      .Elements("channel")
                      .Select(x => new Channel(x.Attribute("id").Value,
                                               x.Element("title").Value,
                                               x.Element("description").Value,
                                               x.Element("dj").Value,
                                               x.Element("djmail").Value,
                                               x.Element("genre").Value,
                                               new Uri(x.Element("image").Value),
                                               new Uri(x.Element("largeimage").Value),
                                               new Uri(x.Element("xlimage").Value),
                                               _unixEpoch.AddSeconds(long.Parse(x.Element("updated").Value)),
                                               uint.Parse(x.Element("listeners").Value),
                                               x.Element("lastPlaying").Value))
                      .ToList();
        }

        public async Task<IEnumerable<Song>> GetRecentlyPlayedSongs(Channel channel)
        {
            var xml = await GetXml($"https://somafm.com/songs/{channel.Id}.xml").ConfigureAwait(false);

            return xml.Root.Elements("song").Select(x => new Song(_unixEpoch.Add(TimeSpan.FromSeconds(long.Parse(x.Element("date").Value))), x.Element("artist").Value, x.Element("album").Value, x.Element("title").Value));
        }

        private static async Task<XDocument> GetXml(string uri)
        {
            using (var response = await _httpClient.GetStreamAsync(uri).ConfigureAwait(false))
            {
                return XDocument.Load(response, LoadOptions.None);
            }
        }
    }
}
