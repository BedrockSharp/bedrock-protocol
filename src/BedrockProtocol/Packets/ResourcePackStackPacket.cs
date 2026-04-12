using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ResourcePackStackPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePackStack;

        public bool MustAccept { get; set; }
        public string GameVersion { get; set; } = string.Empty;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(MustAccept);
            stream.WriteUnsignedVarInt(0); 
            stream.WriteUnsignedVarInt(0); 
            stream.WriteString(GameVersion);
            stream.WriteInt(0);
            stream.WriteBool(false);
        }

        public override void Decode(BinaryStream stream)
        {
            MustAccept = stream.ReadBool();
            stream.ReadUnsignedVarInt(); 
            stream.ReadUnsignedVarInt(); 
            GameVersion = stream.ReadString();
            stream.ReadInt();
            stream.ReadBool();
        }
    }
}
