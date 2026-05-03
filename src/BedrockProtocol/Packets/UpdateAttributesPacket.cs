using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class UpdateAttributesPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.UpdateAttributes;

        public ulong EntityRuntimeId { get; set; }
        public List<NetworkAttribute> Attributes { get; set; } = new List<NetworkAttribute>();
        public ulong Tick { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarLong(EntityRuntimeId);
            stream.WriteUnsignedVarInt((uint)Attributes.Count);
            foreach (var attribute in Attributes)
            {
                attribute.Encode(stream);
            }
            stream.WriteUnsignedVarLong(Tick);
        }

        public override void Decode(BinaryStream stream)
        {
            EntityRuntimeId = stream.ReadUnsignedVarLong();
            uint count = stream.ReadUnsignedVarInt();
            Attributes = new List<NetworkAttribute>((int)count);
            for (int i = 0; i < count; i++)
            {
                var attr = new NetworkAttribute();
                attr.Decode(stream);
                Attributes.Add(attr);
            }
            Tick = stream.ReadUnsignedVarLong();
        }
    }
}
