using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class ResourcePackStackPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePackStack;

        public List<ResourcePackStackEntry> ResourcePackStack { get; set; } = new();
        public bool MustAccept { get; set; }
        public string BaseGameVersion { get; set; } = string.Empty;
        public Experiments Experiments { get; set; } = new();
        public bool UseVanillaEditorPacks { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(MustAccept);

            stream.WriteUnsignedVarInt((uint)ResourcePackStack.Count);
            foreach (var entry in ResourcePackStack)
            {
                entry.Encode(stream);
            }

            stream.WriteString(BaseGameVersion);
            Experiments.Encode(stream);
            stream.WriteBool(UseVanillaEditorPacks);
        }

        public override void Decode(BinaryStream stream)
        {
            MustAccept = stream.ReadBool();

            uint count = stream.ReadUnsignedVarInt();
            ResourcePackStack.Clear();

            for (int i = 0; i < count; i++)
            {
                var entry = new ResourcePackStackEntry();
                entry.Decode(stream);
                ResourcePackStack.Add(entry);
            }

            BaseGameVersion = stream.ReadString();

            Experiments = new Experiments();
            Experiments.Decode(stream);

            UseVanillaEditorPacks = stream.ReadBool();
        }
    }
}