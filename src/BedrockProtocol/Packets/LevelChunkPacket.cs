using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class LevelChunkPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.LevelChunk;

        public int ChunkX { get; set; }
        public int ChunkZ { get; set; }
        public int SubChunkCount { get; set; }
        public bool CacheEnabled { get; set; }
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(ChunkX);
            stream.WriteVarInt(ChunkZ);
            stream.WriteUnsignedVarInt((uint)SubChunkCount);
            stream.WriteBool(CacheEnabled);
            stream.WriteUnsignedVarInt((uint)Payload.Length);
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            ChunkX = stream.ReadVarInt();
            ChunkZ = stream.ReadVarInt();
            SubChunkCount = (int)stream.ReadUnsignedVarInt();
            CacheEnabled = stream.ReadBool();
            uint len = stream.ReadUnsignedVarInt();
            Payload = stream.ReadBytes((int)len);
        }
    }
}
