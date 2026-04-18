using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class LevelChunkPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.LevelChunk;

        public int ChunkX { get; set; }
        public int ChunkZ { get; set; }
        public int Dimension { get; set; }
        public int SubChunkCount { get; set; }
        public bool CacheEnabled { get; set; }
        public bool RequestSubChunks { get; set; }
        public int SubChunkLimit { get; set; }
        public long[] BlobIds { get; set; } = new long[0];
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(ChunkX);
            stream.WriteVarInt(ChunkZ);
            stream.WriteVarInt(Dimension);

            if (!RequestSubChunks)
            {
                stream.WriteUnsignedVarInt((uint)SubChunkCount);
            }
            else if (SubChunkLimit < 0)
            {
                stream.WriteUnsignedVarInt(uint.MaxValue); // -1
            }
            else
            {
                stream.WriteUnsignedVarInt(uint.MaxValue - 1); // -2
                stream.WriteUnsignedVarInt((uint)SubChunkLimit);
            }

            stream.WriteBool(CacheEnabled);
            if (CacheEnabled)
            {
                stream.WriteUnsignedVarInt((uint)BlobIds.Length);
                foreach (long blobId in BlobIds)
                {
                    stream.WriteLongLE(blobId);
                }
            }

            stream.WriteByteArray(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            ChunkX = stream.ReadVarInt();
            ChunkZ = stream.ReadVarInt();
            Dimension = stream.ReadVarInt();

            uint subChunkIndicator = stream.ReadUnsignedVarInt();
            if (subChunkIndicator == uint.MaxValue) // -1
            {
                RequestSubChunks = true;
                SubChunkLimit = -1;
            }
            else if (subChunkIndicator == uint.MaxValue - 1) // -2
            {
                RequestSubChunks = true;
                SubChunkLimit = (int)stream.ReadUnsignedVarInt();
            }
            else
            {
                RequestSubChunks = false;
                SubChunkCount = (int)subChunkIndicator;
            }

            CacheEnabled = stream.ReadBool();
            if (CacheEnabled)
            {
                uint count = stream.ReadUnsignedVarInt();
                BlobIds = new long[count];
                for (int i = 0; i < count; i++)
                {
                    BlobIds[i] = stream.ReadLongLE();
                }
            }

            Payload = stream.ReadByteArray();
        }
    }
}
