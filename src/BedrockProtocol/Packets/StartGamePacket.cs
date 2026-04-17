using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using Nbt;
using System;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class StartGamePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.StartGame;

        public long EntityUniqueId { get; set; }
        public ulong EntityRuntimeId { get; set; }
        public int PlayerGamemode { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }

        public long Seed { get; set; }
        public short SpawnBiomeType { get; set; } = 0;
        public string UserDefinedBiomeName { get; set; } = "plains";
        public int Dimension { get; set; }
        public int Generator { get; set; } = 1;
        public int WorldGamemode { get; set; }
        public int Difficulty { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }
        public bool HasAchievementsDisabled { get; set; } = true;
        public int DayCycleStopTime { get; set; } = -1;
        public int EduEditionOffer { get; set; } = 0;
        public bool HasEduFeaturesEnabled { get; set; } = false;
        public string EducationProductId { get; set; } = "";
        public float RainLevel { get; set; }
        public float LightningLevel { get; set; }
        public bool HasConfirmedPlatformLockedContent { get; set; } = false;
        public bool MultiplayerGame { get; set; } = true;
        public bool BroadcastToLan { get; set; } = true;
        public int XblBroadcastIntent { get; set; } = 4; // Public
        public int PlatformBroadcastIntent { get; set; } = 4; // Public
        public bool CommandsEnabled { get; set; }
        public bool IsTexturePacksRequired { get; set; } = false;
        public GameRules GameRules { get; set; } = GameRules.GetDefault();
        public List<ExperimentEntry> Experiments { get; } = new();
        public bool ExperimentsPreviouslyToggled { get; set; } = false;
        public bool BonusChest { get; set; } = false;
        public bool HasStartWithMapEnabled { get; set; } = false;
        public int PermissionLevel { get; set; } = 1;
        public int ServerChunkTickRange { get; set; } = 4;
        public bool HasLockedBehaviorPack { get; set; } = false;
        public bool HasLockedResourcePack { get; set; } = false;
        public bool IsFromLockedWorldTemplate { get; set; } = false;
        public bool IsUsingMsaGamertagsOnly { get; set; } = false;
        public bool IsFromWorldTemplate { get; set; } = false;
        public bool IsWorldTemplateOptionLocked { get; set; } = false;
        public bool IsOnlySpawningV1Villagers { get; set; } = false;
        public string VanillaVersion { get; set; } = "*";
        public int LimitedWorldWidth { get; set; } = 16;
        public int LimitedWorldDepth { get; set; } = 16;
        public bool NewNether { get; set; } = false;
        public string EduSharedResourceButtonName { get; set; } = "";
        public string EduSharedResourceLinkUri { get; set; } = "";
        public bool ForceExperimentalGameplay { get; set; } = false;
        public byte ChatRestrictionLevel { get; set; }
        public bool DisablePlayerInteractions { get; set; }
        public bool IsDisablingPersonas { get; set; }
        public bool IsDisablingCustomSkins { get; set; }

        public string LevelId { get; set; } = "";
        public string WorldName { get; set; } = "World";
        public string PremiumWorldTemplateId { get; set; } = "";
        public bool IsTrial { get; set; } = false;

        public int RewindHistorySize { get; set; } = 0;
        public bool IsServerAuthoritativeBlockBreaking { get; set; }

        public long CurrentTick { get; set; }
        public int EnchantmentSeed { get; set; }
        public List<CustomBlockDefinition> BlockProperties { get; } = new();
        public string MultiplayerCorrelationId { get; set; } = "";
        public bool IsInventoryServerAuthoritative { get; set; }
        public string ServerEngine { get; set; } = "*";
        public CompoundTag PlayerPropertyData { get; set; } = new CompoundTag();
        public long BlockRegistryChecksum { get; set; } = 0;
        public Guid WorldTemplateId { get; set; } = Guid.Empty;

        public bool ClientSideGenerationEnabled { get; set; }
        public bool BlockNetworkIdsHashed { get; set; }
        public bool IsSoundsServerAuthoritative { get; set; }

        public bool IsHardcore { get; set; } = false;
        public int EditorWorldType { get; set; } = 0;
        public bool CreatedInEditor { get; set; }
        public bool ExportedFromEditor { get; set; }
        public bool MuteEmoteAnnouncements { get; set; }

        public string ServerId { get; set; } = "";
        public string ScenarioId { get; set; } = "";
        public string WorldId { get; set; } = "";
        public string OwnerId { get; set; } = "";

        public bool HasServerJoinInformation { get; set; } = false;
        public ServerJoinInfo? ServerJoinInfo { get; set; }

        public override void Decode(BinaryStream stream) { }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteActorUniqueId(EntityUniqueId);
            stream.WriteActorRuntimeId(EntityRuntimeId);
            stream.WriteVarInt(PlayerGamemode);
            stream.WriteVector3(X, Y, Z);
            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);

            WriteLevelSettings(stream);

            stream.WriteString(LevelId);
            stream.WriteString(WorldName);
            stream.WriteString(PremiumWorldTemplateId);
            stream.WriteBool(IsTrial);

            stream.WriteVarInt(RewindHistorySize);
            stream.WriteBool(IsServerAuthoritativeBlockBreaking);

            stream.WriteLong(CurrentTick);
            stream.WriteVarInt(EnchantmentSeed);

            stream.WriteUnsignedVarInt((uint)BlockProperties.Count);
            foreach (var block in BlockProperties)
            {
                stream.WriteString(block.Identifier);
                stream.WriteNetworkNbt(block.Nbt);
            }

            stream.WriteString(MultiplayerCorrelationId);
            stream.WriteBool(IsInventoryServerAuthoritative);
            stream.WriteString(ServerEngine);

            stream.WriteNetworkNbt(PlayerPropertyData);

            stream.WriteLong(BlockRegistryChecksum);
            stream.WriteUuid(WorldTemplateId);

            stream.WriteBool(ClientSideGenerationEnabled);
            stream.WriteBool(BlockNetworkIdsHashed);
            stream.WriteBool(IsSoundsServerAuthoritative);

            stream.WriteBool(HasServerJoinInformation);
            if (HasServerJoinInformation && ServerJoinInfo != null)
            {
                stream.WriteBool(ServerJoinInfo.GatheringJoinInfo != null);
                if (ServerJoinInfo.GatheringJoinInfo != null)
                {
                    WriteGatheringJoinInfo(stream, ServerJoinInfo.GatheringJoinInfo);
                }
                stream.WriteBool(ServerJoinInfo.StoreEntryPointInfo != null);
                if (ServerJoinInfo.StoreEntryPointInfo != null)
                {
                    WriteStoreEntryPointInfo(stream, ServerJoinInfo.StoreEntryPointInfo);
                }
                stream.WriteBool(ServerJoinInfo.PresenceInfo != null);
                if (ServerJoinInfo.PresenceInfo != null)
                {
                    WritePresenceInfo(stream, ServerJoinInfo.PresenceInfo);
                }
            }

            stream.WriteString(ServerId);
            stream.WriteString(ScenarioId);
            stream.WriteString(WorldId);
            stream.WriteString(OwnerId);
        }

        private void WriteLevelSettings(BinaryStream stream)
        {
            stream.WriteLong(Seed);
            stream.WriteShort(SpawnBiomeType);
            stream.WriteString(UserDefinedBiomeName);
            stream.WriteVarInt(Dimension);
            stream.WriteVarInt(Generator);
            stream.WriteVarInt(WorldGamemode);
            stream.WriteBool(IsHardcore);
            stream.WriteVarInt(Difficulty);
            stream.WriteBlockVector3(SpawnX, SpawnY, SpawnZ);
            stream.WriteBool(HasAchievementsDisabled);
            stream.WriteVarInt(EditorWorldType);
            stream.WriteBool(CreatedInEditor);
            stream.WriteBool(ExportedFromEditor);
            stream.WriteVarInt(DayCycleStopTime);
            stream.WriteVarInt(EduEditionOffer);
            stream.WriteBool(HasEduFeaturesEnabled);
            stream.WriteString(EducationProductId);
            stream.WriteFloat(RainLevel);
            stream.WriteFloat(LightningLevel);
            stream.WriteBool(HasConfirmedPlatformLockedContent);
            stream.WriteBool(MultiplayerGame);
            stream.WriteBool(BroadcastToLan);
            stream.WriteVarInt(XblBroadcastIntent);
            stream.WriteVarInt(PlatformBroadcastIntent);
            stream.WriteBool(CommandsEnabled);
            stream.WriteBool(IsTexturePacksRequired);

            GameRules.WriteStartGame(stream);

            stream.WriteExperiments(Experiments);
            stream.WriteBool(ExperimentsPreviouslyToggled);

            stream.WriteBool(BonusChest);
            stream.WriteBool(HasStartWithMapEnabled);
            stream.WriteVarInt(PermissionLevel);
            stream.WriteInt(ServerChunkTickRange);
            stream.WriteBool(HasLockedBehaviorPack);
            stream.WriteBool(HasLockedResourcePack);
            stream.WriteBool(IsFromLockedWorldTemplate);
            stream.WriteBool(IsUsingMsaGamertagsOnly);
            stream.WriteBool(IsFromWorldTemplate);
            stream.WriteBool(IsWorldTemplateOptionLocked);
            stream.WriteBool(IsOnlySpawningV1Villagers);
            stream.WriteBool(IsDisablingPersonas);
            stream.WriteBool(IsDisablingCustomSkins);
            stream.WriteBool(MuteEmoteAnnouncements);
            stream.WriteString(VanillaVersion);
            stream.WriteInt(LimitedWorldWidth);
            stream.WriteInt(LimitedWorldDepth);
            stream.WriteBool(NewNether);

            stream.WriteString(EduSharedResourceButtonName);
            stream.WriteString(EduSharedResourceLinkUri);

            stream.WriteBool(ForceExperimentalGameplay);

            stream.WriteByte(ChatRestrictionLevel);
            stream.WriteBool(DisablePlayerInteractions);
        }

        private void WriteGatheringJoinInfo(BinaryStream stream, GatheringJoinInfo info)
        {
            stream.WriteUuid(info.ExperienceId);
            stream.WriteString(info.ExperienceName);
            stream.WriteUuid(info.ExperienceWorldId);
            stream.WriteString(info.ExperienceWorldName);
            stream.WriteString(info.CreatorId);
            stream.WriteUuid(info.Unk);
            stream.WriteUuid(info.Unk1);
            stream.WriteString(info.ServerId);
        }

        private void WriteStoreEntryPointInfo(BinaryStream stream, StoreEntryPointInfo info)
        {
            stream.WriteString(info.StoreId);
            stream.WriteString(info.StoreName);
        }

        private void WritePresenceInfo(BinaryStream stream, PresenceInfo info)
        {
            stream.WriteString(info.ExperienceName);
            stream.WriteString(info.WorldName);
        }
    }
}
