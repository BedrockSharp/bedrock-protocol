using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class SpawnExperienceOrbPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SpawnExperienceOrb;

        public Vector3 Position { get; set; }
        public int ExperienceAmount { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVector3(Position);
            stream.WriteVarInt(ExperienceAmount);
        }

        public override void Decode(BinaryStream stream)
        {
            Position = stream.ReadVector3();
            ExperienceAmount = stream.ReadVarInt();
        }
    }
}
