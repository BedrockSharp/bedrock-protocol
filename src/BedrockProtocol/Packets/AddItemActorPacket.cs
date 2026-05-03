using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using BedrockProtocol.Types;

namespace BedrockProtocol.Packets
{
    public class AddItemActorPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AddItemActor;

        public long EntityUniqueId { get; set; }
        public ulong EntityRuntimeId { get; set; }
        public ItemInstance Item { get; set; } = new ItemInstance();
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public MetadataDictionary Metadata { get; set; } = new MetadataDictionary();
        public bool FromFishing { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityUniqueId);
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            Item.Encode(stream);
            stream.WriteVector3(Position);
            stream.WriteVector3(Velocity);
            Metadata.Encode(stream);
            stream.WriteBool(FromFishing);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityUniqueId = stream.ReadVarLong();
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            Item.Decode(stream);
            Position = stream.ReadVector3();
            Velocity = stream.ReadVector3();
            Metadata.Decode(stream);
            FromFishing = stream.ReadBool();
        }
    }
}
