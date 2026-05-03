using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class MobArmourEquipmentPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MobArmourEquipment;

        public ulong EntityRuntimeId { get; set; }
        public ItemInstance Helmet { get; set; } = new ItemInstance();
        public ItemInstance Chestplate { get; set; } = new ItemInstance();
        public ItemInstance Leggings { get; set; } = new ItemInstance();
        public ItemInstance Boots { get; set; } = new ItemInstance();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            Helmet.Encode(stream);
            Chestplate.Encode(stream);
            Leggings.Encode(stream);
            Boots.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            Helmet.Decode(stream);
            Chestplate.Decode(stream);
            Leggings.Decode(stream);
            Boots.Decode(stream);
        }
    }
}
