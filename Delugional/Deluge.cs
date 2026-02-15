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

        private object[] TransformStatusKeys(Dictionary<string, string> mapping, string[] keys)
        {
            if (keys == null)
                return new object[0];
            for (int i = 0; i < keys.Length; i++)
            {
                if (mapping.TryGetValue(keys[i], out string mappedKey))
                {
                    keys[i] = mappedKey;
                }
            }
            return keys.Cast<object>().ToArray();
        }

        /// <summary>
        /// Get the status of torrents matching the specified filters.
        /// </summary>
        /// <param name="filter">Only torrents matching this filter will be returned.</param>
        /// <param name="statusKeys">Only properties matching this argument will be returned. If the array is null or empty, all properties will be returned. You may specify properties by their name in the <see cref="TorrentStatus"/> class using the nameof operator. You may also specify them in raw values according to Deluge documentations. An exception will be thrown when trying to access properties not in this list.</param>
        /// <param name="diff">Return only the differences between the previous and current status.</param>
        /// <returns>A wrapper object for the torrent status. An exception will be thrown when trying to access properties not provided in the statusKeys argument.</returns>
        public async Task<IDictionary<string, TorrentStatus>> GetTorrentsStatusAsync(Filter filter = null, string[] statusKeys = null, bool diff = false)
        {
            var filters = filter != null ? filter.ToDictionary().ToObjectDictionary() : new Dictionary<object, object>();
            var result = await CallAsync("core.get_torrents_status", filters, TransformStatusKeys(TorrentStatus.PropertyNameMappings, statusKeys), diff);
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

        /// <summary>
        /// Get the status of the torrent with matching ID.
        /// </summary>
        /// <param name="torrentId">The ID of the torrent. This is the urn:btih query parameter in the magnet link or the return value of <see cref="AddMagnetAsync(string, IDictionary{string, object})"/>.</param>
        /// <param name="statusKeys">Only properties matching this argument will be returned. If the array is null or empty, all properties will be returned. You may specify properties by their name in the <see cref="TorrentStatus"/> class using the nameof operator. You may also specify them in raw values according to Deluge documentations. An exception will be thrown when trying to access properties not in this list.</param>
        /// <param name="diff">Return only the differences between the previous and current status.</param>
        /// <returns>A wrapper object for the torrent status. An exception will be thrown when trying to access properties not provided in the statusKeys argument.</returns>
        public async Task<TorrentStatus> GetTorrentStatusAsync(string torrentId, string[] statusKeys = null, bool diff = false)
        {
            if (string.IsNullOrWhiteSpace(torrentId))
                throw new ArgumentException("Argument is null or whitespace", nameof(torrentId));

            var result = await CallAsync("core.get_torrent_status", torrentId, TransformStatusKeys(TorrentStatus.PropertyNameMappings, statusKeys), diff);

            return new TorrentStatus(result);
        }


        public async Task<SessionStatus> GetSessionStatusAsync(string[] keys = null)
        {
            var result = await CallAsync("core.get_session_status", (object)TransformStatusKeys(SessionStatus.PropertyNameMappings, keys));
            return new SessionStatus(result);
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