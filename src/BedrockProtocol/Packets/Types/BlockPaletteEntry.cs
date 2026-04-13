namespace BedrockProtocol.Types
{
    public class BlockPaletteEntry
    {
        public string Name { get; set; }
        public CacheableNbt States { get; set; }

        public BlockPaletteEntry(string name, CacheableNbt states)
        {
            Name = name;
            States = states;
        }
    }
}