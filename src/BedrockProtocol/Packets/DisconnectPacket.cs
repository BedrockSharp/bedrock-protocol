using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class DisconnectPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Disconnect;

        public int Reason { get; set; }
        public bool HideDisconnectReason { get; set; }
        public string Message { get; set; } = string.Empty;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteInt(Reason);
            stream.WriteBool(HideDisconnectReason);
            stream.WriteString(Message);
        }

        public override void Decode(BinaryStream stream)
        {
            Reason = stream.ReadInt();
            HideDisconnectReason = stream.ReadBool();
            Message = stream.ReadString();
        }
    }
}
