using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class PlayerAuthInputPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerAuthInput;

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float MoveVectorX { get; set; }
        public float MoveVectorZ { get; set; }
        public float HeadYaw { get; set; }
        public ulong InputFlags { get; set; }
        public int InputMode { get; set; }
        public int PlayMode { get; set; }
        public int InteractionModel { get; set; }
        public ulong GazeDirectionX { get; set; }
        public ulong GazeDirectionY { get; set; }
        public ulong GazeDirectionZ { get; set; }
        public ulong Tick { get; set; }
        public ulong Delta { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            stream.WriteFloat(PositionX);
            stream.WriteFloat(PositionY);
            stream.WriteFloat(PositionZ);
            stream.WriteFloat(MoveVectorX);
            stream.WriteFloat(MoveVectorZ);
            stream.WriteFloat(HeadYaw);
            stream.WriteUnsignedVarLong(InputFlags);
            stream.WriteUnsignedVarInt((uint)InputMode);
            stream.WriteUnsignedVarInt((uint)PlayMode);
            stream.WriteVarInt(InteractionModel);
            stream.WriteUnsignedVarLong(GazeDirectionX);
            stream.WriteUnsignedVarLong(GazeDirectionY);
            stream.WriteUnsignedVarLong(GazeDirectionZ);
            stream.WriteUnsignedVarLong(Tick);
            stream.WriteUnsignedVarLong(Delta);
        }

        public override void Decode(BinaryStream stream)
        {
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            PositionX = stream.ReadFloat();
            PositionY = stream.ReadFloat();
            PositionZ = stream.ReadFloat();
            MoveVectorX = stream.ReadFloat();
            MoveVectorZ = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
            InputFlags = stream.ReadUnsignedVarLong();
            InputMode = (int)stream.ReadUnsignedVarInt();
            PlayMode = (int)stream.ReadUnsignedVarInt();
            InteractionModel = stream.ReadVarInt();
            GazeDirectionX = stream.ReadUnsignedVarLong();
            GazeDirectionY = stream.ReadUnsignedVarLong();
            GazeDirectionZ = stream.ReadUnsignedVarLong();
            Tick = stream.ReadUnsignedVarLong();
            Delta = stream.ReadUnsignedVarLong();
        }
    }
}
