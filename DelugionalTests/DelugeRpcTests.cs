using Delugional;
using Delugional.Daemon;
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
        private static DelugeDaemon daemon;
        private static Deluge deluge;
        private static string magnet1Hash = ParseHashFromMagnetLink(Resources.MagnetLink1);
        private static string magnet2Hash = ParseHashFromMagnetLink(Resources.MagnetLink2);

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext testContext)
        {
            daemon = DelugeDaemon.Default;
            if (!daemon.Running)
                daemon.Start();

            deluge = await daemon.OpenRpcAsync();
            var user = AuthFile.OpenDefault().First();
            await deluge.LoginAsync(user.Username, user.Password);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            daemon.Stop();
            deluge.Dispose();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var existingTorrents = await deluge.GetTorrentsStatusAsync();
            if (existingTorrents.ContainsKey(magnet1Hash))
                await deluge.RemoveTorrentAsync(magnet1Hash);
            if (existingTorrents.ContainsKey(magnet2Hash))
                await deluge.RemoveTorrentAsync(magnet2Hash);
        }

        [TestMethod]
        public async Task AddTorrentFile()
        {
            string torrentId = await deluge.AddTorrentAsync("torrent_file", await File.ReadAllBytesAsync("ubuntu-24.04.4-desktop-amd64.iso.torrent"));

            await deluge.RemoveTorrentAsync(torrentId);

            Assert.IsNotNull(torrentId, "torrentId != null");

        }

        private static string ParseHashFromMagnetLink(string magnetLink)
        {
            const string HashPrefix = "urn:btih:";
            int hashStartIndex = magnetLink.IndexOf(HashPrefix) + HashPrefix.Length;
            int hashEndIndex = magnetLink.IndexOf('&', hashStartIndex);
            if (hashEndIndex == -1)
                hashEndIndex = magnetLink.Length;
            return magnetLink.Substring(hashStartIndex, hashEndIndex - hashStartIndex);
        }

        private async Task<string> AddMagnetAsync()
        {
            return await AddMagnetAsync(Resources.MagnetLink1);
        }

        private async Task<string> AddMagnetAsync(string link)
        {
            var existingTorrents = await deluge.GetTorrentsStatusAsync(new Filter { Keywords = { ParseHashFromMagnetLink(link) } });
            if (existingTorrents.Count > 0)
            {
                // Torrent already exists, return its ID.
                return existingTorrents.Keys.First();
            }
            return await deluge.AddMagnetAsync(link);
        }

        private static async Task<bool> WaitForPausedStateAsync(string torrentId, bool expectedPaused, int attempts = 10, int delayMilliseconds = 500)
        {
            for (int i = 0; i < attempts; i++)
            {
                var status = await deluge.GetTorrentStatusAsync(torrentId, [BuiltInStatuses.State]);
                if (expectedPaused && status.State == TorrentState.Paused)
                    return true;
                if (!expectedPaused && status.State != TorrentState.Paused)
                    return true;


                await Task.Delay(delayMilliseconds);
            }

            return false;
        }

        [TestMethod]
        public async Task PauseResumeSession()
        {
            var torrentId = await AddMagnetAsync(Resources.MagnetLink1);
            var torrentId2 = await AddMagnetAsync(Resources.MagnetLink2);
            await deluge.PauseSessionAsync();
            Assert.IsTrue(await WaitForPausedStateAsync(torrentId, true), "Torrent should be paused.");
            await deluge.ResumeSessionAsync();
            Assert.IsTrue(await WaitForPausedStateAsync(torrentId, false), "Torrent should be resumed.");
        }

        [TestMethod]
        public async Task PauseResumeTorrent()
        {
            var torrentId = await AddMagnetAsync();
            await deluge.PauseTorrentAsync([torrentId]);
            Assert.IsTrue(await WaitForPausedStateAsync(torrentId, true), "Torrent should be paused.");
            await deluge.ResumeTorrentAsync([torrentId]);
            Assert.IsTrue(await WaitForPausedStateAsync(torrentId, false), "Torrent should be resumed.");

        }

        [TestMethod]
        public async Task RemoveTorrents()
        {
            var torrentId = await AddMagnetAsync();
            await deluge.RemoveTorrentsAsync([torrentId], true);
        }

        [TestMethod]
        public async Task GetTorrentStatus()
        {
            var torrentId = await AddMagnetAsync();
            var status = await deluge.GetTorrentStatusAsync(torrentId, []);

            Assert.IsNotNull(status, "status != null");
            Assert.AreEqual("ubuntu-15.10-desktop-amd64.iso", status.Name);
        }

        [TestMethod]
        public async Task GetTorrentsStatus()
        {
            var torrentId = await AddMagnetAsync();
            var statuses = await deluge.GetTorrentsStatusAsync(new Filter { Ids = new HashSet<string> { torrentId } }, [BuiltInStatuses.Name]);
            Assert.IsNotNull(statuses, "statuses != null");
            Assert.AreEqual(1, statuses.Count, "statuses.Count == 1");
            Assert.IsTrue(statuses.ContainsKey(torrentId), $"statuses.ContainsKey({torrentId})");
            Assert.IsNotNull(statuses[torrentId], $"statuses[{torrentId}] != null");
            Assert.AreEqual("ubuntu-15.10-desktop-amd64.iso", statuses[torrentId].Name);
        }

        [TestMethod]
        public async Task GetMethodList()
        {
            string[] methodList = await deluge.GetMethodListAsync();
            Assert.IsNotNull(methodList, "methodList != null");
            Assert.IsTrue(methodList.Length > 0, "methodList.Length > 0");
        }

        [TestMethod]
        public async Task GetSessionStatus()
        {
            await AddMagnetAsync();
            var sessionStatus = await deluge.GetSessionStatusAsync([]);
            
            // When passing empty array, Deluge returns all statistics
            Assert.IsNotNull(sessionStatus, "sessionStatus != null");
            Assert.IsTrue(sessionStatus.Raw.Count > 0, "Should contain session statistics");
            
            // Verify we can access specific statistics
            Assert.IsTrue(sessionStatus.Ses.NumDownloadingTorrents >= 0);
            Assert.IsTrue(sessionStatus.Peer.NumPeersConnected >= 0);
        }
    }
}
