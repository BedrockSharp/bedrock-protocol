using BedrockProtocol.Utils;
using Nbt;

namespace BedrockProtocol.Packets
{
    public class SyncActorPropertyPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SyncActorProperty;

        public CompoundTag? PropertyData { get; set; }

        public override void Encode(BinaryStream stream)
        {
            if (PropertyData != null)
            {
                stream.WriteNbt(PropertyData);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            PropertyData = stream.ReadNetworkNbt();
        }
    }
}
