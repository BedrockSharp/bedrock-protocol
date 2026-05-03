using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class UnlockedRecipesPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UnlockedRecipes;

        public uint UnlockType { get; set; }
        public List<string> Recipes { get; set; } = new List<string>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(UnlockType);
            stream.WriteArray(Recipes, stream.WriteString);
        }

        public override void Decode(BinaryStream stream)
        {
            UnlockType = stream.ReadUnsignedVarInt();
            Recipes = stream.ReadArray(stream.ReadString);
        }
    }
}
