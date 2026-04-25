using BedrockProtocol.Utils;
using System;

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
        public ulong InputFlags2 { get; set; }
        public uint InputMode { get; set; }
        public uint PlayMode { get; set; }
        public uint InteractionModel { get; set; }
        public float InteractRotationX { get; set; }
        public float InteractRotationY { get; set; }
        public ulong Tick { get; set; }
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
        public float DeltaZ { get; set; }

        public override void Encode(BinaryStream stream)
        {
            throw new NotImplementedException();
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
            InputFlags2 = stream.ReadUnsignedVarLong();

            InputMode = stream.ReadUnsignedVarInt();
            PlayMode = stream.ReadUnsignedVarInt();
            InteractionModel = stream.ReadUnsignedVarInt();
            
            InteractRotationX = stream.ReadFloat();
            InteractRotationY = stream.ReadFloat();
            
            Tick = stream.ReadUnsignedVarLong();
            
            DeltaX = stream.ReadFloat();
            DeltaY = stream.ReadFloat();
            DeltaZ = stream.ReadFloat();
            
            // TODO: Implement remaining optional fields after this based on InputFlags (Item Interactions, Block Actions, etc.)
        }
    }
}
