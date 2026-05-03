using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class CompletedUsingItemPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CompletedUsingItem;

        public short UsedItemId { get; set; }
        public int UseMethod { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteShortLE(UsedItemId);
            stream.WriteVarInt(UseMethod);
        }

        public override void Decode(BinaryStream stream)
        {
            UsedItemId = stream.ReadShortLE();
            UseMethod = stream.ReadVarInt();
        }
    }
}
