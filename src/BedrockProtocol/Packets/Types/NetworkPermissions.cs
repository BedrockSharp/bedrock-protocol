using BedrockProtocol.Utils;

namespace BedrockProtocol.Types
{
    public class NetworkPermissions
    {
        public bool DisableClientSounds { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteBool(DisableClientSounds);
        }

        public void Decode(BinaryStream stream)
        {
            DisableClientSounds = stream.ReadBool();
        }
    }
}