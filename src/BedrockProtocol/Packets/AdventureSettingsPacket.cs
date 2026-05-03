using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;

namespace BedrockProtocol.Packets
{
    public class AdventureSettingsPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AdventureSettings;

        public uint Flags { get; set; }
        public uint CommandPermissionLevel { get; set; }
        public uint ActionPermissions { get; set; }
        public uint PermissionLevel { get; set; }
        public uint CustomStoredPermissions { get; set; }
        public long PlayerUniqueId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(Flags);
            stream.WriteUnsignedVarInt(CommandPermissionLevel);
            stream.WriteUnsignedVarInt(ActionPermissions);
            stream.WriteUnsignedVarInt(PermissionLevel);
            stream.WriteUnsignedVarInt(CustomStoredPermissions);
            stream.WriteLong(PlayerUniqueId);
        }

        public override void Decode(BinaryStream stream)
        {
            Flags = stream.ReadUnsignedVarInt();
            CommandPermissionLevel = stream.ReadUnsignedVarInt();
            ActionPermissions = stream.ReadUnsignedVarInt();
            PermissionLevel = stream.ReadUnsignedVarInt();
            CustomStoredPermissions = stream.ReadUnsignedVarInt();
            PlayerUniqueId = stream.ReadLong();
        }
    }
}
