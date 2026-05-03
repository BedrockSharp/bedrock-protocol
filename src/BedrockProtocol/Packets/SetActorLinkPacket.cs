using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SetActorLinkPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetActorLink;

        public EntityLink Link { get; set; } = new EntityLink();

        public override void Encode(BinaryStream stream)
        {
            Link.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            Link.Decode(stream);
        }
    }
}
