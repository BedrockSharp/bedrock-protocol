using Nbt;

namespace BedrockProtocol.Packets.Types
{
    public class CustomBlockDefinition
    {
        public string Identifier { get; set; } = string.Empty;
        public CompoundTag Nbt { get; set; } = new CompoundTag();

        public CustomBlockDefinition(string identifier, CompoundTag nbt)
        {
            Identifier = identifier;
            Nbt = nbt;
        }
    }
}
