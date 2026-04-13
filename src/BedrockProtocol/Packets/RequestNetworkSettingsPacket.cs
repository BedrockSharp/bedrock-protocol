using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class RequestNetworkSettingsPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.RequestNetworkSettings;

        public int ProtocolVersion { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteInt(ProtocolVersion);
        }

        public override void Decode(BinaryStream stream)
        {
            ProtocolVersion = stream.ReadInt();
        }
    }
}
