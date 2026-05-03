using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Enums;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class AbilityData
    {
        public long EntityUniqueId { get; set; }
        public byte PlayerPermissions { get; set; }
        public byte CommandPermissions { get; set; }
        public List<AbilityLayer> Layers { get; set; } = new List<AbilityLayer>();

        public void Encode(BinaryStream stream)
        {
            stream.WriteLong(EntityUniqueId);
            stream.WriteByte(PlayerPermissions);
            stream.WriteByte(CommandPermissions);
            stream.WriteByte((byte)Layers.Count);
            foreach (var layer in Layers)
            {
                layer.Encode(stream);
            }
        }

        public void Decode(BinaryStream stream)
        {
            EntityUniqueId = stream.ReadLong();
            PlayerPermissions = stream.ReadByte();
            CommandPermissions = stream.ReadByte();
            int count = stream.ReadByte();
            Layers.Clear();
            for (int i = 0; i < count; i++)
            {
                var layer = new AbilityLayer();
                layer.Decode(stream);
                Layers.Add(layer);
            }
        }
    }

    public class AbilityLayer
    {
        public ushort Type { get; set; }
        public uint Abilities { get; set; }
        public uint Values { get; set; }
        public float FlySpeed { get; set; }
        public float VerticalFlySpeed { get; set; }
        public float WalkSpeed { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedShort(Type);
            stream.WriteUnsignedInt(Abilities);
            stream.WriteUnsignedInt(Values);
            stream.WriteFloat(FlySpeed);
            stream.WriteFloat(VerticalFlySpeed);
            stream.WriteFloat(WalkSpeed);
        }

        public void Decode(BinaryStream stream)
        {
            Type = stream.ReadUnsignedShort();
            Abilities = stream.ReadUnsignedInt();
            Values = stream.ReadUnsignedInt();
            FlySpeed = stream.ReadFloat();
            VerticalFlySpeed = stream.ReadFloat();
            WalkSpeed = stream.ReadFloat();
        }
    }
}
