namespace BedrockProtocol.Packets.Enums
{
    public enum CompressionAlgorithm : ushort
    {
        Zlib = 0,
        Snappy = 1,
        None = 255
    }
}
