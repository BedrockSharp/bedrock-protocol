using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class ItemRegistryPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ItemRegistry;

        public List<ItemEntry> Items { get; set; } = new List<ItemEntry>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Items.Count);
            foreach (var item in Items)
            {
                item.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedVarInt();
            Items = new List<ItemEntry>((int)count);
            for (int i = 0; i < count; i++)
            {
                var item = new ItemEntry();
                item.Decode(stream);
                Items.Add(item);
            }
        }
    }
}
