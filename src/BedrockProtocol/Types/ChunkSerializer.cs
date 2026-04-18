using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    /// <summary>
    /// Serializes a full chunk column into the payload format expected by LevelChunkPacket.
    /// 
    /// Payload structure:
    ///   [SubChunk data for each non-empty sub-chunk, bottom to top]
    ///   [Biome data — palette-based, one palette per sub-chunk section]
    ///   [Border blocks — single byte 0x00]
    /// </summary>
    public static class ChunkSerializer
    {
        /// <summary>
        /// Serializes a chunk column and returns the payload bytes and the sub-chunk count.
        /// </summary>
        /// <param name="subChunks">Array of sub-chunk data (from bottom to top), or null for empty sub-chunks.</param>
        /// <param name="defaultAirId">The runtime ID for air used in empty sub-chunks.</param>
        /// <returns>Tuple of (payload bytes, subChunkCount).</returns>
        public static (byte[] Payload, int SubChunkCount) Serialize(SubChunkData?[] subChunks, int defaultAirId, int biomeId = 0)
        {
            var stream = new BinaryStream();

            int subChunkCount = FindTopSubChunkIndex(subChunks) + 1;

            for (int i = 0; i < subChunkCount; i++)
            {
                if (subChunks[i] != null)
                {
                    subChunks[i]!.WriteTo(stream);
                }
                else
                {
                    WriteEmptySubChunk(stream, i - 4, defaultAirId);
                }
            }
            for (int i = 0; i < 24; i++)
            {
                WriteBiomePalette(stream, biomeId);
            }

            stream.WriteByte(0);

            return (stream.GetBuffer(), subChunkCount);
        }

        /// <summary>
        /// Finds the index of the topmost non-null sub-chunk.
        /// </summary>
        private static int FindTopSubChunkIndex(SubChunkData?[] subChunks)
        {
            for (int i = subChunks.Length - 1; i >= 0; i--)
            {
                if (subChunks[i] != null)
                    return i;
            }
            return 0;
        }

        /// <summary>
        /// Writes an empty sub-chunk (all air, single palette entry).
        /// </summary>
        private static void WriteEmptySubChunk(BinaryStream stream, int subChunkIndex, int defaultAirId)
        {
            stream.WriteByte(SubChunkData.FormatVersion);
            stream.WriteByte(1);
            stream.WriteByte((byte)(sbyte)subChunkIndex);

            stream.WriteByte(3);

            int wordCount = 4096 / 32;
            for (int i = 0; i < wordCount; i++)
            {
                stream.WriteIntLE(0);
            }

            stream.WriteVarInt(1);
            stream.WriteVarInt(defaultAirId);
        }

        /// <summary>
        /// Writes a single-biome palette for one sub-chunk section.
        /// Uses the same paletted format as block storage but for biome IDs.
        /// For a flat world, every section has the same biome.
        /// </summary>
        private static void WriteBiomePalette(BinaryStream stream, int biomeId)
        {
            stream.WriteByte(3);
            int wordCount = 4096 / 32;
            for (int i = 0; i < wordCount; i++)
            {
                stream.WriteIntLE(0);
            }

            stream.WriteVarInt(1);
            stream.WriteVarInt(biomeId);
        }
    }
}
