using System.Security.Cryptography;

namespace BedrockProtocol.Utils
{
    public static class EncryptionUtils
    {
        public static ECDiffieHellman GenerateKeyPair()
        {
            var ecdh = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP384);
            return ecdh;
        }

        public static byte[] DeriveSharedSecret(ECDiffieHellman serverKey, byte[] clientPublicKeyBytes)
        {
            using var clientEcdh = ECDiffieHellman.Create();
            clientEcdh.ImportSubjectPublicKeyInfo(clientPublicKeyBytes, out _);
            return serverKey.DeriveKeyFromHash(clientEcdh.PublicKey, HashAlgorithmName.SHA256);
        }
    }
}
