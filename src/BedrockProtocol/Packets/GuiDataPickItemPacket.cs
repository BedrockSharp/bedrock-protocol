using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class GuiDataPickItemPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.GUIDataPickItem;

        public string ItemName { get; set; } = string.Empty;
        public string ItemEffects { get; set; } = string.Empty;
        public int HotBarSlot { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(ItemName);
            stream.WriteString(ItemEffects);
            stream.WriteIntLE(HotBarSlot);
        }

        public override void Decode(BinaryStream stream)
        {
            ItemName = stream.ReadString();
            ItemEffects = stream.ReadString();
            HotBarSlot = stream.ReadIntLE();
        }
    }
}
