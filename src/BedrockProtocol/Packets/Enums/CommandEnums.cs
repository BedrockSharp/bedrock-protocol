namespace BedrockProtocol.Packets.Enums
{
    public enum CommandPermissionLevel : byte
    {
        Any = 0,
        GameDirectors = 1,
        Admin = 2,
        Host = 3,
        Owner = 4,
        Internal = 5
    }

    public enum CommandOriginType : uint
    {
        Player = 0,
        Block = 1,
        MinecartBlock = 2,
        DevConsole = 3,
        Test = 4,
        AutomationPlayer = 5,
        ClientAutomation = 6,
        DedicatedServer = 7,
        Entity = 8,
        Virtual = 9,
        GameArgument = 10,
        EntityServer = 11,
        Precompiled = 12,
        GameDirectorEntityServer = 13,
        Script = 14,
        Executor = 15
    }

    public enum CommandOutputType : byte
    {
        None = 0,
        LastOutput = 1,
        Silent = 2,
        AllOutput = 3,
        DataSet = 4
    }

    public enum CommandBlockMode : uint
    {
        Impulse = 0,
        Repeating = 1,
        Chain = 2
    }

    public enum CommandArgConstraint : byte
    {
        CheatsEnabled = 0,
        OperatorPermissions = 1,
        HostPermissions = 2
    }
}
