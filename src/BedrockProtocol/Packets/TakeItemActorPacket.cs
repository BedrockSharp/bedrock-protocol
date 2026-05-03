using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class TakeItemActorPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.TakeItemActor;

        public ulong ItemEntityRuntimeId { get; set; }
        public ulong TakerEntityRuntimeId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(ItemEntityRuntimeId);
            stream.WriteUnsignedVarLong(TakerEntityRuntimeId);
        }

        public override void Decode(BinaryStream stream)
        {
            ItemEntityRuntimeId = stream.ReadUnsignedVarLong();
            TakerEntityRuntimeId = stream.ReadUnsignedVarLong();
        }
    }
}
