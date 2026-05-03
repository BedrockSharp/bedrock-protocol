using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class MobEquipmentPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MobEquipment;

        public ulong EntityRuntimeId { get; set; }
        public ItemInstance Item { get; set; } = new ItemInstance();
        public byte InventorySlot { get; set; }
        public byte HotBarSlot { get; set; }
        public byte WindowId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            Item.Encode(stream);
            stream.WriteByte(InventorySlot);
            stream.WriteByte(HotBarSlot);
            stream.WriteByte(WindowId);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            Item.Decode(stream);
            InventorySlot = stream.ReadByte();
            HotBarSlot = stream.ReadByte();
            WindowId = stream.ReadByte();
        }
    }
}
