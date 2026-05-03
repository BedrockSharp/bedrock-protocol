using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ContainerRegistryCleanupPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ContainerRegistryCleanup;

        public override void Encode(BinaryStream stream)
        {
        }

        public override void Decode(BinaryStream stream)
        {
        }
    }
}
