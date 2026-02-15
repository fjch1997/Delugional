using System;
using System.Collections.Generic;
using System.Linq;

namespace Delugional
{
    /// <summary>
    /// Represents the session status information from Deluge.
    /// AI-generated class based on the structure of the session status response.
    /// </summary>
    public class SessionStatus
    {
        private readonly Dictionary<string, object> value;

        internal SessionStatus(object rencodeResult)
        {
            value = ((Dictionary<object, object>)rencodeResult).ToDictionary(i => i.Key.ToString(), i => i.Value);
        }

        public PeerStatistics Peer => new PeerStatistics(value);

        public NetworkStatistics Net => new NetworkStatistics(value);

        public SessionStatistics Ses => new SessionStatistics(value);

        public DiskStatistics Disk => new DiskStatistics(value);

        public DhtStatistics Dht => new DhtStatistics(value);

        public TrackerStatistics Tracker => new TrackerStatistics(value);

        public UtpStatistics Utp => new UtpStatistics(value);

        /// <summary>
        /// Gets the raw dictionary value for accessing additional or custom fields.
        /// </summary>
        public IReadOnlyDictionary<string, object> Raw => value;
    }

    /// <summary>
    /// Peer-related session statistics.
    /// </summary>
    public class PeerStatistics
    {
        private readonly Dictionary<string, object> value;

        internal PeerStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;

        // Error counters
        public int ErrorPeers => GetInt("peer.error_peers");
        public int DisconnectedPeers => GetInt("peer.disconnected_peers");
        public int EofPeers => GetInt("peer.eof_peers");
        public int ConnResetPeers => GetInt("peer.connreset_peers");
        public int ConnRefusedPeers => GetInt("peer.connrefused_peers");
        public int ConnAbortedPeers => GetInt("peer.connaborted_peers");
        public int NotConnectedPeers => GetInt("peer.notconnected_peers");
        public int PermPeers => GetInt("peer.perm_peers");
        public int BufferPeers => GetInt("peer.buffer_peers");
        public int UnreachablePeers => GetInt("peer.unreachable_peers");
        public int BrokenPipePeers => GetInt("peer.broken_pipe_peers");
        public int AddrInUsePeers => GetInt("peer.addrinuse_peers");
        public int NoAccessPeers => GetInt("peer.no_access_peers");
        public int InvalidArgPeers => GetInt("peer.invalid_arg_peers");
        public int AbortedPeers => GetInt("peer.aborted_peers");

        // Request counters
        public int PieceRequests => GetInt("peer.piece_requests");
        public int MaxPieceRequests => GetInt("peer.max_piece_requests");
        public int InvalidPieceRequests => GetInt("peer.invalid_piece_requests");
        public int ChokedPieceRequests => GetInt("peer.choked_piece_requests");
        public int CancelledPieceRequests => GetInt("peer.cancelled_piece_requests");
        public int PieceRejects => GetInt("peer.piece_rejects");

        // Error type counters
        public int ErrorIncomingPeers => GetInt("peer.error_incoming_peers");
        public int ErrorOutgoingPeers => GetInt("peer.error_outgoing_peers");
        public int ErrorRc4Peers => GetInt("peer.error_rc4_peers");
        public int ErrorEncryptedPeers => GetInt("peer.error_encrypted_peers");
        public int ErrorTcpPeers => GetInt("peer.error_tcp_peers");
        public int ErrorUtpPeers => GetInt("peer.error_utp_peers");

        // Connection counters
        public int ConnectTimeouts => GetInt("peer.connect_timeouts");
        public int UninterestingPeers => GetInt("peer.uninteresting_peers");
        public int TimeoutPeers => GetInt("peer.timeout_peers");
        public int NoMemoryPeers => GetInt("peer.no_memory_peers");
        public int TooManyPeers => GetInt("peer.too_many_peers");
        public int TransportTimeoutPeers => GetInt("peer.transport_timeout_peers");
        public int NumBannedPeers => GetInt("peer.num_banned_peers");
        public int BannedForHashFailure => GetInt("peer.banned_for_hash_failure");

        // Connection attempts
        public int ConnectionAttempts => GetInt("peer.connection_attempts");
        public int ConnectionAttemptLoops => GetInt("peer.connection_attempt_loops");
        public int BoostConnectionAttempts => GetInt("peer.boost_connection_attempts");
        public int MissedConnectionAttempts => GetInt("peer.missed_connection_attempts");
        public int NoPeerConnectionAttempts => GetInt("peer.no_peer_connection_attempts");
        public int IncomingConnections => GetInt("peer.incoming_connections");

