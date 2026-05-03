using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class CommandBlockUpdatePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.CommandBlockUpdate;

        public bool Block { get; set; }
        
        public BlockPosition Position { get; set; } = new BlockPosition(0, 0, 0);
        public CommandBlockMode Mode { get; set; }
        public bool NeedsRedstone { get; set; }
        public bool Conditional { get; set; }

        public ulong MinecartEntityRuntimeId { get; set; }

        public string Command { get; set; } = string.Empty;
        public string LastOutput { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FilteredName { get; set; } = string.Empty;
        public bool ShouldTrackOutput { get; set; }
        public uint TickDelay { get; set; }
        public bool ExecuteOnFirstTick { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(Block);
            if (Block)
            {
                stream.WriteBlockPosition(Position);
                stream.WriteUnsignedVarInt((uint)Mode);
                stream.WriteBool(NeedsRedstone);
                stream.WriteBool(Conditional);
            }
            else
            {
                stream.WriteActorRuntimeId(MinecartEntityRuntimeId);
            }

            stream.WriteString(Command);
            stream.WriteString(LastOutput);
            stream.WriteString(Name);
            stream.WriteString(FilteredName);
            stream.WriteBool(ShouldTrackOutput);
            stream.WriteUnsignedInt(TickDelay);
            stream.WriteBool(ExecuteOnFirstTick);
        }

        public override void Decode(BinaryStream stream)
        {
            Block = stream.ReadBool();
            if (Block)
            {
                Position = stream.ReadBlockPosition();
                Mode = (CommandBlockMode)stream.ReadUnsignedVarInt();
                NeedsRedstone = stream.ReadBool();
                Conditional = stream.ReadBool();
            }
            else
            {
                MinecartEntityRuntimeId = stream.ReadActorRuntimeId();
            }

            Command = stream.ReadString();
            LastOutput = stream.ReadString();
            Name = stream.ReadString();
            FilteredName = stream.ReadString();
            ShouldTrackOutput = stream.ReadBool();
            TickDelay = stream.ReadUnsignedInt();
            ExecuteOnFirstTick = stream.ReadBool();
        }
    }
}
