using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class RequestChunkRadiusPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.RequestChunkRadius;

        public int Radius { get; set; }
        public int MaxRadius { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(Radius);
            stream.WriteByte((byte)MaxRadius);
        }

        public override void Decode(BinaryStream stream)
        {
            Radius = stream.ReadVarInt();
            MaxRadius = stream.ReadByte();
        }
    }
}
