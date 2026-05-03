using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class PlayerArmourDamagePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerArmourDamage;

        public byte Flags { get; set; }
        public int HelmetDamage { get; set; }
        public int ChestplateDamage { get; set; }
        public int LeggingsDamage { get; set; }
        public int BootsDamage { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte(Flags);
            if ((Flags & 1) != 0) stream.WriteVarInt(HelmetDamage);
            if ((Flags & 2) != 0) stream.WriteVarInt(ChestplateDamage);
            if ((Flags & 4) != 0) stream.WriteVarInt(LeggingsDamage);
            if ((Flags & 8) != 0) stream.WriteVarInt(BootsDamage);
        }

        public override void Decode(BinaryStream stream)
        {
            Flags = stream.ReadByte();
            if ((Flags & 1) != 0) HelmetDamage = stream.ReadVarInt();
            if ((Flags & 2) != 0) ChestplateDamage = stream.ReadVarInt();
            if ((Flags & 4) != 0) LeggingsDamage = stream.ReadVarInt();
            if ((Flags & 8) != 0) BootsDamage = stream.ReadVarInt();
        }
    }
}
