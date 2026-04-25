using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class AnimatePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Animate;

        public ulong RuntimeEntityId { get; set; }
        public AnimationAction Action { get; set; }
        public float Data { get; set; }
        public SwingSource SwingSource { get; set; } = SwingSource.None;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)Action);
            stream.WriteUnsignedVarLong(RuntimeEntityId);
            stream.WriteFloat(Data);
            
            stream.WriteBool(SwingSource != SwingSource.None);
            if (SwingSource != SwingSource.None)
            {
                stream.WriteString(SwingSource.GetName());
            }
        }

        public override void Decode(BinaryStream stream)
        {
            Action = (AnimationAction)stream.ReadByte();
            RuntimeEntityId = stream.ReadUnsignedVarLong();
            Data = stream.ReadFloat();
            
            if (stream.ReadBool())
            {
                SwingSource = SwingSourceExtensions.FromName(stream.ReadString());
            }
            else
            {
                SwingSource = SwingSource.None;
            }
        }
    }
}
