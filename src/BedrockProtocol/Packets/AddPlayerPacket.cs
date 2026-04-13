using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class AddPlayerPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.AddPlayer;

        public string Uuid { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public ulong EntityId { get; set; }
        public ulong RuntimeEntityId { get; set; }
        public string PlatformChatId { get; set; } = string.Empty;
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float VelocityZ { get; set; }
        public float Pitch { get; set; }
        public float HeadYaw { get; set; }
        public float Yaw { get; set; }
        
        public byte[] MetadataPayload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteString(Uuid);
            stream.WriteString(Username);
            stream.WriteUnsignedVarLong(EntityId);
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteString(PlatformChatId);
            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);
            stream.WriteFloat(VelocityX);
            stream.WriteFloat(VelocityY);
            stream.WriteFloat(VelocityZ);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(HeadYaw);
            stream.WriteFloat(Yaw);
            stream.WriteUnsignedVarInt((uint)MetadataPayload.Length);
            stream.WriteBytes(MetadataPayload);
        }

        public override void Decode(BinaryStream stream)
        {
            Uuid = stream.ReadString();
            Username = stream.ReadString();
            EntityId = stream.ReadUnsignedVarLong();
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            PlatformChatId = stream.ReadString();
            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();
            VelocityX = stream.ReadFloat();
            VelocityY = stream.ReadFloat();
            VelocityZ = stream.ReadFloat();
            Pitch = stream.ReadFloat();
            HeadYaw = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            uint len = stream.ReadUnsignedVarInt();
            MetadataPayload = stream.ReadBytes((int)len);
        }
    }
}
