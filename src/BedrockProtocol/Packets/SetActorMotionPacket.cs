using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class SetActorMotionPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetActorMotion;

        public ulong EntityRuntimeId { get; set; }
        public Vector3 Velocity { get; set; }
        public ulong Tick { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteVector3(Velocity);
            stream.WriteUnsignedVarLong(Tick);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            Velocity = stream.ReadVector3();
            Tick = stream.ReadUnsignedVarLong();
        }
    }
}
