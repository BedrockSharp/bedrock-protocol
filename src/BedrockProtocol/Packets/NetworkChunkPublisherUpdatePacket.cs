using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class NetworkChunkPublisherUpdatePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.NetworkChunkPublisherUpdate;

        public BlockPosition Position { get; set; }
        public int Radius { get; set; }

        public override void Encode(BinaryStream stream)
        {
            Position.Encode(stream);
            stream.WriteUnsignedVarInt((uint)Radius);
            stream.WriteInt(0); // Saved chunks
        }

        public override void Decode(BinaryStream stream)
        {
            Position = BlockPosition.Decode(stream);
            Radius = (int)stream.ReadUnsignedVarInt();
        }
    }
}
