using BedrockProtocol.Utils;
using Nbt;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class ItemInstance
    {
        public int StackNetworkId { get; set; }
        public ItemStack Stack { get; set; } = new ItemStack();

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(StackNetworkId);
            Stack.Encode(stream);
        }

        public void Decode(BinaryStream stream)
        {
            StackNetworkId = stream.ReadVarInt();
            Stack.Decode(stream);
        }
    }

    public class ItemStack
    {
        public ItemType ItemType { get; set; } = new ItemType();
        public int BlockRuntimeId { get; set; }
        public ushort Count { get; set; }
        public CompoundTag? NbtData { get; set; }
        public List<string> CanBePlacedOn { get; set; } = new List<string>();
        public List<string> CanBreak { get; set; } = new List<string>();
        public bool HasNetworkId { get; set; }
        public long BlockingTick { get; set; }

        public void Encode(BinaryStream stream)
        {
            ItemType.Encode(stream);
            if (ItemType.NetworkId == 0) return;

            stream.WriteUnsignedShort(Count);
            stream.WriteUnsignedVarInt((uint)ItemType.MetadataValue);
            
            stream.WriteVarInt(BlockRuntimeId);
            
            // Encode NbtData and other extra stuff here.
            // Note: Since gophertunnel has a specific item format, let's follow standard NBT serialization
            byte hasNbt = (byte)(NbtData != null ? 1 : 0);
            stream.WriteByte(hasNbt);
            if (NbtData != null)
            {
                stream.WriteNbt(NbtData);
            }

            stream.WriteArray(CanBePlacedOn, stream.WriteString);
            stream.WriteArray(CanBreak, stream.WriteString);

            if (ItemType.NetworkId == 355) // Example for shield blocking tick if needed. In gophertunnel it's specific.
            {
                stream.WriteVarLong(BlockingTick);
            }
        }

        public void Decode(BinaryStream stream)
        {
            ItemType.Decode(stream);
            if (ItemType.NetworkId == 0) return;

            Count = stream.ReadUnsignedShort();
            ItemType.MetadataValue = stream.ReadUnsignedVarInt();

            BlockRuntimeId = stream.ReadVarInt();

            byte hasNbt = stream.ReadByte();
            if (hasNbt == 1)
            {
                NbtData = stream.ReadNetworkNbt();
            }

            CanBePlacedOn = stream.ReadArray(stream.ReadString);
            CanBreak = stream.ReadArray(stream.ReadString);

            if (ItemType.NetworkId == 355) // shield
            {
                BlockingTick = stream.ReadVarLong();
            }
        }
    }

    public class ItemType
    {
        public int NetworkId { get; set; }
        public uint MetadataValue { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(NetworkId);
        }

        public void Decode(BinaryStream stream)
        {
            NetworkId = stream.ReadVarInt();
        }
    }

    public class ItemEntry
    {
        public string Name { get; set; } = string.Empty;
        public short RuntimeId { get; set; }
        public bool ComponentBased { get; set; }
        public int Version { get; set; }
        public CompoundTag? Data { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Name);
            stream.WriteShortLE(RuntimeId);
            stream.WriteBool(ComponentBased);
            stream.WriteVarInt(Version);
            if (ComponentBased && Data != null)
            {
                stream.WriteNbt(Data);
            }
        }

        public void Decode(BinaryStream stream)
        {
            Name = stream.ReadString();
            RuntimeId = stream.ReadShortLE();
            ComponentBased = stream.ReadBool();
            Version = stream.ReadVarInt();
            if (ComponentBased)
            {
                Data = stream.ReadNetworkNbt();
            }
        }
    }

    public class LegacySetItemSlot
    {
        public byte ContainerId { get; set; }
        public byte Slots { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteByte(ContainerId);
            stream.WriteByte(Slots);
        }

        public void Decode(BinaryStream stream)
        {
            ContainerId = stream.ReadByte();
            Slots = stream.ReadByte();
        }
    }
}
