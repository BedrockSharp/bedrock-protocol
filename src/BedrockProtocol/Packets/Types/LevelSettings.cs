using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets.Types
{
    public class LevelSettings
    {
        public ulong Seed { get; set; }
        public short SpawnBiomeType { get; set; }
        public string CustomBiomeName { get; set; } = string.Empty;
        public int Dimension { get; set; }
        public int Generator { get; set; }
        public int WorldGamemode { get; set; }
        public int Difficulty { get; set; }
        public int BlockSpX { get; set; }
        public int BlockSpY { get; set; }
        public int BlockSpZ { get; set; }
        public bool AchievementsDisabled { get; set; }
        public int DayCycleStopTime { get; set; }
        public int EduEditionOffer { get; set; }
        public bool EduFeaturesEnabled { get; set; }
        public string EduProductUuid { get; set; } = string.Empty;
        public float RainLevel { get; set; }
        public float LightningLevel { get; set; }
        public bool HasConfirmedPlatformLockedContent { get; set; }
        public bool IsMultiplayer { get; set; }
        public bool BroadcastToLan { get; set; }
        public int XblBroadcastMode { get; set; }
        public int PlatformBroadcastMode { get; set; }
        public bool CommandsEnabled { get; set; }
        public bool IsTexturepacksRequired { get; set; }
        
        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(Seed);
            stream.WriteShort(SpawnBiomeType);
            stream.WriteString(CustomBiomeName);
            stream.WriteVarInt(Dimension);
            stream.WriteVarInt(Generator);
            stream.WriteVarInt(WorldGamemode);
            stream.WriteVarInt(Difficulty);
            stream.WriteVarInt(BlockSpX);
            stream.WriteUnsignedVarInt((uint)BlockSpY);
            stream.WriteVarInt(BlockSpZ);
            stream.WriteBool(AchievementsDisabled);
            stream.WriteVarInt(DayCycleStopTime);
            stream.WriteVarInt(EduEditionOffer);
            stream.WriteBool(EduFeaturesEnabled);
            stream.WriteString(EduProductUuid);
            stream.WriteFloat(RainLevel);
            stream.WriteFloat(LightningLevel);
            stream.WriteBool(HasConfirmedPlatformLockedContent);
            stream.WriteBool(IsMultiplayer);
            stream.WriteBool(BroadcastToLan);
            stream.WriteVarInt(XblBroadcastMode);
            stream.WriteVarInt(PlatformBroadcastMode);
            stream.WriteBool(CommandsEnabled);
            stream.WriteBool(IsTexturepacksRequired);
        }

        public void Decode(BinaryStream stream)
        {
            Seed = stream.ReadUnsignedVarLong();
            SpawnBiomeType = stream.ReadShort();
            CustomBiomeName = stream.ReadString();
            Dimension = stream.ReadVarInt();
            Generator = stream.ReadVarInt();
            WorldGamemode = stream.ReadVarInt();
            Difficulty = stream.ReadVarInt();
            BlockSpX = stream.ReadVarInt();
            BlockSpY = (int)stream.ReadUnsignedVarInt();
            BlockSpZ = stream.ReadVarInt();
            AchievementsDisabled = stream.ReadBool();
            DayCycleStopTime = stream.ReadVarInt();
            EduEditionOffer = stream.ReadVarInt();
            EduFeaturesEnabled = stream.ReadBool();
            EduProductUuid = stream.ReadString();
            RainLevel = stream.ReadFloat();
            LightningLevel = stream.ReadFloat();
            HasConfirmedPlatformLockedContent = stream.ReadBool();
            IsMultiplayer = stream.ReadBool();
            BroadcastToLan = stream.ReadBool();
            XblBroadcastMode = stream.ReadVarInt();
            PlatformBroadcastMode = stream.ReadVarInt();
            CommandsEnabled = stream.ReadBool();
            IsTexturepacksRequired = stream.ReadBool();
        }
    }
}
