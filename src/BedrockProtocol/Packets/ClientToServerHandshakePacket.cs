using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ClientToServerHandshakePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ClientToServerHandshake;

        public override void Encode(BinaryStream stream)
        {
        }

        public override void Decode(BinaryStream stream)
        {
        }
    }
}
