using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class StartGamePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.StartGame;

        public long EntityId { get; set; }
        public ulong RuntimeEntityId { get; set; }
        public int PlayerGamemode { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public LevelSettings LevelSettings { get; set; } = new LevelSettings();
        public string LevelId { get; set; } = string.Empty;
        public string WorldName { get; set; } = string.Empty;
        public string PremiumWorldTemplateId { get; set; } = string.Empty;
        public bool IsTrial { get; set; }
        public int MovementType { get; set; }
        public int RewindHistorySize { get; set; }
        public bool ServerAuthoritativeBlockBreaking { get; set; }
        public long CurrentTick { get; set; }
        public int EnchantmentSeed { get; set; }
        public string BlockProperties { get; set; } = string.Empty;
        public string ItemStates { get; set; } = string.Empty;
        public string MultiplayerCorrelationId { get; set; } = string.Empty;
        
        public byte[] NbtPayload { get; set; } = new byte[0];

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(EntityId);
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteVarInt(PlayerGamemode);
            stream.WriteFloat(X);
            stream.WriteFloat(Y);
            stream.WriteFloat(Z);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);
            LevelSettings.Encode(stream);
            stream.WriteUnsignedVarInt(0); 
            stream.WriteUnsignedVarInt(0); 
            stream.WriteBool(false); 
            stream.WriteString(LevelId);
            stream.WriteString(WorldName);
            stream.WriteString(PremiumWorldTemplateId);
            stream.WriteBool(IsTrial);
            stream.WriteVarInt(MovementType);
            stream.WriteVarInt(RewindHistorySize);
            stream.WriteBool(ServerAuthoritativeBlockBreaking);
            stream.WriteVarLong(CurrentTick);
            stream.WriteVarInt(EnchantmentSeed);
            stream.WriteUnsignedVarInt((uint)NbtPayload.Length);
            stream.WriteBytes(NbtPayload);
            stream.WriteString(MultiplayerCorrelationId);
            stream.WriteBool(false);
            stream.WriteString(string.Empty);
            stream.WriteString(string.Empty);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityId = stream.ReadVarLong();
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            PlayerGamemode = stream.ReadVarInt();
            X = stream.ReadFloat();
            Y = stream.ReadFloat();
            Z = stream.ReadFloat();
            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();
            LevelSettings.Decode(stream);
            stream.ReadUnsignedVarInt();
            stream.ReadUnsignedVarInt();
            stream.ReadBool();
            LevelId = stream.ReadString();
            WorldName = stream.ReadString();
            PremiumWorldTemplateId = stream.ReadString();
            IsTrial = stream.ReadBool();
            MovementType = stream.ReadVarInt();
            RewindHistorySize = stream.ReadVarInt();
            ServerAuthoritativeBlockBreaking = stream.ReadBool();
            CurrentTick = stream.ReadVarLong();
            EnchantmentSeed = stream.ReadVarInt();
            uint nbtLen = stream.ReadUnsignedVarInt();
            NbtPayload = stream.ReadBytes((int)nbtLen);
            MultiplayerCorrelationId = stream.ReadString();
            stream.ReadBool();
            stream.ReadString();
            stream.ReadString();
        }
    }
}
