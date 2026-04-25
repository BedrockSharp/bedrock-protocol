using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class MovePlayerPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.MovePlayer;

        public ulong RuntimeEntityId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float HeadYaw { get; set; }

        public MoveMode Mode { get; set; }
        public bool OnGround { get; set; }
        public ulong RidingEntityId { get; set; }

        public int TeleportationCause { get; set; } = 0;
        public int EntityType { get; set; } = 0;

        public ulong Tick { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(RuntimeEntityId);

            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);

            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            stream.WriteFloat(HeadYaw);

            stream.WriteByte((byte)Mode);
            stream.WriteBool(OnGround);
            stream.WriteUnsignedVarLong(RidingEntityId);

            if (Mode == MoveMode.Teleport)
            {
                stream.WriteInt(TeleportationCause);
                stream.WriteInt(EntityType);
            }

            stream.WriteUnsignedVarLong(Tick);
        }

        public override void Decode(BinaryStream stream)
        {
            RuntimeEntityId = stream.ReadUnsignedVarLong();

            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();

            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();

            Mode = (MoveMode)stream.ReadByte();
            OnGround = stream.ReadBool();
            RidingEntityId = stream.ReadUnsignedVarLong();

            if (Mode == MoveMode.Teleport)
            {
                TeleportationCause = stream.ReadInt();
                EntityType = stream.ReadInt();
            }

            Tick = stream.ReadUnsignedVarLong();
        }
    }
}