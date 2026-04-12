using System;

namespace BedrockProtocol.Packets.Types
{
    public class PlayerListEntry
    {
        public Guid Uuid { get; set; }
        public long EntityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Xuid { get; set; } = string.Empty;
        public string PlatformChatId { get; set; } = string.Empty;
        public int BuildPlatform { get; set; }
        public byte[] SkinData { get; set; } = Array.Empty<byte>();
        public bool IsTeacher { get; set; }
        public bool IsHost { get; set; }
    }
}
