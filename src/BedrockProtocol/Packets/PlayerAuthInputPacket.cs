using BedrockProtocol.Utils;
using BedrockProtocol.Types;
using System;

namespace BedrockProtocol.Packets
{
    public class PlayerAuthInputPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerAuthInput;

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public Vector3 Position { get; set; }
        public Vector2 MoveVector { get; set; }
        public float HeadYaw { get; set; }
        public ulong InputFlags { get; set; }
        public ulong InputFlags2 { get; set; }
        public uint InputMode { get; set; }
        public uint PlayMode { get; set; }
        public uint InteractionModel { get; set; }
        public Vector2 InteractRotation { get; set; }
        public ulong Tick { get; set; }
        public Vector3 Delta { get; set; }

        public override void Encode(BinaryStream stream)
        {
            throw new NotImplementedException();
        }

        public override void Decode(BinaryStream stream)
        {
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            Position = stream.ReadVector3();
            MoveVector = stream.ReadVector2();
            HeadYaw = stream.ReadFloat();
            
            InputFlags = stream.ReadUnsignedVarLong();
            InputFlags2 = stream.ReadUnsignedVarLong();

            InputMode = stream.ReadUnsignedVarInt();
            PlayMode = stream.ReadUnsignedVarInt();
            InteractionModel = stream.ReadUnsignedVarInt();
            
            InteractRotation = stream.ReadVector2();
            
            Tick = stream.ReadUnsignedVarLong();
            
            Delta = stream.ReadVector3();
            
            // TODO: Implement remaining optional fields after this based on InputFlags (Item Interactions, Block Actions, etc.)
        }
    }
}
