using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    /// <summary>
    /// Palette-based block storage for a 16x16x16 sub-chunk.
    /// Blocks are stored as indices into a local palette, bit-packed into 32-bit words.
    /// 
    /// Network serialization format:
    ///   byte  header = (bitsPerBlock &lt;&lt; 1) | 1   (1 = runtime/network flag)
    ///   int32[] words (little-endian, bit-packed block indices)
    ///   VarInt paletteSize
    ///   VarInt[] paletteEntries (runtime IDs)
    /// </summary>
    public class PalettedBlockStorage
    {
        public const int BlocksPerSubChunk = 16 * 16 * 16; // 4096

        /// <summary>
        /// Valid bits-per-block values and their corresponding blocks-per-word count.
        /// </summary>
        private static readonly int[] ValidBitsPerBlock = { 1, 2, 3, 4, 5, 6, 8, 16 };

        /// <summary>
        /// The palette maps local index -> runtime ID.
        /// </summary>
        private readonly List<int> _palette = new();

        /// <summary>
        /// Maps runtime ID -> local palette index for fast lookup during Set operations.
        /// </summary>
        private readonly Dictionary<int, int> _runtimeIdToIndex = new();

        /// <summary>
        private readonly int[] _blocks = new int[BlocksPerSubChunk];

        public PalettedBlockStorage(int defaultRuntimeId)
        {
            _palette.Add(defaultRuntimeId);
            _runtimeIdToIndex[defaultRuntimeId] = 0;
        }

        /// <summary>
        /// Gets the block index for the given coordinates.
        /// Bedrock uses XZY ordering within sub-chunks.
        /// </summary>
        private static int GetIndex(int x, int y, int z)
        {
            return (x << 8) | (z << 4) | y;
        }

        /// <summary>
        /// Sets the block at the given local coordinates (0-15 each).
        /// </summary>
        public void Set(int x, int y, int z, int runtimeId)
        {
            if (!_runtimeIdToIndex.TryGetValue(runtimeId, out int paletteIndex))
            {
                paletteIndex = _palette.Count;
                _palette.Add(runtimeId);
                _runtimeIdToIndex[runtimeId] = paletteIndex;
            }

            _blocks[GetIndex(x, y, z)] = paletteIndex;
        }

        /// <summary>
        /// Gets the runtime ID of the block at the given local coordinates.
        /// </summary>
        public int Get(int x, int y, int z)
        {
            int paletteIndex = _blocks[GetIndex(x, y, z)];
            return _palette[paletteIndex];
        }

        /// <summary>
        /// Returns true if the storage only contains the default block (air / first palette entry only).
        /// </summary>
        public bool IsEmpty => _palette.Count <= 1;

        /// <summary>
        /// Determines the minimum bits-per-block needed for the current palette size.
        /// </summary>
        private int GetBitsPerBlock()
        {
            int paletteSize = _palette.Count;

            foreach (int bits in ValidBitsPerBlock)
            {
                if ((1 << bits) >= paletteSize)
                    return bits;
            }

            return 16; // Maximum
        }

        /// <summary>
        /// Serializes this block storage to the network format.
        /// </summary>
        public void WriteTo(BinaryStream stream)
        {
            int bitsPerBlock = GetBitsPerBlock();
            int blocksPerWord = 32 / bitsPerBlock;
            int wordCount = (BlocksPerSubChunk + blocksPerWord - 1) / blocksPerWord;

            stream.WriteByte((byte)((bitsPerBlock << 1) | 1));

            int position = 0;
            for (int i = 0; i < wordCount; i++)
            {
                int word = 0;
                for (int j = 0; j < blocksPerWord && position < BlocksPerSubChunk; j++)
                {
                    word |= (_blocks[position] & ((1 << bitsPerBlock) - 1)) << (j * bitsPerBlock);
                    position++;
                }
                stream.WriteIntLE(word);
            }

            stream.WriteVarInt(_palette.Count);
            foreach (int runtimeId in _palette)
            {
                stream.WriteVarInt(runtimeId);
            }
        }
    }
}