        // Peer type counters
        public int NumTcpPeers => GetInt("peer.num_tcp_peers");
        public int NumSocks5Peers => GetInt("peer.num_socks5_peers");
        public int NumHttpProxyPeers => GetInt("peer.num_http_proxy_peers");
        public int NumUtpPeers => GetInt("peer.num_utp_peers");
        public int NumI2pPeers => GetInt("peer.num_i2p_peers");
        public int NumSslPeers => GetInt("peer.num_ssl_peers");
        public int NumSslSocks5Peers => GetInt("peer.num_ssl_socks5_peers");
        public int NumSslHttpProxyPeers => GetInt("peer.num_ssl_http_proxy_peers");
        public int NumSslUtpPeers => GetInt("peer.num_ssl_utp_peers");

        // Peer state counters
        public int NumPeersHalfOpen => GetInt("peer.num_peers_half_open");
        public int NumPeersConnected => GetInt("peer.num_peers_connected");
        public int NumPeersUpInterested => GetInt("peer.num_peers_up_interested");
        public int NumPeersDownInterested => GetInt("peer.num_peers_down_interested");
        public int NumPeersUpUnchokedAll => GetInt("peer.num_peers_up_unchoked_all");
        public int NumPeersUpUnchokedOptimistic => GetInt("peer.num_peers_up_unchoked_optimistic");
        public int NumPeersUpUnchoked => GetInt("peer.num_peers_up_unchoked");
        public int NumPeersDownUnchoked => GetInt("peer.num_peers_down_unchoked");
        public int NumPeersUpRequests => GetInt("peer.num_peers_up_requests");
        public int NumPeersDownRequests => GetInt("peer.num_peers_down_requests");
        public int NumPeersEndGame => GetInt("peer.num_peers_end_game");
        public int NumPeersUpDisk => GetInt("peer.num_peers_up_disk");
        public int NumPeersDownDisk => GetInt("peer.num_peers_down_disk");
    }

    /// <summary>
    /// Network-related session statistics.
    /// </summary>
    public class NetworkStatistics
    {
        private readonly Dictionary<string, object> value;

        internal NetworkStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;
        private long GetLong(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt64(v) : 0L;
        private bool GetBool(string key) => value.TryGetValue(key, out var v) && Convert.ToBoolean(v);

        // Event counters
        public int OnReadCounter => GetInt("net.on_read_counter");
        public int OnWriteCounter => GetInt("net.on_write_counter");
        public int OnTickCounter => GetInt("net.on_tick_counter");
        public int OnLsdCounter => GetInt("net.on_lsd_counter");
        public int OnLsdPeerCounter => GetInt("net.on_lsd_peer_counter");
        public int OnUdpCounter => GetInt("net.on_udp_counter");
        public int OnAcceptCounter => GetInt("net.on_accept_counter");
        public int OnDiskQueueCounter => GetInt("net.on_disk_queue_counter");
        public int OnDiskCounter => GetInt("net.on_disk_counter");

        // Sent bytes
        public long SentPayloadBytes => GetLong("net.sent_payload_bytes");
        public long SentBytes => GetLong("net.sent_bytes");
        public long SentIpOverheadBytes => GetLong("net.sent_ip_overhead_bytes");
        public long SentTrackerBytes => GetLong("net.sent_tracker_bytes");

        // Received bytes
        public long RecvPayloadBytes => GetLong("net.recv_payload_bytes");
        public long RecvBytes => GetLong("net.recv_bytes");
        public long RecvIpOverheadBytes => GetLong("net.recv_ip_overhead_bytes");
        public long RecvTrackerBytes => GetLong("net.recv_tracker_bytes");
        public long RecvFailedBytes => GetLong("net.recv_failed_bytes");
        public long RecvRedundantBytes => GetLong("net.recv_redundant_bytes");

        // Limiter stats
        public int LimiterUpQueue => GetInt("net.limiter_up_queue");
        public int LimiterDownQueue => GetInt("net.limiter_down_queue");
        public long LimiterUpBytes => GetLong("net.limiter_up_bytes");
        public long LimiterDownBytes => GetLong("net.limiter_down_bytes");

        // Connection status
        public bool HasIncomingConnections => GetBool("net.has_incoming_connections");
    }

    /// <summary>
    /// Session-related statistics.
    /// </summary>
    public class SessionStatistics
    {
        private readonly Dictionary<string, object> value;

        internal SessionStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;

