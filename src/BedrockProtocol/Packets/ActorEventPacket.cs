using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ActorEventPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ActorEvent;

        public ulong EntityRuntimeId { get; set; }
        public byte EventType { get; set; }
        public int EventData { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteByte(EventType);
            stream.WriteVarInt(EventData);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            EventType = stream.ReadByte();
            EventData = stream.ReadVarInt();
        }
    }
}
