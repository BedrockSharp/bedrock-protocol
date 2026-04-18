using BedrockProtocol.Utils;
using System;

namespace BedrockProtocol.Packets
{
    public class ResourcePackChunkDataPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePackChunkData;

        public Guid PackId { get; set; }
        public string Version { get; set; } = string.Empty;
        public int ChunkIndex { get; set; }
        public long Progress { get; set; }
        public byte[] Data { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(PackId.ToString());
            stream.WriteString(Version);
            stream.WriteIntLE(ChunkIndex);
            stream.WriteLongLE(Progress);
            stream.WriteByteArray(Data);
        }

        public override void Decode(BinaryStream stream)
        {
            PackId = Guid.Parse(stream.ReadString());
            Version = stream.ReadString();
            ChunkIndex = stream.ReadIntLE();
            Progress = stream.ReadLongLE();
            Data = stream.ReadByteArray();
        }
    }
}
