using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public abstract class Packet
    {
        public abstract uint PacketId { get; }

        public abstract void Encode(BinaryStream stream);

        public abstract void Decode(BinaryStream stream);
    }
}
