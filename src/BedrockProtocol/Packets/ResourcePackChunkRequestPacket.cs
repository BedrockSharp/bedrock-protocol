using BedrockProtocol.Utils;
using System;

namespace BedrockProtocol.Packets
{
    public class ResourcePackChunkRequestPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePackChunkRequest;

        public Guid PackId { get; set; }
        public string Version { get; set; } = string.Empty;
        public int ChunkIndex { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(PackId.ToString());
            stream.WriteString(Version);
            stream.WriteIntLE(ChunkIndex);
        }

        public override void Decode(BinaryStream stream)
        {
            PackId = Guid.Parse(stream.ReadString());
            Version = stream.ReadString();
            ChunkIndex = stream.ReadIntLE();
        }
    }
}
