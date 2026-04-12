using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ContainerOpenPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ContainerOpen;

        public byte WindowId { get; set; }
        public WindowType WindowType { get; set; }
        public int X { get; set; }
        public uint Y { get; set; }
        public int Z { get; set; }
        public long EntityId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(WindowId);
            stream.WriteByte((byte)WindowType);
            stream.WriteVarInt(X);
            stream.WriteUnsignedVarInt(Y);
            stream.WriteVarInt(Z);
            stream.WriteVarLong(EntityId);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadByte();
            WindowType = (WindowType)stream.ReadByte();
            X = stream.ReadVarInt();
            Y = stream.ReadUnsignedVarInt();
            Z = stream.ReadVarInt();
            EntityId = stream.ReadVarLong();
        }
    }
}
