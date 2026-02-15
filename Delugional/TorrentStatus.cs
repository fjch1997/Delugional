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
        internal static Dictionary<string, string> PropertyNameMappings { get; } = new Dictionary<string, string>
        {
            { nameof(ActiveTime), "active_time" },
            { nameof(SeedingTime), "seeding_time" },
            { nameof(FinishedTime), "finished_time" },
            { nameof(AllTimeDownload), "all_time_download" },
            { nameof(StorageMode), "storage_mode" },
            { nameof(DistributedCopies), "distributed_copies" },
            { nameof(DownloadPayloadRate), "download_payload_rate" },
            { nameof(FilePriorities), "file_priorities" },
            { nameof(Hash), "hash" },
            { nameof(AutoManaged), "auto_managed" },
            { nameof(IsAutoManaged), "is_auto_managed" },
            { nameof(IsFinished), "is_finished" },
            { nameof(MaxConnections), "max_connections" },
            { nameof(MaxDownloadSpeed), "max_download_speed" },
            { nameof(MaxUploadSlots), "max_upload_slots" },
            { nameof(MaxUploadSpeed), "max_upload_speed" },
            { nameof(Message), "message" },
            { nameof(MoveCompletedPath), "move_completed_path" },
            { nameof(MoveCompleted), "move_completed" },
            { nameof(NextAnnounce), "next_announce" },
            { nameof(NumPeers), "num_peers" },
            { nameof(NumSeeds), "num_seeds" },
            { nameof(Owner), "owner" },
            { nameof(Paused), "paused" },
            { nameof(PrioritizeFirstLast), "prioritize_first_last" },
            { nameof(PrioritizeFirstLastPieces), "prioritize_first_last_pieces" },
            { nameof(SequentialDownload), "sequential_download" },
            { nameof(Progress), "progress" },
            { nameof(Shared), "shared" },
            { nameof(RemoveAtRatio), "remove_at_ratio" },
            { nameof(SavePath), "save_path" },
            { nameof(DownloadLocation), "download_location" },
            { nameof(SeedsPeersRatio), "seeds_peers_ratio" },
            { nameof(SeedRank), "seed_rank" },
            { nameof(State), "state" },
            { nameof(StopAtRatio), "stop_at_ratio" },
            { nameof(StopRatio), "stop_ratio" },
            { nameof(TimeAdded), "time_added" },
            { nameof(TotalDone), "total_done" },
            { nameof(TotalPayloadDownload), "total_payload_download" },
            { nameof(TotalPayloadUpload), "total_payload_upload" },
            { nameof(TotalPeers), "total_peers" },
            { nameof(TotalSeeds), "total_seeds" },
            { nameof(TotalUploaded), "total_uploaded" },
            { nameof(TotalWanted), "total_wanted" },
            { nameof(TotalRemaining), "total_remaining" },
            { nameof(Tracker), "tracker" },
            { nameof(TrackerHost), "tracker_host" },
            { nameof(Trackers), "trackers" },
            { nameof(TrackerStatus), "tracker_status" },
            { nameof(UploadPayloadRate), "upload_payload_rate" },
            { nameof(Comment), "comment" },
            { nameof(Creator), "creator" },
            { nameof(NumFiles), "num_files" },
            { nameof(NumPieces), "num_pieces" },
            { nameof(PieceLength), "piece_length" },
            { nameof(Private), "private" },
            { nameof(TotalSize), "total_size" },
            { nameof(Eta), "eta" },
            { nameof(FileProgress), "file_progress" },
            { nameof(Files), "files" },
            { nameof(OrigFiles), "orig_files" },
            { nameof(IsSeed), "is_seed" },
            { nameof(Peers), "peers" },
            { nameof(Queue), "queue" },
            { nameof(Ratio), "ratio" },
            { nameof(CompletedTime), "completed_time" },
            { nameof(LastSeenComplete), "last_seen_complete" },
            { nameof(Name), "name" },
            { nameof(Pieces), "pieces" },
            { nameof(SeedMode), "seed_mode" },
            { nameof(SuperSeeding), "super_seeding" },
            { nameof(TimeSinceDownload), "time_since_download" },
            { nameof(TimeSinceUpload), "time_since_upload" },
            { nameof(TimeSinceTransfer), "time_since_transfer" }
        };
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

        public TimeSpan ActiveTime => GetTimeSpan(PropertyNameMappings[nameof(ActiveTime)]);
        public TimeSpan SeedingTime => GetTimeSpan(PropertyNameMappings[nameof(SeedingTime)]);
        public TimeSpan FinishedTime => GetTimeSpan(PropertyNameMappings[nameof(FinishedTime)]);
        public long AllTimeDownload => GetLong(PropertyNameMappings[nameof(AllTimeDownload)]);
        public string StorageMode => GetString(PropertyNameMappings[nameof(StorageMode)]);
        public double DistributedCopies => GetDouble(PropertyNameMappings[nameof(DistributedCopies)]);
        public long DownloadPayloadRate => GetLong(PropertyNameMappings[nameof(DownloadPayloadRate)]);
        public int[] FilePriorities => GetArray<int>(PropertyNameMappings[nameof(FilePriorities)]);
        public string Hash => GetString(PropertyNameMappings[nameof(Hash)]);
        public bool AutoManaged => GetBool(PropertyNameMappings[nameof(AutoManaged)]);
        public bool IsAutoManaged => GetBool(PropertyNameMappings[nameof(IsAutoManaged)]);
        public bool IsFinished => GetBool(PropertyNameMappings[nameof(IsFinished)]);
        public int MaxConnections => GetInt(PropertyNameMappings[nameof(MaxConnections)]);
        public int MaxDownloadSpeed => GetInt(PropertyNameMappings[nameof(MaxDownloadSpeed)]);
        public int MaxUploadSlots => GetInt(PropertyNameMappings[nameof(MaxUploadSlots)]);
        public int MaxUploadSpeed => GetInt(PropertyNameMappings[nameof(MaxUploadSpeed)]);
        public string Message => GetString(PropertyNameMappings[nameof(Message)]);
        public string MoveCompletedPath => GetString(PropertyNameMappings[nameof(MoveCompletedPath)]);
        public bool MoveCompleted => GetBool(PropertyNameMappings[nameof(MoveCompleted)]);
        public TimeSpan NextAnnounce => GetTimeSpan(PropertyNameMappings[nameof(NextAnnounce)]);
        public int NumPeers => GetInt(PropertyNameMappings[nameof(NumPeers)]);
        public int NumSeeds => GetInt(PropertyNameMappings[nameof(NumSeeds)]);
        public string Owner => GetString(PropertyNameMappings[nameof(Owner)]);
        public bool Paused => GetBool(PropertyNameMappings[nameof(Paused)]);
        public bool PrioritizeFirstLast => GetBool(PropertyNameMappings[nameof(PrioritizeFirstLast)]);
        public bool PrioritizeFirstLastPieces => GetBool(PropertyNameMappings[nameof(PrioritizeFirstLastPieces)]);
        public bool SequentialDownload => GetBool(PropertyNameMappings[nameof(SequentialDownload)]);
        public double Progress => GetDouble(PropertyNameMappings[nameof(Progress)]);
        public bool Shared => GetBool(PropertyNameMappings[nameof(Shared)]);
        public bool RemoveAtRatio => GetBool(PropertyNameMappings[nameof(RemoveAtRatio)]);
        public string SavePath => GetString(PropertyNameMappings[nameof(SavePath)]);
        public string DownloadLocation => GetString(PropertyNameMappings[nameof(DownloadLocation)]);
        public double SeedsPeersRatio => GetDouble(PropertyNameMappings[nameof(SeedsPeersRatio)]);
        public int SeedRank => GetInt(PropertyNameMappings[nameof(SeedRank)]);
        public TorrentState State => (TorrentState)Enum.Parse(typeof(TorrentState), GetString(PropertyNameMappings[nameof(State)]), true);
        public bool StopAtRatio => GetBool(PropertyNameMappings[nameof(StopAtRatio)]);
        public double StopRatio => GetDouble(PropertyNameMappings[nameof(StopRatio)]);
        public DateTimeOffset TimeAdded => GetDateTimeOffset(PropertyNameMappings[nameof(TimeAdded)]);
        public long TotalDone => GetLong(PropertyNameMappings[nameof(TotalDone)]);
        public long TotalPayloadDownload => GetLong(PropertyNameMappings[nameof(TotalPayloadDownload)]);
        public long TotalPayloadUpload => GetLong(PropertyNameMappings[nameof(TotalPayloadUpload)]);
        public int TotalPeers => GetInt(PropertyNameMappings[nameof(TotalPeers)]);
        public int TotalSeeds => GetInt(PropertyNameMappings[nameof(TotalSeeds)]);
        public long TotalUploaded => GetLong(PropertyNameMappings[nameof(TotalUploaded)]);
        public long TotalWanted => GetLong(PropertyNameMappings[nameof(TotalWanted)]);
        public long TotalRemaining => GetLong(PropertyNameMappings[nameof(TotalRemaining)]);
        public string Tracker => GetString(PropertyNameMappings[nameof(Tracker)]);
        public string TrackerHost => GetString(PropertyNameMappings[nameof(TrackerHost)]);
        public TorrentTracker[] Trackers => (GetObject(PropertyNameMappings[nameof(Trackers)]) as object[])?.Select(t => new TorrentTracker(t)).ToArray() ?? Array.Empty<TorrentTracker>();
        public string TrackerStatus => GetString(PropertyNameMappings[nameof(TrackerStatus)]);
        public long UploadPayloadRate => GetLong(PropertyNameMappings[nameof(UploadPayloadRate)]);
        public string Comment => GetString(PropertyNameMappings[nameof(Comment)]);
        public string Creator => GetString(PropertyNameMappings[nameof(Creator)]);
        public int NumFiles => GetInt(PropertyNameMappings[nameof(NumFiles)]);
        public int NumPieces => GetInt(PropertyNameMappings[nameof(NumPieces)]);
        public int PieceLength => GetInt(PropertyNameMappings[nameof(PieceLength)]);
        public bool Private => GetBool(PropertyNameMappings[nameof(Private)]);
        public long TotalSize => GetLong(PropertyNameMappings[nameof(TotalSize)]);
        public TimeSpan Eta => GetTimeSpan(PropertyNameMappings[nameof(Eta)]);
        public double[] FileProgress => GetArray<double>(PropertyNameMappings[nameof(FileProgress)]);
        public TorrentFile[] Files => (GetObject(PropertyNameMappings[nameof(Files)]) as object[])?.Select(f => new TorrentFile(f)).ToArray() ?? Array.Empty<TorrentFile>();
        public TorrentFile[] OrigFiles => (GetObject(PropertyNameMappings[nameof(OrigFiles)]) as object[])?.Select(f => new TorrentFile(f)).ToArray() ?? Array.Empty<TorrentFile>();
        public bool IsSeed => GetBool(PropertyNameMappings[nameof(IsSeed)]);
        public TorrentPeer[] Peers => (GetObject(PropertyNameMappings[nameof(Peers)]) as object[])?.Select(p => new TorrentPeer(p)).ToArray() ?? Array.Empty<TorrentPeer>();
        public int Queue => GetInt(PropertyNameMappings[nameof(Queue)]);
        public double Ratio => GetDouble(PropertyNameMappings[nameof(Ratio)]);
        public DateTimeOffset? CompletedTime => GetDateTimeOffset(PropertyNameMappings[nameof(CompletedTime)]);
        public DateTimeOffset LastSeenComplete => GetDateTimeOffset(PropertyNameMappings[nameof(LastSeenComplete)]);
        public string Name => GetString(PropertyNameMappings[nameof(Name)]);
        public bool[] Pieces => GetArray<bool>(PropertyNameMappings[nameof(Pieces)]);
        public bool SeedMode => GetBool(PropertyNameMappings[nameof(SeedMode)]);
        public bool SuperSeeding => GetBool(PropertyNameMappings[nameof(SuperSeeding)]);
        public TimeSpan TimeSinceDownload => GetTimeSpan(PropertyNameMappings[nameof(TimeSinceDownload)]);
        public TimeSpan TimeSinceUpload => GetTimeSpan(PropertyNameMappings[nameof(TimeSinceUpload)]);
        public TimeSpan TimeSinceTransfer => GetTimeSpan(PropertyNameMappings[nameof(TimeSinceTransfer)]);
    }
}
