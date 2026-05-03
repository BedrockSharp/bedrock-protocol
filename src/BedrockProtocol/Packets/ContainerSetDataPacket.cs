using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ContainerSetDataPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ContainerSetData;

        public byte WindowId { get; set; }
        public int Property { get; set; }
        public int Value { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(WindowId);
            stream.WriteVarInt(Property);
            stream.WriteVarInt(Value);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadByte();
            Property = stream.ReadVarInt();
            Value = stream.ReadVarInt();
        }
    }
}
