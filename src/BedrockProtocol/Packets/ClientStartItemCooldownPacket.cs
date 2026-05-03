using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ClientStartItemCooldownPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerStartItemCooldown;

        public string Category { get; set; } = string.Empty;
        public int Duration { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(Category);
            stream.WriteVarInt(Duration);
        }

        public override void Decode(BinaryStream stream)
        {
            Category = stream.ReadString();
            Duration = stream.ReadVarInt();
        }
    }
}
