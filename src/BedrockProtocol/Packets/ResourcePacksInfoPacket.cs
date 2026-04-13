using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using System;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class ResourcePacksInfoPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePacksInfo;

        public List<ResourcePackInfoEntry> ResourcePackEntries { get; set; } = new();
        public bool MustAccept { get; set; }
        public bool HasAddons { get; set; }
        public bool HasScripts { get; set; }
        public Guid WorldTemplateId { get; set; }
        public string WorldTemplateVersion { get; set; } = string.Empty;
        public bool ForceDisableVibrantVisuals { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(MustAccept);
            stream.WriteBool(HasAddons);
            stream.WriteBool(HasScripts);
            stream.WriteBool(ForceDisableVibrantVisuals);
            stream.WriteUuid(WorldTemplateId);
            stream.WriteString(WorldTemplateVersion);

            stream.WriteUnsignedShort((ushort)ResourcePackEntries.Count);
            foreach (var entry in ResourcePackEntries)
            {
                entry.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            MustAccept = stream.ReadBool();
            HasAddons = stream.ReadBool();
            HasScripts = stream.ReadBool();
            ForceDisableVibrantVisuals = stream.ReadBool();
            WorldTemplateId = stream.ReadUuid();
            WorldTemplateVersion = stream.ReadString();

            ushort count = stream.ReadUnsignedShort();
            ResourcePackEntries.Clear();

            for (int i = 0; i < count; i++)
            {
                var entry = new ResourcePackInfoEntry();
                entry.Decode(stream);
                ResourcePackEntries.Add(entry);
            }
        }
    }
}