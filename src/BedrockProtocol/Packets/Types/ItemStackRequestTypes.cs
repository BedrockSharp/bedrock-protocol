using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public struct FullContainerName
    {
        public byte ContainerId { get; set; }
        public uint? DynamicContainerId { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteByte(ContainerId);
            stream.WriteBool(DynamicContainerId.HasValue);
            if (DynamicContainerId.HasValue)
            {
                stream.WriteUnsignedVarInt(DynamicContainerId.Value);
            }
        }

        public void Decode(BinaryStream stream)
        {
            ContainerId = stream.ReadByte();
            if (stream.ReadBool())
            {
                DynamicContainerId = stream.ReadUnsignedVarInt();
            }
        }
    }

    public struct StackRequestSlotInfo
    {
        public FullContainerName ContainerName { get; set; }
        public byte Slot { get; set; }
        public int StackNetworkId { get; set; }

        public void Encode(BinaryStream stream)
        {
            ContainerName.Encode(stream);
            stream.WriteByte(Slot);
            stream.WriteVarInt(StackNetworkId);
        }

        public void Decode(BinaryStream stream)
        {
            ContainerName.Decode(stream);
            Slot = stream.ReadByte();
            StackNetworkId = stream.ReadVarInt();
        }
    }

    public enum StackRequestActionType : byte
    {
        Take = 0,
        Place = 1,
        Swap = 2,
        Drop = 3,
        Destroy = 4,
        Consume = 5,
        Create = 6,
        PlaceInContainer = 7,
        TakeOutContainer = 8,
        LabTableCombine = 9,
        BeaconPayment = 10,
        MineBlock = 11,
        CraftRecipe = 12,
        CraftRecipeAuto = 13,
        CraftCreative = 14,
        CraftRecipeOptional = 15,
        CraftGrindstone = 16,
        CraftLoom = 17,
        CraftNonImplementedDeprecated = 18,
        CraftResultsDeprecated = 19
    }

    public abstract class StackRequestAction
    {
        public abstract StackRequestActionType Type { get; }
        public abstract void Encode(BinaryStream stream);
        public abstract void Decode(BinaryStream stream);

        public static StackRequestAction? Create(StackRequestActionType type)
        {
            return type switch
            {
                StackRequestActionType.Take => new TakeStackRequestAction(),
                StackRequestActionType.Place => new PlaceStackRequestAction(),
                StackRequestActionType.Swap => new SwapStackRequestAction(),
                StackRequestActionType.Drop => new DropStackRequestAction(),
                StackRequestActionType.Destroy => new DestroyStackRequestAction(),
                StackRequestActionType.Consume => new ConsumeStackRequestAction(),
                StackRequestActionType.Create => new CreateStackRequestAction(),
                StackRequestActionType.PlaceInContainer => new PlaceInContainerStackRequestAction(),
                StackRequestActionType.TakeOutContainer => new TakeOutContainerStackRequestAction(),
                StackRequestActionType.LabTableCombine => new LabTableCombineStackRequestAction(),
                StackRequestActionType.BeaconPayment => new BeaconPaymentStackRequestAction(),
                StackRequestActionType.MineBlock => new MineBlockStackRequestAction(),
                StackRequestActionType.CraftRecipe => new CraftRecipeStackRequestAction(),
                StackRequestActionType.CraftRecipeAuto => new AutoCraftRecipeStackRequestAction(),
                StackRequestActionType.CraftCreative => new CraftCreativeStackRequestAction(),
                StackRequestActionType.CraftRecipeOptional => new CraftRecipeOptionalStackRequestAction(),
                StackRequestActionType.CraftGrindstone => new CraftGrindstoneRecipeStackRequestAction(),
                StackRequestActionType.CraftLoom => new CraftLoomRecipeStackRequestAction(),
                StackRequestActionType.CraftNonImplementedDeprecated => new CraftNonImplementedStackRequestAction(),
                StackRequestActionType.CraftResultsDeprecated => new CraftResultsDeprecatedStackRequestAction(),
                _ => null
            };
        }
    }

    public abstract class TransferStackRequestAction : StackRequestAction
    {
        public byte Count { get; set; }
        public StackRequestSlotInfo Source { get; set; }
        public StackRequestSlotInfo Destination { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(Count);
            Source.Encode(stream);
            Destination.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            Count = stream.ReadByte();
            Source = new StackRequestSlotInfo();
            Source.Decode(stream);
            Destination = new StackRequestSlotInfo();
            Destination.Decode(stream);
        }
    }

    public class TakeStackRequestAction : TransferStackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Take;
    }

    public class PlaceStackRequestAction : TransferStackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Place;
    }

    public class SwapStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Swap;
        public StackRequestSlotInfo Source { get; set; }
        public StackRequestSlotInfo Destination { get; set; }

        public override void Encode(BinaryStream stream)
        {
            Source.Encode(stream);
            Destination.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            Source = new StackRequestSlotInfo();
            Source.Decode(stream);
            Destination = new StackRequestSlotInfo();
            Destination.Decode(stream);
        }
    }

    public class DropStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Drop;
        public byte Count { get; set; }
        public StackRequestSlotInfo Source { get; set; }
        public bool Randomly { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(Count);
            Source.Encode(stream);
            stream.WriteBool(Randomly);
        }

        public override void Decode(BinaryStream stream)
        {
            Count = stream.ReadByte();
            Source = new StackRequestSlotInfo();
            Source.Decode(stream);
            Randomly = stream.ReadBool();
        }
    }

    public class DestroyStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Destroy;
        public byte Count { get; set; }
        public StackRequestSlotInfo Source { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(Count);
            Source.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            Count = stream.ReadByte();
            Source = new StackRequestSlotInfo();
            Source.Decode(stream);
        }
    }

    public class ConsumeStackRequestAction : DestroyStackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Consume;
    }

    public class CreateStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.Create;
        public byte ResultsSlot { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(ResultsSlot);
        }

        public override void Decode(BinaryStream stream)
        {
            ResultsSlot = stream.ReadByte();
        }
    }

    public class PlaceInContainerStackRequestAction : TransferStackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.PlaceInContainer;
    }

    public class TakeOutContainerStackRequestAction : TransferStackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.TakeOutContainer;
    }

    public class LabTableCombineStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.LabTableCombine;
        public override void Encode(BinaryStream stream) { }
        public override void Decode(BinaryStream stream) { }
    }

    public class BeaconPaymentStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.BeaconPayment;
        public int PrimaryEffect { get; set; }
        public int SecondaryEffect { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(PrimaryEffect);
            stream.WriteVarInt(SecondaryEffect);
        }

        public override void Decode(BinaryStream stream)
        {
            PrimaryEffect = stream.ReadVarInt();
            SecondaryEffect = stream.ReadVarInt();
        }
    }

    public class MineBlockStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.MineBlock;
        public int HotbarSlot { get; set; }
        public int PredictedDurability { get; set; }
        public int StackNetworkId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(HotbarSlot);
            stream.WriteVarInt(PredictedDurability);
            stream.WriteVarInt(StackNetworkId);
        }

        public override void Decode(BinaryStream stream)
        {
            HotbarSlot = stream.ReadVarInt();
            PredictedDurability = stream.ReadVarInt();
            StackNetworkId = stream.ReadVarInt();
        }
    }

    public class CraftRecipeStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftRecipe;
        public uint RecipeNetworkId { get; set; }
        public byte NumberOfCrafts { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(RecipeNetworkId);
            stream.WriteByte(NumberOfCrafts);
        }

        public override void Decode(BinaryStream stream)
        {
            RecipeNetworkId = stream.ReadUnsignedVarInt();
            NumberOfCrafts = stream.ReadByte();
        }
    }

    public class AutoCraftRecipeStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftRecipeAuto;
        public uint RecipeNetworkId { get; set; }
        public byte NumberOfCrafts { get; set; }
        public byte TimesCrafted { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(RecipeNetworkId);
            stream.WriteByte(NumberOfCrafts);
            stream.WriteByte(TimesCrafted);
        }

        public override void Decode(BinaryStream stream)
        {
            RecipeNetworkId = stream.ReadUnsignedVarInt();
            NumberOfCrafts = stream.ReadByte();
            TimesCrafted = stream.ReadByte();
        }
    }

    public class CraftCreativeStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftCreative;
        public uint CreativeItemNetworkId { get; set; }
        public byte NumberOfCrafts { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(CreativeItemNetworkId);
            stream.WriteByte(NumberOfCrafts);
        }

        public override void Decode(BinaryStream stream)
        {
            CreativeItemNetworkId = stream.ReadUnsignedVarInt();
            NumberOfCrafts = stream.ReadByte();
        }
    }

    public class CraftRecipeOptionalStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftRecipeOptional;
        public uint RecipeNetworkId { get; set; }
        public int FilterStringIndex { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(RecipeNetworkId);
            stream.WriteIntLE(FilterStringIndex);
        }

        public override void Decode(BinaryStream stream)
        {
            RecipeNetworkId = stream.ReadUnsignedVarInt();
            FilterStringIndex = stream.ReadIntLE();
        }
    }

    public class CraftGrindstoneRecipeStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftGrindstone;
        public uint RecipeNetworkId { get; set; }
        public byte NumberOfCrafts { get; set; }
        public int Cost { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(RecipeNetworkId);
            stream.WriteByte(NumberOfCrafts);
            stream.WriteVarInt(Cost);
        }

        public override void Decode(BinaryStream stream)
        {
            RecipeNetworkId = stream.ReadUnsignedVarInt();
            NumberOfCrafts = stream.ReadByte();
            Cost = stream.ReadVarInt();
        }
    }

    public class CraftLoomRecipeStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftLoom;
        public string Pattern { get; set; } = string.Empty;
        public byte TimesCrafted { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(Pattern);
            stream.WriteByte(TimesCrafted);
        }

        public override void Decode(BinaryStream stream)
        {
            Pattern = stream.ReadString();
            TimesCrafted = stream.ReadByte();
        }
    }

    public class CraftNonImplementedStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftNonImplementedDeprecated;
        public override void Encode(BinaryStream stream) { }
        public override void Decode(BinaryStream stream) { }
    }

    public class CraftResultsDeprecatedStackRequestAction : StackRequestAction
    {
        public override StackRequestActionType Type => StackRequestActionType.CraftResultsDeprecated;
        public List<ItemStack> ResultItems { get; set; } = new List<ItemStack>();
        public byte TimesCrafted { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)ResultItems.Count);
            foreach (var item in ResultItems)
            {
                item.Encode(stream);
            }
            stream.WriteByte(TimesCrafted);
        }

        public override void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedVarInt();
            ResultItems = new List<ItemStack>((int)count);
            for (int i = 0; i < count; i++)
            {
                var item = new ItemStack();
                item.Decode(stream);
                ResultItems.Add(item);
            }
            TimesCrafted = stream.ReadByte();
        }
    }

    public class ItemStackRequest
    {
        public int RequestId { get; set; }
        public List<StackRequestAction> Actions { get; set; } = new List<StackRequestAction>();
        public List<string> FilterStrings { get; set; } = new List<string>();
        public int FilterCause { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(RequestId);
            stream.WriteUnsignedVarInt((uint)Actions.Count);
            foreach (var action in Actions)
            {
                stream.WriteByte((byte)action.Type);
                action.Encode(stream);
            }
            stream.WriteArray(FilterStrings, stream.WriteString);
            stream.WriteIntLE(FilterCause);
        }

        public void Decode(BinaryStream stream)
        {
            RequestId = stream.ReadVarInt();
            uint actionCount = stream.ReadUnsignedVarInt();
            Actions = new List<StackRequestAction>((int)actionCount);
            for (int i = 0; i < actionCount; i++)
            {
                var type = (StackRequestActionType)stream.ReadByte();
                var action = StackRequestAction.Create(type);
                if (action != null)
                {
                    action.Decode(stream);
                    Actions.Add(action);
                }
            }
            FilterStrings = stream.ReadArray(stream.ReadString);
            FilterCause = stream.ReadIntLE();
        }
    }

    public struct StackResponseSlotInfo
    {
        public byte Slot { get; set; }
        public byte HotbarSlot { get; set; }
        public byte Count { get; set; }
        public int StackNetworkId { get; set; }
        public string CustomName { get; set; }
        public string FilteredCustomName { get; set; }
        public int DurabilityCorrection { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteByte(Slot);
            stream.WriteByte(HotbarSlot);
            stream.WriteByte(Count);
            stream.WriteVarInt(StackNetworkId);
            stream.WriteString(CustomName ?? string.Empty);
            stream.WriteString(FilteredCustomName ?? string.Empty);
            stream.WriteVarInt(DurabilityCorrection);
        }

        public void Decode(BinaryStream stream)
        {
            Slot = stream.ReadByte();
            HotbarSlot = stream.ReadByte();
            Count = stream.ReadByte();
            StackNetworkId = stream.ReadVarInt();
            CustomName = stream.ReadString();
            FilteredCustomName = stream.ReadString();
            DurabilityCorrection = stream.ReadVarInt();
        }
    }

    public struct StackResponseContainerInfo
    {
        public FullContainerName Container { get; set; }
        public List<StackResponseSlotInfo> SlotInfo { get; set; }

        public void Encode(BinaryStream stream)
        {
            Container.Encode(stream);
            stream.WriteUnsignedVarInt((uint)SlotInfo.Count);
            foreach (var info in SlotInfo)
            {
                info.Encode(stream);
            }
        }

        public void Decode(BinaryStream stream)
        {
            Container.Decode(stream);
            uint count = stream.ReadUnsignedVarInt();
            SlotInfo = new List<StackResponseSlotInfo>((int)count);
            for (int i = 0; i < count; i++)
            {
                var info = new StackResponseSlotInfo();
                info.Decode(stream);
                SlotInfo.Add(info);
            }
        }
    }

    public class ItemStackResponse
    {
        public byte Status { get; set; }
        public int RequestId { get; set; }
        public List<StackResponseContainerInfo> ContainerInfo { get; set; } = new List<StackResponseContainerInfo>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteByte(Status);
            stream.WriteVarInt(RequestId);
            if (Status == 0) // OK
            {
                stream.WriteUnsignedVarInt((uint)ContainerInfo.Count);
                foreach (var info in ContainerInfo)
                {
                    info.Encode(stream);
                }
            }
        }

        public void Decode(BinaryStream stream)
        {
            Status = stream.ReadByte();
            RequestId = stream.ReadVarInt();
            if (Status == 0)
            {
                uint count = stream.ReadUnsignedVarInt();
                ContainerInfo = new List<StackResponseContainerInfo>((int)count);
                for (int i = 0; i < count; i++)
                {
                    var info = new StackResponseContainerInfo();
                    info.Decode(stream);
                    ContainerInfo.Add(info);
                }
            }
        }
    }
}
