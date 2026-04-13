using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SetEntityDataPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetEntityData;

        public ulong RuntimeEntityId { get; set; }
        public MetadataDictionary Metadata { get; set; } = new MetadataDictionary();
        public ulong Tick { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            Metadata.Encode(stream);
            stream.WriteUnsignedVarLong(Tick);
        }

        public override void Decode(BinaryStream stream)
        {
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            Metadata.Decode(stream);
            Tick = stream.ReadUnsignedVarLong();
        }
    }
}
