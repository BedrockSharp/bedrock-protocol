using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class MoveEntityAbsolutePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MoveEntityAbsolute;

        public ulong RuntimeEntityId { get; set; }
        public byte Flags { get; set; }
        public Vector3 Position { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float HeadYaw { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteByte(Flags);
            stream.WriteVector3(Position);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            stream.WriteFloat(HeadYaw);
        }

        public override void Decode(BinaryStream stream)
        {
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            Flags = stream.ReadByte();
            Position = stream.ReadVector3();
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
        }
    }
}
