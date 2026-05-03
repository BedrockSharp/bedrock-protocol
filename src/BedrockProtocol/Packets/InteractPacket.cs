using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class InteractPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Interact;

        public InteractAction Action { get; set; }
        public ulong TargetRuntimeEntityId { get; set; }
        public Vector3 Position { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)Action);
            stream.WriteUnsignedVarLong(TargetRuntimeEntityId);
            if (Action == InteractAction.Hover || Action == InteractAction.Interact)
            {
                stream.WriteVector3(Position);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            Action = (InteractAction)stream.ReadByte();
            TargetRuntimeEntityId = stream.ReadUnsignedVarLong();
            if (Action == InteractAction.Hover || Action == InteractAction.Interact)
            {
                Position = stream.ReadVector3();
            }
        }
    }
}
