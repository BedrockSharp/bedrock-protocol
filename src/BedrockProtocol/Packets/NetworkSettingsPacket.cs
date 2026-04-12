using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class NetworkSettingsPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.NetworkSettings;

        public ushort CompressionThreshold { get; set; }
        public CompressionAlgorithm CompressionAlgorithm { get; set; }
        public bool ClientThrottling { get; set; }
        public byte ThrottlingThreshold { get; set; }
        public float ThrottlingScalar { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteShort((short)CompressionThreshold);
            stream.WriteShort((short)CompressionAlgorithm);
            stream.WriteBool(ClientThrottling);
            stream.WriteByte(ThrottlingThreshold);
            stream.WriteFloat(ThrottlingScalar);
        }

        public override void Decode(BinaryStream stream)
        {
            CompressionThreshold = (ushort)stream.ReadShort();
            CompressionAlgorithm = (CompressionAlgorithm)stream.ReadShort();
            ClientThrottling = stream.ReadBool();
            ThrottlingThreshold = stream.ReadByte();
            ThrottlingScalar = stream.ReadFloat();
        }
    }
}
