using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class NetworkAttributeValue
    {
        public string Name { get; set; } = string.Empty;
        public float Min { get; set; }
        public float Value { get; set; }
        public float Max { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Name);
            stream.WriteFloat(Min);
            stream.WriteFloat(Value);
            stream.WriteFloat(Max);
        }

        public void Decode(BinaryStream stream)
        {
            Name = stream.ReadString();
            Min = stream.ReadFloat();
            Value = stream.ReadFloat();
            Max = stream.ReadFloat();
        }
    }

    public class EntityLink
    {
        public long RiddenEntityUniqueId { get; set; }
        public long RiderEntityUniqueId { get; set; }
        public byte Type { get; set; }
        public bool Immediate { get; set; }
        public bool RiderInitiated { get; set; }
        public float VehicleAngularVelocity { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteVarLong(RiddenEntityUniqueId);
            stream.WriteVarLong(RiderEntityUniqueId);
            stream.WriteByte(Type);
            stream.WriteBool(Immediate);
            stream.WriteBool(RiderInitiated);
            stream.WriteFloat(VehicleAngularVelocity);
        }

        public void Decode(BinaryStream stream)
        {
            RiddenEntityUniqueId = stream.ReadVarLong();
            RiderEntityUniqueId = stream.ReadVarLong();
            Type = stream.ReadByte();
            Immediate = stream.ReadBool();
            RiderInitiated = stream.ReadBool();
            VehicleAngularVelocity = stream.ReadFloat();
        }
    }

    public class AttributeModifier
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
        public int Operation { get; set; }
        public int Operand { get; set; }
        public bool Serializable { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteString(Id);
            stream.WriteString(Name);
            stream.WriteFloat(Amount);
            stream.WriteIntLE(Operation);
            stream.WriteIntLE(Operand);
            stream.WriteBool(Serializable);
        }

        public void Decode(BinaryStream stream)
        {
            Id = stream.ReadString();
            Name = stream.ReadString();
            Amount = stream.ReadFloat();
            Operation = stream.ReadIntLE();
            Operand = stream.ReadIntLE();
            Serializable = stream.ReadBool();
        }
    }

    public class NetworkAttribute
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public float Value { get; set; }
        public float DefaultMin { get; set; }
        public float DefaultMax { get; set; }
        public float DefaultValue { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<AttributeModifier> Modifiers { get; set; } = new List<AttributeModifier>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteFloat(Min);
            stream.WriteFloat(Max);
            stream.WriteFloat(Value);
            stream.WriteFloat(DefaultMin);
            stream.WriteFloat(DefaultMax);
            stream.WriteFloat(DefaultValue);
            stream.WriteString(Name);
            
            stream.WriteUnsignedVarInt((uint)Modifiers.Count);
            foreach (var modifier in Modifiers)
            {
                modifier.Encode(stream);
            }
        }

        public void Decode(BinaryStream stream)
        {
            Min = stream.ReadFloat();
            Max = stream.ReadFloat();
            Value = stream.ReadFloat();
            DefaultMin = stream.ReadFloat();
            DefaultMax = stream.ReadFloat();
            DefaultValue = stream.ReadFloat();
            Name = stream.ReadString();

            uint modifierCount = stream.ReadUnsignedVarInt();
            Modifiers = new List<AttributeModifier>((int)modifierCount);
            for (int i = 0; i < modifierCount; i++)
            {
                var mod = new AttributeModifier();
                mod.Decode(stream);
                Modifiers.Add(mod);
            }
        }
    }
}
