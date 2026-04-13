using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    public class ServerTelemetryData
    {
        public string ServerId { get; set; } = string.Empty;
        public string ScenarioId { get; set; } = string.Empty;
        public string WorldId { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(ServerId);
            stream.WriteString(ScenarioId);
            stream.WriteString(WorldId);
            stream.WriteString(OwnerId);
        }

        public void Decode(BinaryStream stream)
        {
            ServerId = stream.ReadString();
            ScenarioId = stream.ReadString();
            WorldId = stream.ReadString();
            OwnerId = stream.ReadString();
        }
    }
}