using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SetCommandsEnabledPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetCommandsEnabled;

        public bool Enabled { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(Enabled);
        }

        public override void Decode(BinaryStream stream)
        {
            Enabled = stream.ReadBool();
        }
    }
}
