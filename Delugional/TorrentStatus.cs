using System;
using System.Collections.Generic;
using System.Linq;

namespace Delugional
{
    public class TorrentStatus
    {
        private Dictionary<string, object> value;
        internal TorrentStatus(object rencodeResult)
        {
            value = ((Dictionary<object, object>)rencodeResult)?.ToDictionary(s => (string)s.Key, s => s.Value);
        }

        private int GetInt(string key) => Convert.ToInt32(value[key]);
        private long GetLong(string key) => Convert.ToInt64(value[key]);
        private double GetDouble(string key) => Convert.ToDouble(value[key]);
        private bool GetBool(string key) => Convert.ToBoolean(value[key]);
        private string GetString(string key) => value[key] as string;
        private TimeSpan GetTimeSpan(string key)
        {
            var seconds = GetInt(key);
            return seconds >= 0 ? TimeSpan.FromSeconds(seconds) : TimeSpan.Zero;
        }
        private DateTimeOffset GetDateTimeOffset(string key) => DateTimeOffset.FromUnixTimeSeconds(GetLong(key));
        private object GetObject(string key) => value[key];
        private T[] GetArray<T>(string key)
        {
            var array = value[key] as object[];
            if (array == null) return Array.Empty<T>();

            if (typeof(T) == typeof(int))
                return array.Select(x => Convert.ToInt32(x) as object).Cast<T>().ToArray();
            if (typeof(T) == typeof(double))
                return array.Select(x => Convert.ToDouble(x) as object).Cast<T>().ToArray();
            if (typeof(T) == typeof(bool))
                return array.Select(x => Convert.ToBoolean(x) as object).Cast<T>().ToArray();

            throw new InvalidCastException($"Unsupported array type: {typeof(T).Name}");
        }

        public TimeSpan ActiveTime => GetTimeSpan("active_time");
        public TimeSpan SeedingTime => GetTimeSpan("seeding_time");
        public TimeSpan FinishedTime => GetTimeSpan("finished_time");
        public long AllTimeDownload => GetLong("all_time_download");
        public string StorageMode => GetString("storage_mode");
        public double DistributedCopies => GetDouble("distributed_copies");
        public long DownloadPayloadRate => GetLong("download_payload_rate");
        public int[] FilePriorities => GetArray<int>("file_priorities");
        public string Hash => GetString("hash");
        public bool AutoManaged => GetBool("auto_managed");
        public bool IsAutoManaged => GetBool("is_auto_managed");
        public bool IsFinished => GetBool("is_finished");
        public int MaxConnections => GetInt("max_connections");
        public int MaxDownloadSpeed => GetInt("max_download_speed");
        public int MaxUploadSlots => GetInt("max_upload_slots");
        public int MaxUploadSpeed => GetInt("max_upload_speed");
        public string Message => GetString("message");
        public string MoveCompletedPath => GetString("move_completed_path");
        public bool MoveCompleted => GetBool("move_completed");
        public TimeSpan NextAnnounce => GetTimeSpan("next_announce");
        public int NumPeers => GetInt("num_peers");
        public int NumSeeds => GetInt("num_seeds");
        public string Owner => GetString("owner");
        public bool Paused => GetBool("paused");
        public bool PrioritizeFirstLast => GetBool("prioritize_first_last");
        public bool PrioritizeFirstLastPieces => GetBool("prioritize_first_last_pieces");
        public bool SequentialDownload => GetBool("sequential_download");
        public double Progress => GetDouble("progress");
        public bool Shared => GetBool("shared");
        public bool RemoveAtRatio => GetBool("remove_at_ratio");
        public string SavePath => GetString("save_path");
        public string DownloadLocation => GetString("download_location");
        public double SeedsPeersRatio => GetDouble("seeds_peers_ratio");
        public int SeedRank => GetInt("seed_rank");
        public TorrentState State => (TorrentState)Enum.Parse(typeof(TorrentState), GetString("state"), true);
        public bool StopAtRatio => GetBool("stop_at_ratio");
        public double StopRatio => GetDouble("stop_ratio");
        public DateTimeOffset TimeAdded => GetDateTimeOffset("time_added");
        public long TotalDone => GetLong("total_done");
        public long TotalPayloadDownload => GetLong("total_payload_download");
        public long TotalPayloadUpload => GetLong("total_payload_upload");
        public int TotalPeers => GetInt("total_peers");
        public int TotalSeeds => GetInt("total_seeds");
        public long TotalUploaded => GetLong("total_uploaded");
        public long TotalWanted => GetLong("total_wanted");
        public long TotalRemaining => GetLong("total_remaining");
        public string Tracker => GetString("tracker");
        public string TrackerHost => GetString("tracker_host");
        public TorrentTracker[] Trackers => (GetObject("trackers") as object[])?.Select(t => new TorrentTracker(t)).ToArray() ?? Array.Empty<TorrentTracker>();
        public string TrackerStatus => GetString("tracker_status");
        public long UploadPayloadRate => GetLong("upload_payload_rate");
        public string Comment => GetString("comment");
        public string Creator => GetString("creator");
        public int NumFiles => GetInt("num_files");
        public int NumPieces => GetInt("num_pieces");
        public int PieceLength => GetInt("piece_length");
        public bool Private => GetBool("private");
        public long TotalSize => GetLong("total_size");
        public TimeSpan Eta => GetTimeSpan("eta");
        public double[] FileProgress => GetArray<double>("file_progress");
        public TorrentFile[] Files => (GetObject("files") as object[])?.Select(f => new TorrentFile(f)).ToArray() ?? Array.Empty<TorrentFile>();
        public TorrentFile[] OrigFiles => (GetObject("orig_files") as object[])?.Select(f => new TorrentFile(f)).ToArray() ?? Array.Empty<TorrentFile>();
        public bool IsSeed => GetBool("is_seed");
        public TorrentPeer[] Peers => (GetObject("peers") as object[])?.Select(p => new TorrentPeer(p)).ToArray() ?? Array.Empty<TorrentPeer>();
        public int Queue => GetInt("queue");
        public double Ratio => GetDouble("ratio");
        public DateTimeOffset? CompletedTime => GetDateTimeOffset("completed_time");
        public DateTimeOffset LastSeenComplete => GetDateTimeOffset("last_seen_complete");
        public string Name => GetString("name");
        public bool[] Pieces => GetArray<bool>("pieces");
        public bool SeedMode => GetBool("seed_mode");
        public bool SuperSeeding => GetBool("super_seeding");
        public TimeSpan TimeSinceDownload => GetTimeSpan("time_since_download");
        public TimeSpan TimeSinceUpload => GetTimeSpan("time_since_upload");
        public TimeSpan TimeSinceTransfer => GetTimeSpan("time_since_transfer");
    }
}
