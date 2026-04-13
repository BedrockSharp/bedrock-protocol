using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ServerToClientHandshakePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ServerToClientHandshake;

        public string JwtToken { get; set; } = string.Empty;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(JwtToken);
        }

        public override void Decode(BinaryStream stream)
        {
            JwtToken = stream.ReadString();
        }
    }
}
