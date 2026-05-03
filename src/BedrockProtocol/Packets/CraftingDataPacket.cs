using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class CraftingDataPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CraftingData;

        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            Payload = stream.ReadRemainingBytes();
        }
    }
}
