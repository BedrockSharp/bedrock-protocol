namespace BedrockProtocol.Packets.Types
{
    public class ServerTelemetryData
    {
        public string ServerId { get; set; } = string.Empty;
        public string ScenarioId { get; set; } = string.Empty;
        public string WorldId { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
    }
}
