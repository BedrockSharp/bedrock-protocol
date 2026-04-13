using BedrockProtocol.Utils;
using System;

namespace BedrockProtocol.Packets.Types
{
    public class ResourcePackInfoEntry
    {
        public Guid PackId { get; set; }
        public string Version { get; set; } = string.Empty;
        public ulong SizeBytes { get; set; }
        public string EncryptionKey { get; set; } = string.Empty;
        public string SubPackName { get; set; } = string.Empty;
        public string ContentId { get; set; } = string.Empty;
        public bool HasScripts { get; set; }
        public bool IsAddonPack { get; set; }
        public bool IsRtxCapable { get; set; }
        public string CdnUrl { get; set; } = string.Empty;

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(PackId.ToString());
            stream.WriteString(Version);
            stream.WriteUnsignedLong(SizeBytes);
            stream.WriteString(EncryptionKey);
            stream.WriteString(SubPackName);
            stream.WriteString(ContentId);
            stream.WriteBool(HasScripts);
            stream.WriteBool(IsAddonPack);
            stream.WriteBool(IsRtxCapable);
            stream.WriteString(CdnUrl);
        }

        public void Decode(BinaryStream stream)
        {
            PackId = Guid.Parse(stream.ReadString());
            Version = stream.ReadString();
            SizeBytes = stream.ReadUnsignedLong();
            EncryptionKey = stream.ReadString();
            SubPackName = stream.ReadString();
            ContentId = stream.ReadString();
            HasScripts = stream.ReadBool();
            IsAddonPack = stream.ReadBool();
            IsRtxCapable = stream.ReadBool();
            CdnUrl = stream.ReadString();
        }
    }
}