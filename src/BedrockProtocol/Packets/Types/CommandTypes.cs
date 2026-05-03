using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;
using System;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class CommandOrigin
    {
        public CommandOriginType Origin { get; set; }
        public Guid Uuid { get; set; }
        public string RequestId { get; set; } = string.Empty;
        public long PlayerUniqueId { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Origin.ToString().ToLower());
            stream.WriteUuid(Uuid);
            stream.WriteString(RequestId);
            if (Origin == CommandOriginType.DevConsole || Origin == CommandOriginType.Test)
            {
                stream.WriteVarLong(PlayerUniqueId);
            }
        }

        public void Decode(BinaryStream stream)
        {
            string originStr = stream.ReadString();
            if (Enum.TryParse(originStr, true, out CommandOriginType originType))
            {
                Origin = originType;
            }
            else if (originStr == "commandblock")
            {
                Origin = CommandOriginType.Block;
            }
            else if (originStr == "minecartcommandblock")
            {
                Origin = CommandOriginType.MinecartBlock;
            }
            else if (originStr == "scripting")
            {
                Origin = CommandOriginType.Script;
            }
            else if (originStr == "executecontext")
            {
                Origin = CommandOriginType.Executor;
            }
            else
            {
                Origin = (CommandOriginType)255;
            }

            Uuid = stream.ReadUuid();
            RequestId = stream.ReadString();
            if (Origin == CommandOriginType.DevConsole || Origin == CommandOriginType.Test)
            {
                PlayerUniqueId = stream.ReadVarLong();
            }
        }

        public static CommandOriginType ParseOrigin(string originStr)
        {
            if (Enum.TryParse(originStr, true, out CommandOriginType originType)) return originType;
            switch (originStr)
            {
                case "commandblock": return CommandOriginType.Block;
                case "minecartcommandblock": return CommandOriginType.MinecartBlock;
                case "scripting": return CommandOriginType.Script;
                case "executecontext": return CommandOriginType.Executor;
                default: return (CommandOriginType)255;
            }
        }

        public static string FormatOrigin(CommandOriginType origin)
        {
            switch (origin)
            {
                case CommandOriginType.Block: return "commandblock";
                case CommandOriginType.MinecartBlock: return "minecartcommandblock";
                case CommandOriginType.Script: return "scripting";
                case CommandOriginType.Executor: return "executecontext";
                default: return origin.ToString().ToLower();
            }
        }
    }

    public class CommandOutputMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Parameters { get; set; } = new List<string>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteBool(Success);
            stream.WriteString(Message);
            stream.WriteArray(Parameters, stream.WriteString);
        }

        public void Decode(BinaryStream stream)
        {
            Success = stream.ReadBool();
            Message = stream.ReadString();
            Parameters = stream.ReadArray(stream.ReadString);
        }
    }

    public class Command
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ushort Flags { get; set; }
        public CommandPermissionLevel PermissionLevel { get; set; }
        public uint AliasesOffset { get; set; }
        public List<uint> ChainedSubcommandOffsets { get; set; } = new List<uint>();
        public List<CommandOverload> Overloads { get; set; } = new List<CommandOverload>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Name);
            stream.WriteString(Description);
            stream.WriteUnsignedShort(Flags);
            stream.WriteString(PermissionLevel.ToString().ToLower());
            stream.WriteUnsignedInt(AliasesOffset);
            stream.WriteArray(ChainedSubcommandOffsets, stream.WriteUnsignedInt);
            stream.WriteArray(Overloads, o => o.Encode(stream));
        }

        public void Decode(BinaryStream stream)
        {
            Name = stream.ReadString();
            Description = stream.ReadString();
            Flags = stream.ReadUnsignedShort();
            string permStr = stream.ReadString();
            if (Enum.TryParse(permStr, true, out CommandPermissionLevel level)) PermissionLevel = level;
            AliasesOffset = stream.ReadUnsignedInt();
            ChainedSubcommandOffsets = stream.ReadArray(stream.ReadUnsignedInt);
            Overloads = stream.ReadArray(() =>
            {
                var o = new CommandOverload();
                o.Decode(stream);
                return o;
            });
        }
    }

    public class CommandOverload
    {
        public bool Chaining { get; set; }
        public List<CommandParameter> Parameters { get; set; } = new List<CommandParameter>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteBool(Chaining);
            stream.WriteArray(Parameters, p => p.Encode(stream));
        }

        public void Decode(BinaryStream stream)
        {
            Chaining = stream.ReadBool();
            Parameters = stream.ReadArray(() =>
            {
                var p = new CommandParameter();
                p.Decode(stream);
                return p;
            });
        }
    }

    public class CommandParameter
    {
        public string Name { get; set; } = string.Empty;
        public uint Type { get; set; }
        public bool Optional { get; set; }
        public byte Options { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Name);
            stream.WriteUnsignedInt(Type);
            stream.WriteBool(Optional);
            stream.WriteByte(Options);
        }

        public void Decode(BinaryStream stream)
        {
            Name = stream.ReadString();
            Type = stream.ReadUnsignedInt();
            Optional = stream.ReadBool();
            Options = stream.ReadByte();
        }
    }

    public class CommandEnum
    {
        public string Type { get; set; } = string.Empty;
        public List<uint> ValueIndices { get; set; } = new List<uint>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Type);
            stream.WriteArray(ValueIndices, stream.WriteUnsignedInt);
        }

        public void Decode(BinaryStream stream)
        {
            Type = stream.ReadString();
            ValueIndices = stream.ReadArray(stream.ReadUnsignedInt);
        }
    }

    public class ChainedSubcommand
    {
        public string Name { get; set; } = string.Empty;
        public List<ChainedSubcommandValue> Values { get; set; } = new List<ChainedSubcommandValue>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Name);
            stream.WriteArray(Values, v => v.Encode(stream));
        }

        public void Decode(BinaryStream stream)
        {
            Name = stream.ReadString();
            Values = stream.ReadArray(() =>
            {
                var v = new ChainedSubcommandValue();
                v.Decode(stream);
                return v;
            });
        }
    }

    public class ChainedSubcommandValue
    {
        public uint Index { get; set; }
        public uint Value { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedInt(Index);
            stream.WriteUnsignedInt(Value);
        }

        public void Decode(BinaryStream stream)
        {
            Index = stream.ReadUnsignedInt();
            Value = stream.ReadUnsignedInt();
        }
    }

    public class DynamicEnum
    {
        public string Type { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Type);
            stream.WriteArray(Values, stream.WriteString);
        }

        public void Decode(BinaryStream stream)
        {
            Type = stream.ReadString();
            Values = stream.ReadArray(stream.ReadString);
        }
    }

    public class CommandEnumConstraint
    {
        public uint EnumValueIndex { get; set; }
        public uint EnumIndex { get; set; }
        public byte[] Constraints { get; set; } = Array.Empty<byte>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedInt(EnumValueIndex);
            stream.WriteUnsignedInt(EnumIndex);
            stream.WriteByteArray(Constraints);
        }

        public void Decode(BinaryStream stream)
        {
            EnumValueIndex = stream.ReadUnsignedInt();
            EnumIndex = stream.ReadUnsignedInt();
            Constraints = stream.ReadByteArray();
        }
    }
}
