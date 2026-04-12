using System.IO;

namespace BedrockProtocol.Types
{
    public static class VarLong
    {
        public static ulong ReadUnsigned(BinaryReader reader)
        {
            ulong value = 0;
            int shift = 0;
            byte b;
            do
            {
                b = reader.ReadByte();
                value |= (ulong)(b & 0x7F) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);
            return value;
        }

        public static long Read(BinaryReader reader)
        {
            ulong raw = ReadUnsigned(reader);
            long temp = (long)((raw << 63) >> 63);
            return (long)(raw >> 1) ^ temp;
        }

        public static void WriteUnsigned(BinaryWriter writer, ulong value)
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

        public static void Write(BinaryWriter writer, long value)
        {
            ulong z = (ulong)((value << 1) ^ (value >> 63));
            WriteUnsigned(writer, z);
        }
    }
}
