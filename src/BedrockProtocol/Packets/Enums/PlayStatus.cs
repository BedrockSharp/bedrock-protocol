namespace BedrockProtocol.Packets.Enums
{
    public enum PlayStatus : int
    {
        LoginSuccess = 0,
        LoginFailedClient = 1,
        LoginFailedServer = 2,
        PlayerSpawn = 3,
        LoginFailedInvalidTenant = 4,
        LoginFailedVanillaEdu = 5,
        LoginFailedEduVanilla = 6,
        LoginFailedServerFull = 7
    }
}
