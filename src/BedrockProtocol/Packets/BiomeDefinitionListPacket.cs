using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class BiomeDefinitionListPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.BiomeDefinitionList;

        public byte[] NbtPayload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBytes(NbtPayload);
        }

        public override void Decode(BinaryStream stream)
        {
            NbtPayload = stream.ReadBytes((int)(stream.GetBuffer().Length - stream.Position));
        }
    }
}
