using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class RemoveEntityPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.RemoveEntity;

        public long EntityId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityId);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityId = stream.ReadVarLong();
        }
    }
}
