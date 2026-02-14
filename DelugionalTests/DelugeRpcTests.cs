using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Delugional;
using Delugional.Daemon;
using Delugional.Rpc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DelugionalTests
{
    [TestClass]
    public class DelugeRpcTests
    {
        private IDelugeDaemon daemon;
        private User user = AuthFile.OpenDefault().First();

        [TestInitialize]
        public void Initialize()
        {
            daemon = DelugeDaemon.Default;
            if (!daemon.Running)
                daemon.Start();
        }

        [TestCleanup]
        public void Cleanup()
        {
            daemon.Stop();
        }

        [TestMethod]
        public async Task AddTorrentFile()
        {
            using (IDelugeRpc deluge = await daemon.OpenRpcAsync())
            {
                await deluge.LoginAsync(user.Username, user.Password);

                string torrentId = await deluge.AddTorrentAsync("torrent_file", await File.ReadAllBytesAsync("ubuntu-24.04.4-desktop-amd64.iso.torrent"));

                await deluge.RemoveTorrentAsync(torrentId);

                Assert.IsNotNull(torrentId, "torrentId != null");
            }
        }

        [TestMethod]
        public async Task AddMagnetLink()
        {
            using (IDelugeRpc deluge = await daemon.OpenRpcAsync())
            {
                await deluge.LoginAsync(user.Username, user.Password);

                string torrentId = await deluge.AddMagnetAsync(Resources.MagnetLink1);

                await deluge.RemoveTorrentAsync(torrentId, true);

                Assert.IsNotNull(torrentId, "torrentId != null");
            }
        }

        private string ParseHashFromMagnetLink(string magnetLink)
        {
            const string HashPrefix = "urn:btih:";
            int hashStartIndex = magnetLink.IndexOf(HashPrefix) + HashPrefix.Length;
            int hashEndIndex = magnetLink.IndexOf('&', hashStartIndex);
            if (hashEndIndex == -1)
                hashEndIndex = magnetLink.Length;
            return magnetLink.Substring(hashStartIndex, hashEndIndex - hashStartIndex);
        }

        private async Task<string> AddMagnetAsync(IDelugeRpc deluge)
        {
            var existingTorrents = await deluge.GetTorrentsStatusAsync(new Filter { Keywords = { ParseHashFromMagnetLink(Resources.MagnetLink1) } });
            if (existingTorrents.Count > 0)
            {
                // Torrent already exists, return its ID.
                return existingTorrents.Keys.First();
            }
            return await deluge.AddMagnetAsync(Resources.MagnetLink1);
        }

        [TestMethod]
        public async Task GetTorrentStatus()
        {
            using (IDelugeRpc deluge = await daemon.OpenRpcAsync())
            {
                await deluge.LoginAsync(user.Username, user.Password);
                var torrentId = await AddMagnetAsync(deluge);
                try
                {
                    IDictionary<string, object> status = await deluge.GetTorrentStatusAsync(torrentId, new[] { BuiltInStatuses.Name, BuiltInStatuses.ActiveTime });
                    
                    Assert.IsNotNull(status, "status != null");
                    Assert.AreEqual(2, status.Count, "status.Count == 2");
                    Assert.IsTrue(status.ContainsKey(BuiltInStatuses.Name), "status.ContainsKey(BuiltInStatuses.Name)");
                    Assert.IsTrue(status.ContainsKey(BuiltInStatuses.ActiveTime), "status.ContainsKey(BuiltInStatuses.ActiveTime)");
                }
                finally
                {
                    await deluge.RemoveTorrentAsync(torrentId, true);
                }
            }
        }
    }
}
