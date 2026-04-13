using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets.Types
{
    public class ResourcePackStackEntry
    {
        public string PackId { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string SubPackName { get; set; } = string.Empty;

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(PackId);
            stream.WriteString(Version);
            stream.WriteString(SubPackName);
        }

        public void Decode(BinaryStream stream)
        {
            PackId = stream.ReadString();
            Version = stream.ReadString();
            SubPackName = stream.ReadString();
        }
    }
}