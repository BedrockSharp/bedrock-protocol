using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class InventorySlotPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.InventorySlot;

        public uint WindowId { get; set; }
        public uint Slot { get; set; }
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(WindowId);
            stream.WriteUnsignedVarInt(Slot);
            stream.WriteUnsignedVarInt((uint)Payload.Length);
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadUnsignedVarInt();
            Slot = stream.ReadUnsignedVarInt();
            uint len = stream.ReadUnsignedVarInt();
            Payload = stream.ReadBytes((int)len);
        }
    }
}
