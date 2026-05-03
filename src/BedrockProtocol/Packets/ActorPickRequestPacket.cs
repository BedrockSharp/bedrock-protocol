using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ActorPickRequestPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ActorPickRequest;

        public long EntityUniqueId { get; set; }
        public byte MaxRelatedItems { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityUniqueId);
            stream.WriteByte(MaxRelatedItems);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityUniqueId = stream.ReadVarLong();
            MaxRelatedItems = stream.ReadByte();
        }
    }
}
