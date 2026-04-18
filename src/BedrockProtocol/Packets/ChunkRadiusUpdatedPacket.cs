using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ChunkRadiusUpdatedPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ChunkRadiusUpdated;

        public int Radius { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(Radius);
        }

        public override void Decode(BinaryStream stream)
        {
            Radius = stream.ReadVarInt();
        }
    }
}
