using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class AddMobPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AddMob;

        public long EntityId { get; set; }
        public ulong RuntimeEntityId { get; set; }
        public string Type { get; set; } = string.Empty;
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float VelocityZ { get; set; }
        public float Pitch { get; set; }
        public float HeadYaw { get; set; }
        public float Yaw { get; set; }
        public MetadataDictionary Metadata { get; set; } = new MetadataDictionary();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityId);
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteString(Type);
            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);
            stream.WriteFloat(VelocityX);
            stream.WriteFloat(VelocityY);
            stream.WriteFloat(VelocityZ);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(HeadYaw);
            stream.WriteFloat(Yaw);
            stream.WriteUnsignedVarInt(0); // Attributes
            Metadata.Encode(stream);
            stream.WriteUnsignedVarInt(0); // Links
        }

        public override void Decode(BinaryStream stream)
        {
            EntityId = stream.ReadVarLong();
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            Type = stream.ReadString();
            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();
            VelocityX = stream.ReadFloat();
            VelocityY = stream.ReadFloat();
            VelocityZ = stream.ReadFloat();
            Pitch = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            stream.ReadUnsignedVarInt();
            Metadata.Decode(stream);
            stream.ReadUnsignedVarInt();
        }
    }
}
