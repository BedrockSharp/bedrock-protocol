using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Packets.Types;
using System;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class CommandOutputPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CommandOutput;

        public CommandOrigin CommandOrigin { get; set; } = new CommandOrigin();
        public CommandOutputType OutputType { get; set; }
        public uint SuccessCount { get; set; }
        public List<CommandOutputMessage> OutputMessages { get; set; } = new List<CommandOutputMessage>();
        public string? DataSet { get; set; } = null;

        public override void Encode(BinaryStream stream)
        {
            CommandOrigin.Encode(stream);
            stream.WriteString(OutputType.ToString().ToLower());
            stream.WriteUnsignedInt(SuccessCount);
            stream.WriteArray(OutputMessages, m => m.Encode(stream));
            stream.WriteOptional(DataSet, (s, v) => s.WriteString(v));
        }

        public override void Decode(BinaryStream stream)
        {
            CommandOrigin.Decode(stream);
            string typeStr = stream.ReadString();
            if (Enum.TryParse(typeStr, true, out CommandOutputType type))
            {
                OutputType = type;
            }
            else
            {
                OutputType = CommandOutputType.None;
            }
            SuccessCount = stream.ReadUnsignedInt();
            OutputMessages = stream.ReadArray(() =>
            {
                var m = new CommandOutputMessage();
                m.Decode(stream);
                return m;
            });
            DataSet = stream.ReadOptionalClass(s => s.ReadString());
        }
    }
}
