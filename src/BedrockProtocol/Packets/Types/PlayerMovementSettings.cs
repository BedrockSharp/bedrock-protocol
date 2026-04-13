using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    public class PlayerMovementSettings
    {
        public int RewindHistorySize { get; set; }
        public bool ServerAuthoritativeBlockBreaking { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(RewindHistorySize);
            stream.WriteBool(ServerAuthoritativeBlockBreaking);
        }

        public void Decode(BinaryStream stream)
        {
            RewindHistorySize = stream.ReadVarInt();
            ServerAuthoritativeBlockBreaking = stream.ReadBool();
        }
    }
}