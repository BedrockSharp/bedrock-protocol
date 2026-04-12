using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class MoveEntityAbsolutePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MoveEntityAbsolute;

        public ulong RuntimeEntityId { get; set; }
        public byte Flags { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float HeadYaw { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteByte(Flags);
            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            stream.WriteFloat(HeadYaw);
        }

        public override void Decode(BinaryStream stream)
        {
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            Flags = stream.ReadByte();
            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
        }
    }
}
