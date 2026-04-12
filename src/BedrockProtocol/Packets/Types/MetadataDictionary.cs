using System.Collections.Generic;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets.Types
{
    public class MetadataDictionary
    {
        public Dictionary<uint, MetadataEntry> Entries { get; } = new Dictionary<uint, MetadataEntry>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Entries.Count);
            foreach (var entry in Entries.Values)
            {
                stream.WriteUnsignedVarInt(entry.Id);
                stream.WriteUnsignedVarInt((uint)entry.Type);
                entry.Encode(stream);
            }
        }

        public void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedVarInt();
            for (int i = 0; i < count; i++)
            {
                uint id = stream.ReadUnsignedVarInt();
                EntityDataType type = (EntityDataType)stream.ReadUnsignedVarInt();
                MetadataEntry entry = CreateEntry(type);
                entry.Id = id;
                entry.Decode(stream);
                Entries[id] = entry;
            }
        }

        private MetadataEntry CreateEntry(EntityDataType type)
        {
            return type switch
            {
                EntityDataType.Byte => new ByteMetadataEntry(),
                EntityDataType.Int => new IntMetadataEntry(),
                EntityDataType.Float => new FloatMetadataEntry(),
                EntityDataType.String => new StringMetadataEntry(),
                EntityDataType.Long => new LongMetadataEntry(),
                _ => throw new System.NotImplementedException($"Metadata type {type} not implemented")
            };
        }
    }
}
