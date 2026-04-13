using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class UpdateAttributesPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UpdateAttributes;

        public ulong EntityId { get; set; }
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityId);
            stream.WriteUnsignedVarInt((uint)Payload.Length);
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityId = stream.ReadUnsignedVarLong();
            uint len = stream.ReadUnsignedVarInt();
            Payload = stream.ReadBytes((int)len);
        }
    }
}
