using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class InventoryContentPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.InventoryContent;

        public uint WindowId { get; set; }
        public byte[] Payload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(WindowId);
            stream.WriteUnsignedVarInt((uint)Payload.Length);
            stream.WriteBytes(Payload);
        }

        public override void Decode(BinaryStream stream)
        {
            WindowId = stream.ReadUnsignedVarInt();
            uint len = stream.ReadUnsignedVarInt();
            Payload = stream.ReadBytes((int)len);
        }
    }
}
