using BedrockProtocol.Utils;
using BedrockProtocol.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class InventoryAction
    {
        public uint SourceType { get; set; }
        public int WindowId { get; set; }
        public uint InventoryFlags { get; set; }
        public uint Slot { get; set; }
        public ItemInstance OldItem { get; set; } = new ItemInstance();
        public ItemInstance NewItem { get; set; } = new ItemInstance();

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(SourceType);
            switch (SourceType)
            {
                case 0:
                    stream.WriteVarInt(WindowId);
                    break;
                case 1:
                    stream.WriteUnsignedVarInt(InventoryFlags);
                    break;
                case 99999:
                    stream.WriteVarInt(WindowId);
                    break;
            }
            stream.WriteUnsignedVarInt(Slot);
            OldItem.Encode(stream);
            NewItem.Encode(stream);
        }

        public void Decode(BinaryStream stream)
        {
            SourceType = stream.ReadUnsignedVarInt();
            switch (SourceType)
            {
                case 0:
                    WindowId = stream.ReadVarInt();
                    break;
                case 1:
                    InventoryFlags = stream.ReadUnsignedVarInt();
                    break;
                case 99999:
                    WindowId = stream.ReadVarInt();
                    break;
            }
            Slot = stream.ReadUnsignedVarInt();
            OldItem.Decode(stream);
            NewItem.Decode(stream);
        }
    }

    public abstract class InventoryTransactionData
    {
        public abstract void Encode(BinaryStream stream);
        public abstract void Decode(BinaryStream stream);
    }

    public class NormalTransactionData : InventoryTransactionData
    {
        public override void Encode(BinaryStream stream) { }
        public override void Decode(BinaryStream stream) { }
    }

    public class MismatchTransactionData : InventoryTransactionData
    {
        public override void Encode(BinaryStream stream) { }
        public override void Decode(BinaryStream stream) { }
    }

    public class UseItemTransactionData : InventoryTransactionData
    {
        public uint ActionType { get; set; }
        public BlockPosition BlockPosition { get; set; } = new BlockPosition(0, 0, 0);
        public int BlockFace { get; set; }
        public int HotBarSlot { get; set; }
        public ItemInstance HeldItem { get; set; } = new ItemInstance();
        public Vector3 PlayerPosition { get; set; }
        public Vector3 ClickPosition { get; set; }
        public uint BlockRuntimeId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(ActionType);
            stream.WriteBlockPosition(BlockPosition);
            stream.WriteVarInt(BlockFace);
            stream.WriteVarInt(HotBarSlot);
            HeldItem.Encode(stream);
            stream.WriteVector3(PlayerPosition);
            stream.WriteVector3(ClickPosition);
            stream.WriteUnsignedVarInt(BlockRuntimeId);
        }

        public override void Decode(BinaryStream stream)
        {
            ActionType = stream.ReadUnsignedVarInt();
            BlockPosition = stream.ReadBlockPosition();
            BlockFace = stream.ReadVarInt();
            HotBarSlot = stream.ReadVarInt();
            HeldItem.Decode(stream);
            PlayerPosition = stream.ReadVector3();
            ClickPosition = stream.ReadVector3();
            BlockRuntimeId = stream.ReadUnsignedVarInt();
        }
    }

    public class UseItemOnEntityTransactionData : InventoryTransactionData
    {
        public ulong EntityRuntimeId { get; set; }
        public uint ActionType { get; set; }
        public int HotBarSlot { get; set; }
        public ItemInstance HeldItem { get; set; } = new ItemInstance();
        public Vector3 PlayerPosition { get; set; }
        public Vector3 ClickPosition { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteUnsignedVarInt(ActionType);
            stream.WriteVarInt(HotBarSlot);
            HeldItem.Encode(stream);
            stream.WriteVector3(PlayerPosition);
            stream.WriteVector3(ClickPosition);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            ActionType = stream.ReadUnsignedVarInt();
            HotBarSlot = stream.ReadVarInt();
            HeldItem.Decode(stream);
            PlayerPosition = stream.ReadVector3();
            ClickPosition = stream.ReadVector3();
        }
    }

    public class ReleaseItemTransactionData : InventoryTransactionData
    {
        public uint ActionType { get; set; }
        public int HotBarSlot { get; set; }
        public ItemInstance HeldItem { get; set; } = new ItemInstance();
        public Vector3 HeadPosition { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(ActionType);
            stream.WriteVarInt(HotBarSlot);
            HeldItem.Encode(stream);
            stream.WriteVector3(HeadPosition);
        }

        public override void Decode(BinaryStream stream)
        {
            ActionType = stream.ReadUnsignedVarInt();
            HotBarSlot = stream.ReadVarInt();
            HeldItem.Decode(stream);
            HeadPosition = stream.ReadVector3();
        }
    }
}
