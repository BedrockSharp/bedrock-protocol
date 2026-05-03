using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using BedrockProtocol.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class AddActorPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AddActor;

        public long EntityUniqueId { get; set; }
        public ulong EntityRuntimeId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float HeadYaw { get; set; }
        public float BodyYaw { get; set; }
        public List<NetworkAttributeValue> Attributes { get; set; } = new List<NetworkAttributeValue>();
        public MetadataDictionary Metadata { get; set; } = new MetadataDictionary();
        public List<EntityLink> EntityLinks { get; set; } = new List<EntityLink>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityUniqueId);
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteString(EntityType);
            stream.WriteVector3(Position);
            stream.WriteVector3(Velocity);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            stream.WriteFloat(HeadYaw);
            stream.WriteFloat(BodyYaw);
            
            stream.WriteUnsignedVarInt((uint)Attributes.Count);
            foreach (var attribute in Attributes)
            {
                attribute.Encode(stream);
            }

            Metadata.Encode(stream);

            stream.WriteUnsignedVarInt((uint)EntityLinks.Count);
            foreach (var link in EntityLinks)
            {
                link.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            EntityUniqueId = stream.ReadVarLong();
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            EntityType = stream.ReadString();
            Position = stream.ReadVector3();
            Velocity = stream.ReadVector3();
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
            BodyYaw = stream.ReadFloat();

            uint attributeCount = stream.ReadUnsignedVarInt();
            Attributes = new List<NetworkAttributeValue>((int)attributeCount);
            for (int i = 0; i < attributeCount; i++)
            {
                var attr = new NetworkAttributeValue();
                attr.Decode(stream);
                Attributes.Add(attr);
            }

            Metadata.Decode(stream);

            uint linkCount = stream.ReadUnsignedVarInt();
            EntityLinks = new List<EntityLink>((int)linkCount);
            for (int i = 0; i < linkCount; i++)
            {
                var link = new EntityLink();
                link.Decode(stream);
                EntityLinks.Add(link);
            }
        }
    }
}
