using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public enum GameRule
    {
        CommandBlocksEnabled,
        CommandBlockOutput,
        DoDaylightCycle,
        DoEntityDrops,
        DoFireTick,
        DoInsomnia,
        DoImmediateRespawn,
        DoMobLoot,
        DoMobSpawning,
        DoTileDrops,
        DoWeatherCycle,
        DrowningDamage,
        FallDamage,
        FireDamage,
        FreezeDamage,
        FunctionCommandLimit,
        KeepInventory,
        MaxCommandChainLength,
        MobGriefing,
        NaturalRegeneration,
        Pvp,
        RandomTickSpeed,
        SendCommandFeedback,
        ShowCoordinates,
        ShowDeathMessages,
        SpawnRadius,
        TntExplodes,
        ProjectilesCanBreakBlocks,
        TntExplosionDropDecay,
        ShowTags,
        ExperimentalGameplay,
        PlayersSleepingPercentage,
        DoLimitedCrafting,
        RespawnBlocksExplode,
        ShowBorderEffect,
        ShowDaysPlayed,
        LocatorBar,
        RecipesUnlock
    }

    public enum GameRuleType : uint
    {
        Boolean = 1,
        Integer = 2,
        Float = 3
    }

    public class GameRuleValue
    {
        public GameRuleType Type { get; set; }
        public object Value { get; set; }
        public bool CanBeChanged { get; set; }

        public GameRuleValue(GameRuleType type, object value, bool canBeChanged = true)
        {
            Type = type;
            Value = value;
            CanBeChanged = canBeChanged;
        }

        public void Write(BinaryStream stream, bool isStartGame = false)
        {
            stream.WriteBool(CanBeChanged);
            stream.WriteUnsignedVarInt((uint)Type);

            switch (Type)
            {
                case GameRuleType.Boolean:
                    stream.WriteBool((bool)Value);
                    break;
                case GameRuleType.Integer:
                    if (isStartGame)
                        stream.WriteVarInt((int)Value);
                    else
                        stream.WriteInt((int)Value);
                    break;
                case GameRuleType.Float:
                    stream.WriteFloat((float)Value);
                    break;
            }
        }
    }

    public class GameRules
    {
        public Dictionary<GameRule, GameRuleValue> Rules { get; } = new();

        public static GameRules GetDefault()
        {
            var gr = new GameRules();
            gr.Rules[GameRule.CommandBlocksEnabled] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.CommandBlockOutput] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoDaylightCycle] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoEntityDrops] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoFireTick] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoInsomnia] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoImmediateRespawn] = new GameRuleValue(GameRuleType.Boolean, false);
            gr.Rules[GameRule.DoMobLoot] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoMobSpawning] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoTileDrops] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DoWeatherCycle] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.DrowningDamage] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.FallDamage] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.FireDamage] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.FreezeDamage] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.FunctionCommandLimit] = new GameRuleValue(GameRuleType.Integer, 10000);
            gr.Rules[GameRule.KeepInventory] = new GameRuleValue(GameRuleType.Boolean, false);
            gr.Rules[GameRule.MaxCommandChainLength] = new GameRuleValue(GameRuleType.Integer, 65536);
            gr.Rules[GameRule.MobGriefing] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.NaturalRegeneration] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.Pvp] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.RandomTickSpeed] = new GameRuleValue(GameRuleType.Integer, 3);
            gr.Rules[GameRule.SendCommandFeedback] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ShowCoordinates] = new GameRuleValue(GameRuleType.Boolean, false);
            gr.Rules[GameRule.ShowDeathMessages] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.SpawnRadius] = new GameRuleValue(GameRuleType.Integer, 5);
            gr.Rules[GameRule.TntExplodes] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ProjectilesCanBreakBlocks] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.TntExplosionDropDecay] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ShowTags] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ExperimentalGameplay] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.PlayersSleepingPercentage] = new GameRuleValue(GameRuleType.Integer, 100);
            gr.Rules[GameRule.DoLimitedCrafting] = new GameRuleValue(GameRuleType.Boolean, false);
            gr.Rules[GameRule.RespawnBlocksExplode] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ShowBorderEffect] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.ShowDaysPlayed] = new GameRuleValue(GameRuleType.Boolean, false);
            gr.Rules[GameRule.LocatorBar] = new GameRuleValue(GameRuleType.Boolean, true);
            gr.Rules[GameRule.RecipesUnlock] = new GameRuleValue(GameRuleType.Boolean, false);
            return gr;
        }

        public static string GetName(GameRule rule)
        {
            return rule switch
            {
                GameRule.CommandBlocksEnabled => "commandBlocksEnabled",
                GameRule.CommandBlockOutput => "commandBlockOutput",
                GameRule.DoDaylightCycle => "doDaylightCycle",
                GameRule.DoEntityDrops => "doEntityDrops",
                GameRule.DoFireTick => "doFireTick",
                GameRule.DoInsomnia => "doInsomnia",
                GameRule.DoImmediateRespawn => "doImmediateRespawn",
                GameRule.DoMobLoot => "doMobLoot",
                GameRule.DoMobSpawning => "doMobSpawning",
                GameRule.DoTileDrops => "doTileDrops",
                GameRule.DoWeatherCycle => "doWeatherCycle",
                GameRule.DrowningDamage => "drowningDamage",
                GameRule.FallDamage => "fallDamage",
                GameRule.FireDamage => "fireDamage",
                GameRule.FreezeDamage => "freezeDamage",
                GameRule.FunctionCommandLimit => "functionCommandLimit",
                GameRule.KeepInventory => "keepInventory",
                GameRule.MaxCommandChainLength => "maxCommandChainLength",
                GameRule.MobGriefing => "mobGriefing",
                GameRule.NaturalRegeneration => "naturalRegeneration",
                GameRule.Pvp => "pvp",
                GameRule.RandomTickSpeed => "randomTickSpeed",
                GameRule.SendCommandFeedback => "sendCommandFeedback",
                GameRule.ShowCoordinates => "showCoordinates",
                GameRule.ShowDeathMessages => "showDeathMessages",
                GameRule.SpawnRadius => "spawnRadius",
                GameRule.TntExplodes => "tntExplodes",
                GameRule.ProjectilesCanBreakBlocks => "projectilesCanBreakBlocks",
                GameRule.TntExplosionDropDecay => "tntExplosionDropDecay",
                GameRule.ShowTags => "showTags",
                GameRule.ExperimentalGameplay => "experimentalGameplay",
                GameRule.PlayersSleepingPercentage => "playersSleepingPercentage",
                GameRule.DoLimitedCrafting => "doLimitedCrafting",
                GameRule.RespawnBlocksExplode => "respawnBlocksExplode",
                GameRule.ShowBorderEffect => "showBorderEffect",
                GameRule.ShowDaysPlayed => "showDaysPlayed",
                GameRule.LocatorBar => "locatorBar",
                GameRule.RecipesUnlock => "recipesUnlock",
                _ => rule.ToString().ToLower()
            };
        }

        public void WriteStartGame(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Rules.Count);
            foreach (var rule in Rules)
            {
                stream.WriteString(GetName(rule.Key));
                rule.Value.Write(stream, true);
            }
        }
    }
}
