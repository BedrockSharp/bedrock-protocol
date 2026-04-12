using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets.Types
{
    public enum EntityDataType
    {
        Byte = 0,
        Short = 1,
        Int = 2,
        Float = 3,
        String = 4,
        Compound = 5,
        Pos = 6,
        Long = 7,
        Vec3 = 8
    }

    public abstract class MetadataEntry
    {
        public uint Id { get; set; }
        public abstract EntityDataType Type { get; }
        public abstract void Encode(BinaryStream stream);
        public abstract void Decode(BinaryStream stream);
    }

    public class ByteMetadataEntry : MetadataEntry
    {
        public override EntityDataType Type => EntityDataType.Byte;
        public byte Value { get; set; }
        public override void Encode(BinaryStream stream) => stream.WriteByte(Value);
        public override void Decode(BinaryStream stream) => Value = stream.ReadByte();
    }

    public class IntMetadataEntry : MetadataEntry
    {
        public override EntityDataType Type => EntityDataType.Int;
        public int Value { get; set; }
        public override void Encode(BinaryStream stream) => stream.WriteVarInt(Value);
        public override void Decode(BinaryStream stream) => Value = stream.ReadVarInt();
    }

    public class FloatMetadataEntry : MetadataEntry
    {
        public override EntityDataType Type => EntityDataType.Float;
        public float Value { get; set; }
        public override void Encode(BinaryStream stream) => stream.WriteFloat(Value);
        public override void Decode(BinaryStream stream) => Value = stream.ReadFloat();
    }

    public class StringMetadataEntry : MetadataEntry
    {
        public override EntityDataType Type => EntityDataType.String;
        public string Value { get; set; } = string.Empty;
        public override void Encode(BinaryStream stream) => stream.WriteString(Value);
        public override void Decode(BinaryStream stream) => Value = stream.ReadString();
    }

    public class LongMetadataEntry : MetadataEntry
    {
        public override EntityDataType Type => EntityDataType.Long;
        public long Value { get; set; }
        public override void Encode(BinaryStream stream) => stream.WriteVarLong(Value);
        public override void Decode(BinaryStream stream) => Value = stream.ReadVarLong();
    }
}
