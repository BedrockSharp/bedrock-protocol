namespace BedrockProtocol.Packets.Types
{
    public struct BlockPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public BlockPosition(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarInt(X);
            stream.WriteVarInt(Y);
            stream.WriteVarInt(Z);
        }

        public static BlockPosition Decode(BinaryStream stream)
        {
            return new BlockPosition
            {
                X = stream.ReadVarInt(),
                Y = stream.ReadVarInt(),
                Z = stream.ReadVarInt()
            };
        }
    }
}
