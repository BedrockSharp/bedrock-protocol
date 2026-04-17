using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class PlayStatusPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayStatus;

        public PlayStatus Status { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBigEndianInt((int)Status);
        }

        public override void Decode(BinaryStream stream)
        {
            Status = (PlayStatus)stream.ReadBigEndianInt();
        }
    }
}
