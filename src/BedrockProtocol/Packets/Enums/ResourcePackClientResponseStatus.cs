namespace BedrockProtocol.Packets.Enums
{
    public enum ResourcePackClientResponseStatus : byte
    {
        Refused = 1,
        SendPacks = 2,
        HaveAllPacks = 3,
        Completed = 4
    }
}
