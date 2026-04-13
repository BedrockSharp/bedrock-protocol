using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    public class GatheringJoinInformation
    {
        public string JoinId { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(JoinId);
            stream.WriteString(HostName);
        }

        public void Decode(BinaryStream stream)
        {
            JoinId = stream.ReadString();
            HostName = stream.ReadString();
        }
    }
}