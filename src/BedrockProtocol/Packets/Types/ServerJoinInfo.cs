using System;

namespace BedrockProtocol.Packets.Types
{
    public class ServerJoinInfo
    {
        public GatheringJoinInfo? GatheringJoinInfo { get; set; }
        public StoreEntryPointInfo? StoreEntryPointInfo { get; set; }
        public PresenceInfo? PresenceInfo { get; set; }
    }

    public class GatheringJoinInfo
    {
        public Guid ExperienceId { get; set; }
        public string ExperienceName { get; set; } = string.Empty;
        public Guid ExperienceWorldId { get; set; }
        public string ExperienceWorldName { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public Guid Unk { get; set; }
        public Guid Unk1 { get; set; }
        public string ServerId { get; set; } = string.Empty;
    }

    public class StoreEntryPointInfo
    {
        public string StoreId { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;
    }

    public class PresenceInfo
    {
        public string ExperienceName { get; set; } = string.Empty;
        public string WorldName { get; set; } = string.Empty;
    }
}
