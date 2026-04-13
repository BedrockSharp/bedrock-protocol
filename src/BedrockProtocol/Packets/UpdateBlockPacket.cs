using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class UpdateBlockPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UpdateBlock;

        public int X { get; set; }
        public uint Y { get; set; }
        public int Z { get; set; }
        public uint RuntimeId { get; set; }
        public uint Flags { get; set; }
        public uint Layer { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(X);
            stream.WriteUnsignedVarInt(Y);
            stream.WriteVarInt(Z);
            stream.WriteUnsignedVarInt(RuntimeId);
            stream.WriteUnsignedVarInt(Flags);
            stream.WriteUnsignedVarInt(Layer);
        }

        public override void Decode(BinaryStream stream)
        {
            X = stream.ReadVarInt();
            Y = stream.ReadUnsignedVarInt();
            Z = stream.ReadVarInt();
            RuntimeId = stream.ReadUnsignedVarInt();
            Flags = stream.ReadUnsignedVarInt();
            Layer = stream.ReadUnsignedVarInt();
        }
    }
}
