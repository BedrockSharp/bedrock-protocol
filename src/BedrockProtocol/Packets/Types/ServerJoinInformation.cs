using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    public class ServerJoinInformation
    {
        public GatheringJoinInformation GatheringJoinInformation { get; set; } = new();

        public void Encode(BinaryStream stream)
        {
            stream.WriteOptional(GatheringJoinInformation, (s, v) => v.Encode(s));
        }

        public void Decode(BinaryStream stream)
        {
            GatheringJoinInformation = stream.ReadOptional<GatheringJoinInformation>();
        }
    }
}