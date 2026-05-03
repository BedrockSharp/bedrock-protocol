using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class InventoryTransactionPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.InventoryTransaction;

        public int LegacyRequestId { get; set; }
        public List<LegacySetItemSlot> LegacySetItemSlots { get; set; } = new List<LegacySetItemSlot>();
        public uint TransactionType { get; set; }
        public List<InventoryAction> Actions { get; set; } = new List<InventoryAction>();
        public InventoryTransactionData? TransactionData { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(LegacyRequestId);
            if (LegacyRequestId != 0)
            {
                stream.WriteUnsignedVarInt((uint)LegacySetItemSlots.Count);
                foreach (var slot in LegacySetItemSlots) slot.Encode(stream);
            }
            stream.WriteUnsignedVarInt(TransactionType);
            stream.WriteUnsignedVarInt((uint)Actions.Count);
            foreach (var action in Actions) action.Encode(stream);
            if (TransactionData != null)
            {
                TransactionData.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            LegacyRequestId = stream.ReadVarInt();
            if (LegacyRequestId != 0)
            {
                uint count = stream.ReadUnsignedVarInt();
                LegacySetItemSlots.Clear();
                for (int i = 0; i < count; i++)
                {
                    var slot = new LegacySetItemSlot();
                    slot.Decode(stream);
                    LegacySetItemSlots.Add(slot);
                }
            }
            TransactionType = stream.ReadUnsignedVarInt();
            uint actionCount = stream.ReadUnsignedVarInt();
            Actions.Clear();
            for (int i = 0; i < actionCount; i++)
            {
                var action = new InventoryAction();
                action.Decode(stream);
                Actions.Add(action);
            }

            switch (TransactionType)
            {
                case 0:
                    TransactionData = new NormalTransactionData();
                    break;
                case 1:
                    TransactionData = new MismatchTransactionData();
                    break;
                case 2:
                    TransactionData = new UseItemTransactionData();
                    break;
                case 3:
                    TransactionData = new UseItemOnEntityTransactionData();
                    break;
                case 4:
                    TransactionData = new ReleaseItemTransactionData();
                    break;
            }
            if (TransactionData != null)
            {
                TransactionData.Decode(stream);
            }
        }
    }
}
