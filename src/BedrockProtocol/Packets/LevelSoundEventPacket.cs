using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class LevelSoundEventPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.LevelSoundEvent;

        public SoundEvent Sound { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public int ExtraData { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public bool IsBabyMob { get; set; }
        public bool IsGlobal { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Sound);
            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);
            stream.WriteVarInt(ExtraData);
            stream.WriteString(EntityType);
            stream.WriteBool(IsBabyMob);
            stream.WriteBool(IsGlobal);
        }

        public override void Decode(BinaryStream stream)
        {
            Sound = (SoundEvent)stream.ReadUnsignedVarInt();
            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();
            ExtraData = stream.ReadVarInt();
            EntityType = stream.ReadString();
            IsBabyMob = stream.ReadBool();
            IsGlobal = stream.ReadBool();
        }
    }
}
