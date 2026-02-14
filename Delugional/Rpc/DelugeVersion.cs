namespace Delugional.Rpc
{
    public enum DelugeVersion
    {
        None, // Unknown. Need detection.
        V1, // Deluge 1.3 and earlier.
        V2, // Deluge 2.0 without protocol version number in RPC messages.
        V2_1, // Deluge 2.1 and later with protocol version number in RPC messages.
    }
}
