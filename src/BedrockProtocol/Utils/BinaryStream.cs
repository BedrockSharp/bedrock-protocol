using BedrockProtocol.Types;
using BedrockProtocol.Packets.Types;
using Nbt;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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

        public ushort ReadUnsignedShort() => _reader.ReadUInt16();
        public void WriteUnsignedShort(ushort value) => _writer.Write(value);

        public int ReadInt() => _reader.ReadInt32();
        public void WriteInt(int value) => _writer.Write(value);

        public uint ReadUnsignedInt() => _reader.ReadUInt32();
        public void WriteUnsignedInt(uint value) => _writer.Write(value);

        public int ReadBigEndianInt()
        {
            byte[] bytes = _reader.ReadBytes(4);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public void WriteBigEndianInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            _writer.Write(bytes);
        }

        public long ReadLong() => _reader.ReadInt64();
        public void WriteLong(long value) => _writer.Write(value);

        public ulong ReadUnsignedLong() => _reader.ReadUInt64();
        public void WriteUnsignedLong(ulong value) => _writer.Write(value);

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

        public void WriteOptional<T>(T? value, Action<BinaryStream, T> writer) where T : struct {
            WriteBool(value.HasValue);
            if(value.HasValue) {
                writer(this, value.Value);
            }
        }

        public void WriteOptional<T>(T value, Action<BinaryStream, T> writer) where T : class {
            WriteBool(value != null);
            if(value != null) {
                writer(this, value);
            }
        }
        
        public T? ReadOptional<T>(Func<BinaryStream, T> reader) where T : struct {
            bool exists = ReadBool();
            if(!exists) return null;
            return reader(this);
        }

        public T? ReadOptionalClass<T>(Func<BinaryStream, T> reader) where T : class {
            bool exists = ReadBool();
            if(!exists) return null;
            return reader(this);
        }

        public T? ReadOptional<T>() where T : class, new() {
            bool exists = ReadBool();
            if(!exists) return null;
            T obj = new();
            ((dynamic) obj).Decode(this);
            return obj;
        }

        public long ReadActorUniqueId() => ReadVarLong();
        public void WriteActorUniqueId(long value) => WriteVarLong(value);

        public ulong ReadActorRuntimeId() => ReadUnsignedVarLong();
        public void WriteActorRuntimeId(ulong value) => WriteUnsignedVarLong(value);

        public byte[] ReadByteArray()
        {
            uint length = ReadUnsignedVarInt();
            return ReadBytes((int)length);
        }

        public void WriteByteArray(byte[] data)
        {
            WriteUnsignedVarInt((uint)data.Length);
            WriteBytes(data);
        }

        public void WriteVector3(float x, float y, float z)
        {
            WriteFloat(x);
            WriteFloat(y);
            WriteFloat(z);
        }

        public void WriteBlockVector3(int x, int y, int z)
        {
            WriteVarInt(x);
            WriteVarInt(y);
            WriteVarInt(z);
        }

        public void WriteNbt(CompoundTag tag)
        {
            var nbtWriter = new NbtBinaryWriter(_memoryStream, false); 
            tag.WriteTag(nbtWriter);
        }

        public void WriteNetworkNbt(CompoundTag tag)
        {
            var nbtWriter = new NbtBinaryWriter(_memoryStream, false);
            tag.WriteNetwork(nbtWriter);
        }

        public void WriteExperiments(List<ExperimentEntry> experiments)
        {
            WriteIntLE(experiments.Count);
            foreach (var experiment in experiments)
            {
                WriteString(experiment.Name);
                WriteBool(experiment.Enabled);
            }
        }

        public void WriteArray<T>(ICollection<T> collection, Action<T> elementWriter)
        {
            WriteUnsignedVarInt((uint)collection.Count);
            foreach (var item in collection)
            {
                elementWriter(item);
            }
        }

        public List<T> ReadArray<T>(Func<T> elementReader)
        {
            uint length = ReadUnsignedVarInt();
            var list = new List<T>((int)length);
            for (int i = 0; i < length; i++)
            {
                list.Add(elementReader());
            }
            return list;
        }

        public void WriteIntLE(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            _writer.Write(bytes);
        }

        public int ReadIntLE()
        {
            byte[] bytes = _reader.ReadBytes(4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public void WriteLongLE(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            _writer.Write(bytes);
        }

        public long ReadLongLE()
        {
            byte[] bytes = _reader.ReadBytes(8);
            return BitConverter.ToInt64(bytes, 0);
        }

        public void WriteShortLE(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            _writer.Write(bytes);
        }

        public short ReadShortLE()
        {
            byte[] bytes = _reader.ReadBytes(2);
            return BitConverter.ToInt16(bytes, 0);
        }
    }
}
