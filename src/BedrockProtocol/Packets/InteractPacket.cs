using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class InteractPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Interact;

        public InteractAction Action { get; set; }
        public ulong TargetRuntimeEntityId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)Action);
            stream.WriteUnsignedVarLong(TargetRuntimeEntityId);
            if (Action == InteractAction.Hover || Action == InteractAction.Interact)
            {
                stream.WriteFloat(X);
                stream.WriteFloat(Y);
                stream.WriteFloat(Z);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            Action = (InteractAction)stream.ReadByte();
            TargetRuntimeEntityId = stream.ReadUnsignedVarLong();
            if (Action == InteractAction.Hover || Action == InteractAction.Interact)
            {
                X = stream.ReadFloat();
                Y = stream.ReadFloat();
                Z = stream.ReadFloat();
            }
        }
    }
}
