using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class HurtArmourPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.HurtArmour;

        public int Cause { get; set; }
        public int Damage { get; set; }
        public long ArmourSlots { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(Cause);
            stream.WriteVarInt(Damage);
            stream.WriteVarLong(ArmourSlots);
        }

        public override void Decode(BinaryStream stream)
        {
            Cause = stream.ReadVarInt();
            Damage = stream.ReadVarInt();
            ArmourSlots = stream.ReadVarLong();
        }
    }
}
