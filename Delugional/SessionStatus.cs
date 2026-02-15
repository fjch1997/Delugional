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

        internal static Dictionary<string, string> PropertyNameMappings { get; } = new Dictionary<string, string>
        {
            // Peer Statistics - Error counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorPeers)}", "peer.error_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.DisconnectedPeers)}", "peer.disconnected_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.EofPeers)}", "peer.eof_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnResetPeers)}", "peer.connreset_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnRefusedPeers)}", "peer.connrefused_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnAbortedPeers)}", "peer.connaborted_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NotConnectedPeers)}", "peer.notconnected_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.PermPeers)}", "peer.perm_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.BufferPeers)}", "peer.buffer_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.UnreachablePeers)}", "peer.unreachable_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.BrokenPipePeers)}", "peer.broken_pipe_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.AddrInUsePeers)}", "peer.addrinuse_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NoAccessPeers)}", "peer.no_access_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.InvalidArgPeers)}", "peer.invalid_arg_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.AbortedPeers)}", "peer.aborted_peers" },
            
            // Peer Statistics - Request counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.PieceRequests)}", "peer.piece_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.MaxPieceRequests)}", "peer.max_piece_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.InvalidPieceRequests)}", "peer.invalid_piece_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ChokedPieceRequests)}", "peer.choked_piece_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.CancelledPieceRequests)}", "peer.cancelled_piece_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.PieceRejects)}", "peer.piece_rejects" },
            
            // Peer Statistics - Error type counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorIncomingPeers)}", "peer.error_incoming_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorOutgoingPeers)}", "peer.error_outgoing_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorRc4Peers)}", "peer.error_rc4_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorEncryptedPeers)}", "peer.error_encrypted_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorTcpPeers)}", "peer.error_tcp_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ErrorUtpPeers)}", "peer.error_utp_peers" },
            
            // Peer Statistics - Connection counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnectTimeouts)}", "peer.connect_timeouts" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.UninterestingPeers)}", "peer.uninteresting_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.TimeoutPeers)}", "peer.timeout_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NoMemoryPeers)}", "peer.no_memory_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.TooManyPeers)}", "peer.too_many_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.TransportTimeoutPeers)}", "peer.transport_timeout_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumBannedPeers)}", "peer.num_banned_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.BannedForHashFailure)}", "peer.banned_for_hash_failure" },
            
            // Peer Statistics - Connection attempts
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnectionAttempts)}", "peer.connection_attempts" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.ConnectionAttemptLoops)}", "peer.connection_attempt_loops" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.BoostConnectionAttempts)}", "peer.boost_connection_attempts" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.MissedConnectionAttempts)}", "peer.missed_connection_attempts" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NoPeerConnectionAttempts)}", "peer.no_peer_connection_attempts" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.IncomingConnections)}", "peer.incoming_connections" },
            
            // Peer Statistics - Peer type counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumTcpPeers)}", "peer.num_tcp_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumSocks5Peers)}", "peer.num_socks5_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumHttpProxyPeers)}", "peer.num_http_proxy_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumUtpPeers)}", "peer.num_utp_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumI2pPeers)}", "peer.num_i2p_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumSslPeers)}", "peer.num_ssl_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumSslSocks5Peers)}", "peer.num_ssl_socks5_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumSslHttpProxyPeers)}", "peer.num_ssl_http_proxy_peers" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumSslUtpPeers)}", "peer.num_ssl_utp_peers" },
            
            // Peer Statistics - Peer state counters
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersHalfOpen)}", "peer.num_peers_half_open" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersConnected)}", "peer.num_peers_connected" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpInterested)}", "peer.num_peers_up_interested" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersDownInterested)}", "peer.num_peers_down_interested" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpUnchokedAll)}", "peer.num_peers_up_unchoked_all" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpUnchokedOptimistic)}", "peer.num_peers_up_unchoked_optimistic" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpUnchoked)}", "peer.num_peers_up_unchoked" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersDownUnchoked)}", "peer.num_peers_down_unchoked" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpRequests)}", "peer.num_peers_up_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersDownRequests)}", "peer.num_peers_down_requests" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersEndGame)}", "peer.num_peers_end_game" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersUpDisk)}", "peer.num_peers_up_disk" },
            { $"{nameof(Peer)}.{nameof(PeerStatistics.NumPeersDownDisk)}", "peer.num_peers_down_disk" },
            
            // Network Statistics - Event counters
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnReadCounter)}", "net.on_read_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnWriteCounter)}", "net.on_write_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnTickCounter)}", "net.on_tick_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnLsdCounter)}", "net.on_lsd_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnLsdPeerCounter)}", "net.on_lsd_peer_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnUdpCounter)}", "net.on_udp_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnAcceptCounter)}", "net.on_accept_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnDiskQueueCounter)}", "net.on_disk_queue_counter" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.OnDiskCounter)}", "net.on_disk_counter" },
            
            // Network Statistics - Sent bytes
            { $"{nameof(Net)}.{nameof(NetworkStatistics.SentPayloadBytes)}", "net.sent_payload_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.SentBytes)}", "net.sent_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.SentIpOverheadBytes)}", "net.sent_ip_overhead_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.SentTrackerBytes)}", "net.sent_tracker_bytes" },
            
            // Network Statistics - Received bytes
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvPayloadBytes)}", "net.recv_payload_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvBytes)}", "net.recv_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvIpOverheadBytes)}", "net.recv_ip_overhead_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvTrackerBytes)}", "net.recv_tracker_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvFailedBytes)}", "net.recv_failed_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.RecvRedundantBytes)}", "net.recv_redundant_bytes" },
            
            // Network Statistics - Limiter stats
            { $"{nameof(Net)}.{nameof(NetworkStatistics.LimiterUpQueue)}", "net.limiter_up_queue" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.LimiterDownQueue)}", "net.limiter_down_queue" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.LimiterUpBytes)}", "net.limiter_up_bytes" },
            { $"{nameof(Net)}.{nameof(NetworkStatistics.LimiterDownBytes)}", "net.limiter_down_bytes" },
            
            // Network Statistics - Connection status
            { $"{nameof(Net)}.{nameof(NetworkStatistics.HasIncomingConnections)}", "net.has_incoming_connections" },
            
            // Session Statistics - Torrent state counters
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumCheckingTorrents)}", "ses.num_checking_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumStoppedTorrents)}", "ses.num_stopped_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumUploadOnlyTorrents)}", "ses.num_upload_only_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumDownloadingTorrents)}", "ses.num_downloading_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumSeedingTorrents)}", "ses.num_seeding_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumQueuedSeedingTorrents)}", "ses.num_queued_seeding_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumQueuedDownloadTorrents)}", "ses.num_queued_download_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumErrorTorrents)}", "ses.num_error_torrents" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NonFilterTorrents)}", "ses.non_filter_torrents" },
            
            // Session Statistics - Piece statistics
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumPiecePassed)}", "ses.num_piece_passed" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumPieceFailed)}", "ses.num_piece_failed" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumHavePieces)}", "ses.num_have_pieces" },
            { $"{nameof(Ses)}.{nameof(SessionStatistics.NumTotalPiecesAdded)}", "ses.num_total_pieces_added" },
            
            // Disk Statistics
            { $"{nameof(Disk)}.{nameof(DiskStatistics.DiskBlocksInUse)}", "disk.disk_blocks_in_use" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.BlocksWritten)}", "disk.blocks_written" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.BlocksRead)}", "disk.blocks_read" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.BlocksReadHit)}", "disk.blocks_read_hit" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.WritesCached)}", "disk.writes_cached" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.ReadsCached)}", "disk.reads_cached" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.QueuedWriteBytes)}", "disk.queued_write_bytes" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.QueuedJobs)}", "disk.queued_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.PeakQueuedJobs)}", "disk.peak_queued_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.PendingReadingJobs)}", "disk.pending_reading_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.RunningThreads)}", "disk.running_threads" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.FencedReadJobs)}", "disk.fenced_read_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.FencedWriteJobs)}", "disk.fenced_write_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.BlockedJobs)}", "disk.blocked_jobs" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.NumWritingThreads)}", "disk.num_writing_threads" },
            { $"{nameof(Disk)}.{nameof(DiskStatistics.NumRunningThreads)}", "disk.num_running_threads" },
            
            // DHT Statistics
            { $"{nameof(Dht)}.{nameof(DhtStatistics.DhtNodes)}", "dht.dht_nodes" },
            { $"{nameof(Dht)}.{nameof(DhtStatistics.DhtNodeCache)}", "dht.dht_node_cache" },
            { $"{nameof(Dht)}.{nameof(DhtStatistics.DhtTorrents)}", "dht.dht_torrents" },
            { $"{nameof(Dht)}.{nameof(DhtStatistics.DhtGlobalNodes)}", "dht.dht_global_nodes" },
            { $"{nameof(Dht)}.{nameof(DhtStatistics.DhtTotalAllocations)}", "dht.dht_total_allocations" },
            
            // Tracker Statistics
            { $"{nameof(Tracker)}.{nameof(TrackerStatistics.TrackerRequestTimeouts)}", "ses.num_incoming_tracker" },
            
            // UTP Statistics
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPacketLoss)}", "utp.utp_packet_loss" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpTimeout)}", "utp.utp_timeout" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPacketsIn)}", "utp.utp_packets_in" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPacketsOut)}", "utp.utp_packets_out" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpFastRetransmit)}", "utp.utp_fast_retransmit" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPacketResend)}", "utp.utp_packet_resend" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpSamplesAboveTarget)}", "utp.utp_samples_above_target" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpSamplesBelowTarget)}", "utp.utp_samples_below_target" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPayloadPktsIn)}", "utp.utp_payload_pkts_in" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpPayloadPktsOut)}", "utp.utp_payload_pkts_out" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpInvalidPktsIn)}", "utp.utp_invalid_pkts_in" },
            { $"{nameof(Utp)}.{nameof(UtpStatistics.UtpRedundantPktsIn)}", "utp.utp_redundant_pkts_in" }
        };

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
        public int ErrorPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorPeers)}"]);
        public int DisconnectedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(DisconnectedPeers)}"]);
        public int EofPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(EofPeers)}"]);
        public int ConnResetPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnResetPeers)}"]);
        public int ConnRefusedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnRefusedPeers)}"]);
        public int ConnAbortedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnAbortedPeers)}"]);
        public int NotConnectedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NotConnectedPeers)}"]);
        public int PermPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(PermPeers)}"]);
        public int BufferPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(BufferPeers)}"]);
        public int UnreachablePeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(UnreachablePeers)}"]);
        public int BrokenPipePeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(BrokenPipePeers)}"]);
        public int AddrInUsePeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(AddrInUsePeers)}"]);
        public int NoAccessPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NoAccessPeers)}"]);
        public int InvalidArgPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(InvalidArgPeers)}"]);
        public int AbortedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(AbortedPeers)}"]);

        // Request counters
        public int PieceRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(PieceRequests)}"]);
        public int MaxPieceRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(MaxPieceRequests)}"]);
        public int InvalidPieceRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(InvalidPieceRequests)}"]);
        public int ChokedPieceRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ChokedPieceRequests)}"]);
        public int CancelledPieceRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(CancelledPieceRequests)}"]);
        public int PieceRejects => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(PieceRejects)}"]);

        // Error type counters
        public int ErrorIncomingPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorIncomingPeers)}"]);
        public int ErrorOutgoingPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorOutgoingPeers)}"]);
        public int ErrorRc4Peers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorRc4Peers)}"]);
        public int ErrorEncryptedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorEncryptedPeers)}"]);
        public int ErrorTcpPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorTcpPeers)}"]);
        public int ErrorUtpPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ErrorUtpPeers)}"]);

        // Connection counters
        public int ConnectTimeouts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnectTimeouts)}"]);
        public int UninterestingPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(UninterestingPeers)}"]);
        public int TimeoutPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(TimeoutPeers)}"]);
        public int NoMemoryPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NoMemoryPeers)}"]);
        public int TooManyPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(TooManyPeers)}"]);
        public int TransportTimeoutPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(TransportTimeoutPeers)}"]);
        public int NumBannedPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumBannedPeers)}"]);
        public int BannedForHashFailure => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(BannedForHashFailure)}"]);

        // Connection attempts
        public int ConnectionAttempts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnectionAttempts)}"]);
        public int ConnectionAttemptLoops => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(ConnectionAttemptLoops)}"]);
        public int BoostConnectionAttempts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(BoostConnectionAttempts)}"]);
        public int MissedConnectionAttempts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(MissedConnectionAttempts)}"]);
        public int NoPeerConnectionAttempts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NoPeerConnectionAttempts)}"]);
        public int IncomingConnections => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(IncomingConnections)}"]);

        // Peer type counters
        public int NumTcpPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumTcpPeers)}"]);
        public int NumSocks5Peers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumSocks5Peers)}"]);
        public int NumHttpProxyPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumHttpProxyPeers)}"]);
        public int NumUtpPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumUtpPeers)}"]);
        public int NumI2pPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumI2pPeers)}"]);
        public int NumSslPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumSslPeers)}"]);
        public int NumSslSocks5Peers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumSslSocks5Peers)}"]);
        public int NumSslHttpProxyPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumSslHttpProxyPeers)}"]);
        public int NumSslUtpPeers => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumSslUtpPeers)}"]);

        // Peer state counters
        public int NumPeersHalfOpen => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersHalfOpen)}"]);
        public int NumPeersConnected => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersConnected)}"]);
        public int NumPeersUpInterested => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpInterested)}"]);
        public int NumPeersDownInterested => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersDownInterested)}"]);
        public int NumPeersUpUnchokedAll => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpUnchokedAll)}"]);
        public int NumPeersUpUnchokedOptimistic => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpUnchokedOptimistic)}"]);
        public int NumPeersUpUnchoked => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpUnchoked)}"]);
        public int NumPeersDownUnchoked => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersDownUnchoked)}"]);
        public int NumPeersUpRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpRequests)}"]);
        public int NumPeersDownRequests => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersDownRequests)}"]);
        public int NumPeersEndGame => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersEndGame)}"]);
        public int NumPeersUpDisk => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersUpDisk)}"]);
        public int NumPeersDownDisk => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Peer)}.{nameof(NumPeersDownDisk)}"]);
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
        public int OnReadCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnReadCounter)}"]);
        public int OnWriteCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnWriteCounter)}"]);
        public int OnTickCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnTickCounter)}"]);
        public int OnLsdCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnLsdCounter)}"]);
        public int OnLsdPeerCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnLsdPeerCounter)}"]);
        public int OnUdpCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnUdpCounter)}"]);
        public int OnAcceptCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnAcceptCounter)}"]);
        public int OnDiskQueueCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnDiskQueueCounter)}"]);
        public int OnDiskCounter => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(OnDiskCounter)}"]);

        // Sent bytes
        public long SentPayloadBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(SentPayloadBytes)}"]);
        public long SentBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(SentBytes)}"]);
        public long SentIpOverheadBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(SentIpOverheadBytes)}"]);
        public long SentTrackerBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(SentTrackerBytes)}"]);

        // Received bytes
        public long RecvPayloadBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvPayloadBytes)}"]);
        public long RecvBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvBytes)}"]);
        public long RecvIpOverheadBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvIpOverheadBytes)}"]);
        public long RecvTrackerBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvTrackerBytes)}"]);
        public long RecvFailedBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvFailedBytes)}"]);
        public long RecvRedundantBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(RecvRedundantBytes)}"]);

        // Limiter stats
        public int LimiterUpQueue => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(LimiterUpQueue)}"]);
        public int LimiterDownQueue => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(LimiterDownQueue)}"]);
        public long LimiterUpBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(LimiterUpBytes)}"]);
        public long LimiterDownBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(LimiterDownBytes)}"]);

        // Connection status
        public bool HasIncomingConnections => GetBool(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Net)}.{nameof(HasIncomingConnections)}"]);
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
        public int NumCheckingTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumCheckingTorrents)}"]);
        public int NumStoppedTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumStoppedTorrents)}"]);
        public int NumUploadOnlyTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumUploadOnlyTorrents)}"]);
        public int NumDownloadingTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumDownloadingTorrents)}"]);
        public int NumSeedingTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumSeedingTorrents)}"]);
        public int NumQueuedSeedingTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumQueuedSeedingTorrents)}"]);
        public int NumQueuedDownloadTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumQueuedDownloadTorrents)}"]);
        public int NumErrorTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumErrorTorrents)}"]);
        public int NonFilterTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NonFilterTorrents)}"]);

        // Piece statistics
        public int NumPiecePassed => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumPiecePassed)}"]);
        public int NumPieceFailed => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumPieceFailed)}"]);
        public int NumHavePieces => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumHavePieces)}"]);
        public int NumTotalPiecesAdded => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Ses)}.{nameof(NumTotalPiecesAdded)}"]);
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
        public long DiskBlocksInUse => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(DiskBlocksInUse)}"]);
        public int BlocksWritten => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(BlocksWritten)}"]);
        public int BlocksRead => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(BlocksRead)}"]);
        public int BlocksReadHit => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(BlocksReadHit)}"]);
        public long WritesCached => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(WritesCached)}"]);
        public long ReadsCached => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(ReadsCached)}"]);
        public long QueuedWriteBytes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(QueuedWriteBytes)}"]);
        public int QueuedJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(QueuedJobs)}"]);
        public int PeakQueuedJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(PeakQueuedJobs)}"]);
        public int PendingReadingJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(PendingReadingJobs)}"]);
        public int RunningThreads => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(RunningThreads)}"]);
        public int FencedReadJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(FencedReadJobs)}"]);
        public int FencedWriteJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(FencedWriteJobs)}"]);
        public int BlockedJobs => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(BlockedJobs)}"]);
        public int NumWritingThreads => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(NumWritingThreads)}"]);
        public int NumRunningThreads => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Disk)}.{nameof(NumRunningThreads)}"]);
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

        public int DhtNodes => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Dht)}.{nameof(DhtNodes)}"]);
        public int DhtNodeCache => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Dht)}.{nameof(DhtNodeCache)}"]);
        public int DhtTorrents => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Dht)}.{nameof(DhtTorrents)}"]);
        public long DhtGlobalNodes => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Dht)}.{nameof(DhtGlobalNodes)}"]);
        public int DhtTotalAllocations => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Dht)}.{nameof(DhtTotalAllocations)}"]);
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

        public int TrackerRequestTimeouts => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Tracker)}.{nameof(TrackerRequestTimeouts)}"]);
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

        public int UtpPacketLoss => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPacketLoss)}"]);
        public int UtpTimeout => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpTimeout)}"]);
        public int UtpPacketsIn => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPacketsIn)}"]);
        public int UtpPacketsOut => GetInt(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPacketsOut)}"]);
        public long UtpFastRetransmit => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpFastRetransmit)}"]);
        public long UtpPacketResend => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPacketResend)}"]);
        public long UtpSamplesAboveTarget => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpSamplesAboveTarget)}"]);
        public long UtpSamplesBelowTarget => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpSamplesBelowTarget)}"]);
        public long UtpPayloadPktsIn => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPayloadPktsIn)}"]);
        public long UtpPayloadPktsOut => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpPayloadPktsOut)}"]);
        public long UtpInvalidPktsIn => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpInvalidPktsIn)}"]);
        public long UtpRedundantPktsIn => GetLong(SessionStatus.PropertyNameMappings[$"{nameof(SessionStatus.Utp)}.{nameof(UtpRedundantPktsIn)}"]);
    }
}