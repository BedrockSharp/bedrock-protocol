using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class AvailableActorIdentifiersPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AvailableActorIdentifiers;

        public byte[] SerialisedEntityIdentifiers { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBytes(SerialisedEntityIdentifiers);
        }

        public override void Decode(BinaryStream stream)
        {
            SerialisedEntityIdentifiers = stream.ReadRemainingBytes();
        }
    }
}
