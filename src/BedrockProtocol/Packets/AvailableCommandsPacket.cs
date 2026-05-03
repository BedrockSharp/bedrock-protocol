using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class AvailableCommandsPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AvailableCommands;

        public List<string> EnumValues { get; set; } = new List<string>();
        public List<string> ChainedSubcommandValues { get; set; } = new List<string>();
        public List<string> Suffixes { get; set; } = new List<string>();
        public List<CommandEnum> Enums { get; set; } = new List<CommandEnum>();
        public List<ChainedSubcommand> ChainedSubcommands { get; set; } = new List<ChainedSubcommand>();
        public List<Command> Commands { get; set; } = new List<Command>();
        public List<DynamicEnum> DynamicEnums { get; set; } = new List<DynamicEnum>();
        public List<CommandEnumConstraint> Constraints { get; set; } = new List<CommandEnumConstraint>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteArray(EnumValues, stream.WriteString);
            stream.WriteArray(ChainedSubcommandValues, stream.WriteString);
            stream.WriteArray(Suffixes, stream.WriteString);
            stream.WriteArray(Enums, e => e.Encode(stream));
            stream.WriteArray(ChainedSubcommands, c => c.Encode(stream));
            stream.WriteArray(Commands, c => c.Encode(stream));
            stream.WriteArray(DynamicEnums, d => d.Encode(stream));
            stream.WriteArray(Constraints, c => c.Encode(stream));
        }

        public override void Decode(BinaryStream stream)
        {
            EnumValues = stream.ReadArray(stream.ReadString);
            ChainedSubcommandValues = stream.ReadArray(stream.ReadString);
            Suffixes = stream.ReadArray(stream.ReadString);
            Enums = stream.ReadArray(() =>
            {
                var e = new CommandEnum();
                e.Decode(stream);
                return e;
            });
            ChainedSubcommands = stream.ReadArray(() =>
            {
                var c = new ChainedSubcommand();
                c.Decode(stream);
                return c;
            });
            Commands = stream.ReadArray(() =>
            {
                var c = new Command();
                c.Decode(stream);
                return c;
            });
            DynamicEnums = stream.ReadArray(() =>
            {
                var d = new DynamicEnum();
                d.Decode(stream);
                return d;
            });
            Constraints = stream.ReadArray(() =>
            {
                var c = new CommandEnumConstraint();
                c.Decode(stream);
                return c;
            });
        }
    }
}
