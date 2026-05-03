using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class CommandRequestPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CommandRequest;

        public string CommandLine { get; set; }
        public CommandOrigin CommandOrigin { get; set; } = new CommandOrigin();
        public bool Internal { get; set; }
        public string Version { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(CommandLine);
            CommandOrigin.Encode(stream);
            stream.WriteBool(Internal);
            stream.WriteString(Version);
        }

        public override void Decode(BinaryStream stream)
        {
            CommandLine = stream.ReadString();
            CommandOrigin.Decode(stream);
            Internal = stream.ReadBool();
            Version = stream.ReadString();
        }
    }
}
