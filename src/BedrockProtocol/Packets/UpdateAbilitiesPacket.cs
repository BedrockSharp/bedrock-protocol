using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;

namespace BedrockProtocol.Packets
{
    public class UpdateAbilitiesPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UpdateAbilities;

        public AbilityData AbilityData { get; set; } = new AbilityData();

        public override void Encode(BinaryStream stream)
        {
            AbilityData.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            AbilityData.Decode(stream);
        }
    }
}
