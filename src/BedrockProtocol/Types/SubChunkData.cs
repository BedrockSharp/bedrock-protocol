using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    /// <summary>
    /// Represents a single 16x16x16 sub-chunk with one or more block storage layers.
    /// Layer 0 = primary blocks, Layer 1 = waterlogged blocks (optional).
    /// 
    /// Network serialization format:
    ///   byte version (9)
    ///   byte numStorageLayers
    ///   byte subChunkIndex (for version 9)
    ///   [for each layer: PalettedBlockStorage data]
    /// </summary>
    public class SubChunkData
    {
        public const byte FormatVersion = 9;

        /// <summary>
        /// Block storage layers. Typically 1 layer, or 2 for waterlogged blocks.
        /// </summary>
        public List<PalettedBlockStorage> Layers { get; } = new();

        /// <summary>
        /// The Y-index of this sub-chunk (e.g., -4 for Y=-64, 0 for Y=0, etc.)
        /// Used in version 9 format.
        /// </summary>
        public sbyte SubChunkIndex { get; set; }

        /// <summary>
        /// Serializes the sub-chunk data for network transmission.
        /// </summary>
        public void WriteTo(BinaryStream stream)
        {
            stream.WriteByte(FormatVersion);
            stream.WriteByte((byte)Layers.Count);
            stream.WriteByte((byte)SubChunkIndex);

            foreach (var layer in Layers)
            {
                layer.WriteTo(stream);
            }
        }
    }
}
