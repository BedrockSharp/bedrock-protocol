using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class MoveActorDeltaPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MoveActorDelta;

        public ulong EntityRuntimeId { get; set; }
        public ushort Flags { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteUnsignedShort(Flags);

            if ((Flags & 0x01) != 0) stream.WriteFloat(Position.X);
            if ((Flags & 0x02) != 0) stream.WriteFloat(Position.Y);
            if ((Flags & 0x04) != 0) stream.WriteFloat(Position.Z);
            
            if ((Flags & 0x08) != 0) stream.WriteByte((byte)(Rotation.X * 256.0f / 360.0f));
            if ((Flags & 0x10) != 0) stream.WriteByte((byte)(Rotation.Y * 256.0f / 360.0f));
            if ((Flags & 0x20) != 0) stream.WriteByte((byte)(Rotation.Z * 256.0f / 360.0f));
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            Flags = stream.ReadUnsignedShort();

            float px = 0, py = 0, pz = 0;
            if ((Flags & 0x01) != 0) px = stream.ReadFloat();
            if ((Flags & 0x02) != 0) py = stream.ReadFloat();
            if ((Flags & 0x04) != 0) pz = stream.ReadFloat();
            Position = new Vector3(px, py, pz);

            float rx = 0, ry = 0, rz = 0;
            if ((Flags & 0x08) != 0) rx = stream.ReadByte() * (360.0f / 256.0f);
            if ((Flags & 0x10) != 0) ry = stream.ReadByte() * (360.0f / 256.0f);
            if ((Flags & 0x20) != 0) rz = stream.ReadByte() * (360.0f / 256.0f);
            Rotation = new Vector3(rx, ry, rz);
        }
    }
}
