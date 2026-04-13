using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using BedrockProtocol.Types;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace BedrockProtocol.Packets
{
    public class StartGamePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.StartGame;

        public long ActorUniqueId { get; set; }
        public ulong ActorRuntimeId { get; set; }
        public int PlayerGamemode { get; set; }

        public float PlayerX { get; set; }
        public float PlayerY { get; set; }
        public float PlayerZ { get; set; }

        public float Pitch { get; set; }
        public float Yaw { get; set; }

        public CacheableNbt PlayerActorProperties { get; set; } = null!;
        public LevelSettings LevelSettings { get; set; } = null!;

        public string LevelId { get; set; } = string.Empty;
        public string WorldName { get; set; } = string.Empty;
        public string PremiumWorldTemplateId { get; set; } = string.Empty;
        public bool IsTrial { get; set; }

        public PlayerMovementSettings PlayerMovementSettings { get; set; } = null!;

        public ulong CurrentTick { get; set; }
        public int EnchantmentSeed { get; set; }

        public List<BlockPaletteEntry> BlockPalette { get; set; } = new();

        public string MultiplayerCorrelationId { get; set; } = string.Empty;
        public bool EnableNewInventorySystem { get; set; }

        public string ServerSoftwareVersion { get; set; } = string.Empty;

        public ulong BlockPaletteChecksum { get; set; }

        public Guid WorldTemplateId { get; set; }

        public bool EnableClientSideChunkGeneration { get; set; }
        public bool BlockNetworkIdsAreHashes { get; set; }

        public NetworkPermissions NetworkPermissions { get; set; } = null!;
        public ServerJoinInformation ServerJoinInformation { get; set; } = null!;
        public ServerTelemetryData ServerTelemetryData { get; set; } = null!;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteActorUniqueId(ActorUniqueId);
            stream.WriteActorRuntimeId(ActorRuntimeId);
            stream.WriteVarInt(PlayerGamemode);

            stream.WriteFloat(PlayerX);
            stream.WriteFloat(PlayerY);
            stream.WriteFloat(PlayerZ);

            stream.WriteFloat(Pitch);
            stream.WriteFloat(Yaw);

            LevelSettings.Encode(stream);

            stream.WriteString(LevelId);
            stream.WriteString(WorldName);
            stream.WriteString(PremiumWorldTemplateId);
            stream.WriteBool(IsTrial);

            PlayerMovementSettings.Encode(stream);

            stream.WriteUnsignedLong(CurrentTick);

            stream.WriteVarInt(EnchantmentSeed);

            stream.WriteUnsignedVarInt((uint)BlockPalette.Count);

            foreach (var entry in BlockPalette)
            {
                stream.WriteString(entry.Name);
                stream.WriteByteArray(entry.States.GetEncodedNbt());
            }

            stream.WriteString(MultiplayerCorrelationId);
            stream.WriteBool(EnableNewInventorySystem);
            stream.WriteString(ServerSoftwareVersion);

            stream.WriteByteArray(PlayerActorProperties.GetEncodedNbt());

            stream.WriteUnsignedLong(BlockPaletteChecksum);

            stream.WriteUuid(WorldTemplateId);

            stream.WriteBool(EnableClientSideChunkGeneration);
            stream.WriteBool(BlockNetworkIdsAreHashes);

            NetworkPermissions.Encode(stream);

            stream.WriteOptional(ServerJoinInformation, (s, v) => v.Encode(s));

            ServerTelemetryData.Encode(stream);
        }

        public override void Decode(BinaryStream stream)
        {
            ActorUniqueId = stream.ReadActorUniqueId();
            ActorRuntimeId = stream.ReadActorRuntimeId();
            PlayerGamemode = stream.ReadVarInt();

            PlayerX = stream.ReadFloat();
            PlayerY = stream.ReadFloat();
            PlayerZ = stream.ReadFloat();

            Pitch = stream.ReadFloat();
            Yaw = stream.ReadFloat();

            LevelSettings = new LevelSettings();
            LevelSettings.Decode(stream);

            LevelId = stream.ReadString();
            WorldName = stream.ReadString();
            PremiumWorldTemplateId = stream.ReadString();
            IsTrial = stream.ReadBool();

            PlayerMovementSettings = new PlayerMovementSettings();
            PlayerMovementSettings.Decode(stream);

            CurrentTick = stream.ReadUnsignedLong();

            EnchantmentSeed = stream.ReadVarInt();

            uint count = stream.ReadUnsignedVarInt();

            BlockPalette.Clear();

            for (int i = 0; i < count; i++)
            {
                var name = stream.ReadString();
                var nbt = stream.ReadByteArray();

                BlockPalette.Add(new BlockPaletteEntry(name, new CacheableNbt(nbt)));
            }

            MultiplayerCorrelationId = stream.ReadString();
            EnableNewInventorySystem = stream.ReadBool();
            ServerSoftwareVersion = stream.ReadString();

            PlayerActorProperties = new CacheableNbt(stream.ReadByteArray());

            BlockPaletteChecksum = stream.ReadUnsignedLong();

            WorldTemplateId = stream.ReadUuid();

            EnableClientSideChunkGeneration = stream.ReadBool();
            BlockNetworkIdsAreHashes = stream.ReadBool();

            NetworkPermissions = new NetworkPermissions();
            NetworkPermissions.Decode(stream);

            ServerJoinInformation = stream.ReadOptional<ServerJoinInformation>();

            ServerTelemetryData = new ServerTelemetryData();
            ServerTelemetryData.Decode(stream);
        }
    }
}