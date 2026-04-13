using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ResourcePackClientResponsePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePackClientResponse;

        public byte ResponseStatus { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(ResponseStatus);
            stream.WriteShort(0); 
        }

        public override void Decode(BinaryStream stream)
        {
            ResponseStatus = stream.ReadByte();
            stream.ReadShort(); 
        }
    }
}
