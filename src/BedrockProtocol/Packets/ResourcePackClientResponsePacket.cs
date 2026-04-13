using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;

namespace BedrockProtocol.Packets
{
    public class ResourcePackClientResponsePacket : Packet
    {

        public override uint PacketId => (uint)PacketIds.ResourcePackClientResponse;

        public ResourcePackClientResponseStatus ResponseStatus { get; set; }

        public List<string> PackIds { get; set; } = new List<string>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)ResponseStatus);
            stream.WriteUnsignedShort((ushort)PackIds.Count);
            foreach (var packId in PackIds)
            {
                stream.WriteString(packId);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            ResponseStatus = (ResourcePackClientResponseStatus)stream.ReadByte();
            ushort count = stream.ReadUnsignedShort();
            PackIds.Clear();
            for (int i = 0; i < count; i++)
            {
                PackIds.Add(stream.ReadString());
            }
        }
    }
}
