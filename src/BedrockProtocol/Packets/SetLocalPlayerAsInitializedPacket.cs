using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SetLocalPlayerAsInitializedPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetLocalPlayerAsInitialized;

        public ulong RuntimeEntityId { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(RuntimeEntityId);
        }

        public override void Decode(BinaryStream stream)
        {
            RuntimeEntityId = stream.ReadUnsignedVarLong();
        }
    }
}
