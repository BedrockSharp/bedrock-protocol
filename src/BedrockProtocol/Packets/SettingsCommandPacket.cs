using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SettingsCommandPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SettingsCommand;

        public string CommandLine { get; set; } = string.Empty;
        public bool SuppressOutput { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(CommandLine);
            stream.WriteBool(SuppressOutput);
        }

        public override void Decode(BinaryStream stream)
        {
            CommandLine = stream.ReadString();
            SuppressOutput = stream.ReadBool();
        }
    }
}
