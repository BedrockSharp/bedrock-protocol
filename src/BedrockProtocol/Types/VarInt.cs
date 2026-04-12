using System.IO;

namespace BedrockProtocol.Types
{
    public static class VarInt
    {
        public static uint ReadUnsigned(BinaryReader reader)
        {
            uint value = 0;
            int shift = 0;
            byte b;
            do
            {
                b = reader.ReadByte();
                value |= (uint)(b & 0x7F) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);
            return value;
        }

        public static int Read(BinaryReader reader)
        {
            uint raw = ReadUnsigned(reader);
            int temp = (int)((raw << 31) >> 31);
            return (int)(raw >> 1) ^ temp;
        }

        public static void WriteUnsigned(BinaryWriter writer, uint value)
        {
            do
            {
                byte b = (byte)(value & 0x7F);
                value >>= 7;
                if (value != 0)
                {
                    b |= 0x80;
                }
                writer.Write(b);
            } while (value != 0);
        }

        public static void Write(BinaryWriter writer, int value)
        {
            uint z = (uint)((value << 1) ^ (value >> 31));
            WriteUnsigned(writer, z);
        }
    }
}