        // Torrent state counters
        public int NumCheckingTorrents => GetInt("ses.num_checking_torrents");
        public int NumStoppedTorrents => GetInt("ses.num_stopped_torrents");
        public int NumUploadOnlyTorrents => GetInt("ses.num_upload_only_torrents");
        public int NumDownloadingTorrents => GetInt("ses.num_downloading_torrents");
        public int NumSeedingTorrents => GetInt("ses.num_seeding_torrents");
        public int NumQueuedSeedingTorrents => GetInt("ses.num_queued_seeding_torrents");
        public int NumQueuedDownloadTorrents => GetInt("ses.num_queued_download_torrents");
        public int NumErrorTorrents => GetInt("ses.num_error_torrents");
        public int NonFilterTorrents => GetInt("ses.non_filter_torrents");

        // Piece statistics
        public int NumPiecePassed => GetInt("ses.num_piece_passed");
        public int NumPieceFailed => GetInt("ses.num_piece_failed");
        public int NumHavePieces => GetInt("ses.num_have_pieces");
        public int NumTotalPiecesAdded => GetInt("ses.num_total_pieces_added");
    }

    /// <summary>
    /// Disk I/O statistics.
    /// </summary>
    public class DiskStatistics
    {
        private readonly Dictionary<string, object> value;

        internal DiskStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;
        private long GetLong(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt64(v) : 0L;

        // Disk I/O counters
        public long DiskBlocksInUse => GetLong("disk.disk_blocks_in_use");
        public int BlocksWritten => GetInt("disk.blocks_written");
        public int BlocksRead => GetInt("disk.blocks_read");
        public int BlocksReadHit => GetInt("disk.blocks_read_hit");
        public long WritesCached => GetLong("disk.writes_cached");
        public long ReadsCached => GetLong("disk.reads_cached");
        public long QueuedWriteBytes => GetLong("disk.queued_write_bytes");
        public int QueuedJobs => GetInt("disk.queued_jobs");
        public int PeakQueuedJobs => GetInt("disk.peak_queued_jobs");
        public int PendingReadingJobs => GetInt("disk.pending_reading_jobs");
        public int RunningThreads => GetInt("disk.running_threads");
        public int FencedReadJobs => GetInt("disk.fenced_read_jobs");
        public int FencedWriteJobs => GetInt("disk.fenced_write_jobs");
        public int BlockedJobs => GetInt("disk.blocked_jobs");
        public int NumWritingThreads => GetInt("disk.num_writing_threads");
        public int NumRunningThreads => GetInt("disk.num_running_threads");
    }

    /// <summary>
    /// DHT (Distributed Hash Table) statistics.
    /// </summary>
    public class DhtStatistics
    {
        private readonly Dictionary<string, object> value;

        internal DhtStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;
        private long GetLong(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt64(v) : 0L;

        public int DhtNodes => GetInt("dht.dht_nodes");
        public int DhtNodeCache => GetInt("dht.dht_node_cache");
        public int DhtTorrents => GetInt("dht.dht_torrents");
        public long DhtGlobalNodes => GetLong("dht.dht_global_nodes");
        public int DhtTotalAllocations => GetInt("dht.dht_total_allocations");
    }

    /// <summary>
    /// Tracker-related statistics.
    /// </summary>
    public class TrackerStatistics
    {
        private readonly Dictionary<string, object> value;

        internal TrackerStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;

        public int TrackerRequestTimeouts => GetInt("ses.num_incoming_tracker");
    }

    /// <summary>
    /// UTP (Micro Transport Protocol) statistics.
    /// </summary>
    public class UtpStatistics
    {
        private readonly Dictionary<string, object> value;

        internal UtpStatistics(Dictionary<string, object> value)
        {
            this.value = value;
        }

        private int GetInt(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt32(v) : 0;
        private long GetLong(string key) => value.TryGetValue(key, out var v) ? Convert.ToInt64(v) : 0L;

        public int UtpPacketLoss => GetInt("utp.utp_packet_loss");
        public int UtpTimeout => GetInt("utp.utp_timeout");
        public int UtpPacketsIn => GetInt("utp.utp_packets_in");
        public int UtpPacketsOut => GetInt("utp.utp_packets_out");
        public long UtpFastRetransmit => GetLong("utp.utp_fast_retransmit");
        public long UtpPacketResend => GetLong("utp.utp_packet_resend");
        public long UtpSamplesAboveTarget => GetLong("utp.utp_samples_above_target");
        public long UtpSamplesBelowTarget => GetLong("utp.utp_samples_below_target");
        public long UtpPayloadPktsIn => GetLong("utp.utp_payload_pkts_in");
        public long UtpPayloadPktsOut => GetLong("utp.utp_payload_pkts_out");
        public long UtpInvalidPktsIn => GetLong("utp.utp_invalid_pkts_in");
        public long UtpRedundantPktsIn => GetLong("utp.utp_redundant_pkts_in");
    }
}