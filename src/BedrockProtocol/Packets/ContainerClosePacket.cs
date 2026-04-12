using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ContainerClosePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ContainerClose;

        public byte WindowId { get; set; }
        public bool ServerInitiated { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(WindowId);
            stream.WriteBool(ServerInitiated);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadByte();
            ServerInitiated = stream.ReadBool();
        }
    }
}
