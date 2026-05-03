namespace BedrockProtocol.Packets.Enums
{
    public enum AdventureFlag : uint
    {
        WorldImmutable = 1 << 0,
        NoPvM = 1 << 1,
        NoMvP = 1 << 2,
        Unused = 1 << 3,
        ShowNameTags = 1 << 4,
        AutoJump = 1 << 5,
        AllowFlight = 1 << 6,
        NoClip = 1 << 7,
        WorldBuilder = 1 << 8,
        Flying = 1 << 9,
        Muted = 1 << 10
    }

    public enum ActionPermission : uint
    {
        Mine = 1 << 0,
        DoorsAndSwitches = 1 << 1,
        OpenContainers = 1 << 2,
        AttackPlayers = 1 << 3,
        AttackMobs = 1 << 4,
        Operator = 1 << 5,
        Teleport = 1 << 6,
        Build = 1 << 7,
        Default = 1 << 8
    }

    public enum PermissionLevel : uint
    {
        Visitor = 0,
        Member = 1,
        Operator = 2,
        Custom = 3
    }

    public enum AbilityLayerType : ushort
    {
        CustomCache = 0,
        Base = 1,
        Spectator = 2,
        Commands = 3,
        Editor = 4,
        LoadingScreen = 5
    }

    public enum Ability : uint
    {
        Build = 1 << 0,
        Mine = 1 << 1,
        DoorsAndSwitches = 1 << 2,
        OpenContainers = 1 << 3,
        AttackPlayers = 1 << 4,
        AttackMobs = 1 << 5,
        OperatorCommands = 1 << 6,
        Teleport = 1 << 7,
        Invulnerable = 1 << 8,
        Flying = 1 << 9,
        MayFly = 1 << 10,
        InstantBuild = 1 << 11,
        Lightning = 1 << 12,
        FlySpeed = 1 << 13,
        WalkSpeed = 1 << 14,
        Muted = 1 << 15,
        WorldBuilder = 1 << 16,
        NoClip = 1 << 17,
        PrivilegedBuilder = 1 << 18,
        VerticalFlySpeed = 1 << 19
    }
}
