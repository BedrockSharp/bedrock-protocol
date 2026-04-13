using System;
using System.IO;
using System.Text;
using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class LoginPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Login;

        public int ProtocolVersion { get; set; }
        public string ChainDataJwt { get; set; } = string.Empty;
        public string ClientDataJwt { get; set; } = string.Empty;

        public override void Encode(BinaryStream stream)
        {
            byte[] pv = BitConverter.GetBytes(ProtocolVersion);
            if (BitConverter.IsLittleEndian) Array.Reverse(pv);
            stream.WriteBytes(pv);

            using var payloadStream = new MemoryStream();
            using var payloadWriter = new BinaryWriter(payloadStream, Encoding.UTF8);

            WriteBedrockString(payloadWriter, ChainDataJwt);
            WriteBedrockString(payloadWriter, ClientDataJwt);

            var payloadBytes = payloadStream.ToArray();
            stream.WriteUnsignedVarInt((uint)payloadBytes.Length);
            stream.WriteBytes(payloadBytes);
        }

        public override void Decode(BinaryStream stream)
        {
            byte[] pv = stream.ReadBytes(4);
            if (BitConverter.IsLittleEndian) Array.Reverse(pv);
            ProtocolVersion = BitConverter.ToInt32(pv, 0);

            uint len = stream.ReadUnsignedVarInt();
            var payloadBytes = stream.ReadBytes((int)len);
            
            using var payloadStream = new MemoryStream(payloadBytes);
            using var payloadReader = new BinaryReader(payloadStream, Encoding.UTF8);

            ChainDataJwt = ReadBedrockString(payloadReader);
            ClientDataJwt = ReadBedrockString(payloadReader);
        }

        private void WriteBedrockString(BinaryWriter writer, string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            BedrockProtocol.Types.VarInt.WriteUnsigned(writer, (uint)bytes.Length);
            writer.Write(bytes);
        }

        private string ReadBedrockString(BinaryReader reader)
        {
            uint length = BedrockProtocol.Types.VarInt.ReadUnsigned(reader);
            byte[] bytes = reader.ReadBytes((int)length);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
