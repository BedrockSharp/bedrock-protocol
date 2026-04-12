using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class CreativeContentPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CreativeContent;

        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(0); 
        }

        public override void Decode(BinaryStream stream)
        {
            stream.ReadUnsignedVarInt(); 
        }
    }
}
