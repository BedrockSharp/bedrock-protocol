using System.IO;
using System.Text;
using BedrockProtocol.Types;

namespace BedrockProtocol.Utils
{
    public class BinaryStream
    {
        private MemoryStream _memoryStream;
        private BinaryReader _reader;
        private BinaryWriter _writer;

        public BinaryStream()
        {
            _memoryStream = new MemoryStream();
            _reader = new BinaryReader(_memoryStream, Encoding.UTF8);
            _writer = new BinaryWriter(_memoryStream, Encoding.UTF8);
        }

        public BinaryStream(byte[] buffer)
        {
            _memoryStream = new MemoryStream(buffer);
            _reader = new BinaryReader(_memoryStream, Encoding.UTF8);
            _writer = new BinaryWriter(_memoryStream, Encoding.UTF8);
        }

        public byte[] GetBuffer()
        {
            return _memoryStream.ToArray();
        }
        
        public void SetBuffer(byte[] buffer)
        {
            _memoryStream = new MemoryStream(buffer);
            _reader = new BinaryReader(_memoryStream, Encoding.UTF8);
            _writer = new BinaryWriter(_memoryStream, Encoding.UTF8);
        }

        public bool Eof => _memoryStream.Position >= _memoryStream.Length;
        public long Position
        {
            get => _memoryStream.Position;
            set => _memoryStream.Position = value;
        }
        
        public byte ReadByte() => _reader.ReadByte();
        public void WriteByte(byte value) => _writer.Write(value);

        public short ReadShort() => _reader.ReadInt16();
        public void WriteShort(short value) => _writer.Write(value);

        public int ReadInt() => _reader.ReadInt32();
        public void WriteInt(int value) => _writer.Write(value);

        public long ReadLong() => _reader.ReadInt64();
        public void WriteLong(long value) => _writer.Write(value);

        public float ReadFloat() => _reader.ReadSingle();
        public void WriteFloat(float value) => _writer.Write(value);

        public bool ReadBool() => _reader.ReadBoolean();
        public void WriteBool(bool value) => _writer.Write(value);

        public int ReadVarInt() => VarInt.Read(_reader);
        public void WriteVarInt(int value) => VarInt.Write(_writer, value);

        public uint ReadUnsignedVarInt() => VarInt.ReadUnsigned(_reader);
        public void WriteUnsignedVarInt(uint value) => VarInt.WriteUnsigned(_writer, value);

        public long ReadVarLong() => VarLong.Read(_reader);
        public void WriteVarLong(long value) => VarLong.Write(_writer, value);

        public ulong ReadUnsignedVarLong() => VarLong.ReadUnsigned(_reader);
        public void WriteUnsignedVarLong(ulong value) => VarLong.WriteUnsigned(_writer, value);

        public string ReadString()
        {
            uint length = ReadUnsignedVarInt();
            byte[] bytes = _reader.ReadBytes((int)length);
            return Encoding.UTF8.GetString(bytes);
        }

        public void WriteString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            WriteUnsignedVarInt((uint)bytes.Length);
            _writer.Write(bytes);
        }
        
        public byte[] ReadBytes(int count) => _reader.ReadBytes(count);
        public void WriteBytes(byte[] value) => _writer.Write(value);

        public void WriteUuid(System.Guid uuid)
        {
            byte[] bytes = uuid.ToByteArray();
            _writer.Write(bytes);
        }

        public System.Guid ReadUuid()
        {
            byte[] bytes = _reader.ReadBytes(16);
            return new System.Guid(bytes);
        }
    }
}
