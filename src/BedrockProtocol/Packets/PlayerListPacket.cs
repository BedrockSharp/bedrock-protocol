using System.Collections.Generic;
using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class PlayerListPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerList;

        public PlayerListAction Action { get; set; }
        public List<PlayerListEntry> Entries { get; set; } = new List<PlayerListEntry>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)Action);
            stream.WriteUnsignedVarInt((uint)Entries.Count);
            foreach (var entry in Entries)
            {
                stream.WriteUuid(entry.Uuid);
                if (Action == PlayerListAction.Add)
                {
                    stream.WriteVarLong(entry.EntityId);
                    stream.WriteString(entry.Name);
                    stream.WriteString(entry.Xuid);
                    stream.WriteString(entry.PlatformChatId);
                    stream.WriteInt(entry.BuildPlatform);
                    stream.WriteUnsignedVarInt((uint)entry.SkinData.Length);
                    stream.WriteBytes(entry.SkinData);
                    stream.WriteBool(entry.IsTeacher);
                    stream.WriteBool(entry.IsHost);
                }
            }
        }

        public override void Decode(BinaryStream stream)
        {
            Action = (PlayerListAction)stream.ReadByte();
            uint count = stream.ReadUnsignedVarInt();
            for (int i = 0; i < count; i++)
            {
                var entry = new PlayerListEntry();
                entry.Uuid = stream.ReadUuid();
                if (Action == PlayerListAction.Add)
                {
                    entry.EntityId = stream.ReadVarLong();
                    entry.Name = stream.ReadString();
                    entry.Xuid = stream.ReadString();
                    entry.PlatformChatId = stream.ReadString();
                    entry.BuildPlatform = stream.ReadInt();
                    uint skinLen = stream.ReadUnsignedVarInt();
                    entry.SkinData = stream.ReadBytes((int)skinLen);
                    entry.IsTeacher = stream.ReadBool();
                    entry.IsHost = stream.ReadBool();
                }
                Entries.Add(entry);
            }
        }
    }
}
