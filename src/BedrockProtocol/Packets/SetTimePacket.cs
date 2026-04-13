using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class SetTimePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.SetTime;

        public int Time { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(Time);
        }

        public override void Decode(BinaryStream stream)
        {
            Time = stream.ReadVarInt();
        }
    }
}
