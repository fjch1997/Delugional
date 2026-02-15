using Delugional.Rpc;
using Delugional.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delugional
{
    public class Deluge : DelugeRpc
    {
        public Deluge(DelugeRpcConnection connection) : base(connection)
        {
        }

        public async Task<string> AddMagnetAsync(string url, IDictionary<string, object> options = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Argument is null or whitespace", nameof(url));

            return await CallAsync("core.add_torrent_magnet", url, options?.ToObjectDictionary()) as string;
        }

        public async Task<string> AddTorrentAsync(string fileName, byte[] fileDump, IDictionary<string, object> options = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Argument is null or whitespace", nameof(fileName));
            if (fileDump == null)
                throw new ArgumentNullException(nameof(fileDump));
            if (fileDump.Length == 0)
                throw new ArgumentException("Argument is empty collection", nameof(fileDump));

            string fileContents = Base64.Encode(fileDump);

            return await CallAsync("core.add_torrent_file", fileName, fileContents, options?.ToObjectDictionary()) as string;
        }

        public async Task<IDictionary<string, TorrentStatus>> GetTorrentsStatusAsync(Filter filter = null, string[] statusKeys = null, bool diff = false)
        {
            Dictionary<object, object> filters = filter != null ? filter.ToDictionary().ToObjectDictionary() : new Dictionary<object, object>();

            object result = await CallAsync("core.get_torrents_status", filters, statusKeys != null ? statusKeys.ToObjectArray() : new object[0], diff);
            if (result == null)
                return null;

            var dict = new Dictionary<string, TorrentStatus>();
            var torrents = (Dictionary<object, object>)result;
            foreach (var torrent in torrents)
            {
                var torrentId = (string)torrent.Key;
                dict[torrentId] = new TorrentStatus(torrent.Value);
            }

            return dict;
        }

        public async Task<SessionStatus> GetSessionStatusAsync(string[] keys = null)
        {
            if (keys == null)
                keys = new string[0];
            var result = await CallAsync("core.get_session_status", (object)keys);
            return new SessionStatus(result);
        }

        public async Task<TorrentStatus> GetTorrentStatusAsync(string torrentId, string[] statusKeys = null, bool diff = false)
        {
            if (string.IsNullOrWhiteSpace(torrentId))
                throw new ArgumentException("Argument is null or whitespace", nameof(torrentId));

            var result = await CallAsync("core.get_torrent_status", torrentId, statusKeys?.ToObjectArray(), diff);

            return new TorrentStatus(result);
        }

        public Task PauseSessionAsync()
        {
            return CallAsync("core.pause_session");
        }

        public Task PauseTorrentAsync(string[] keys)
        {
            return CallAsync("core.pause_torrent", (object)keys);
        }

        public async Task<bool> RemoveTorrentAsync(string torrentId, bool removeData = false)
        {
            if (string.IsNullOrWhiteSpace(torrentId))
                throw new ArgumentException("Argument is null or whitespace", nameof(torrentId));

            object result = await CallAsync("core.remove_torrent", torrentId, removeData);

            return result is bool && (bool)result;
        }

        public async Task<object[]> RemoveTorrentsAsync(string[] torrentIds, bool removeData = false)
        {
            if (torrentIds == null)
                throw new ArgumentNullException(nameof(torrentIds));
            if (torrentIds.Length == 0)
                throw new ArgumentException("Argument is empty collection", nameof(torrentIds));

            return await CallAsync("core.remove_torrents", torrentIds.ToObjectArray(), removeData) as object[];
        }

        public Task ResumeSessionAsync()
        {
            return CallAsync("core.resume_session");
        }
        public Task ResumeTorrentAsync(string[] keys)
        {
            return CallAsync("core.resume_torrent", (object)keys);
        }

        public async Task<string[]> GetMethodListAsync()
        {
            var result = await CallAsync("daemon.get_method_list");
            return ((object[])result).Cast<string>().ToArray();
        }
    }
}