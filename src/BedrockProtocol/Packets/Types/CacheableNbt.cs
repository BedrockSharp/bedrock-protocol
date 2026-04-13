namespace BedrockProtocol.Types
{
    public class CacheableNbt
    {
        private byte[] encoded;

        public CacheableNbt(byte[] encoded)
        {
            this.encoded = encoded;
        }

        public byte[] GetEncodedNbt()
        {
            return encoded;
        }
    }
}