using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class UpdateEquipPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UpdateEquip;

        public byte WindowId { get; set; }
        public byte WindowType { get; set; }
        public int Size { get; set; }
        public long EntityUniqueId { get; set; }
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(WindowId);
            stream.WriteByte(WindowType);
            stream.WriteVarInt(Size);
            stream.WriteVarLong(EntityUniqueId);
            stream.WriteUnsignedVarInt((uint)Payload.Length);
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadByte();
            WindowType = stream.ReadByte();
            Size = stream.ReadVarInt();
            EntityUniqueId = stream.ReadVarLong();
            uint len = stream.ReadUnsignedVarInt();
            Payload = stream.ReadBytes((int)len);
        }
    }
}
