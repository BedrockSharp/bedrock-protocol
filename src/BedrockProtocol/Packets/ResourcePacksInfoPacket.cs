using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class ResourcePacksInfoPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ResourcePacksInfo;

        public bool MustAccept { get; set; }
        public bool HasScripts { get; set; }
        public bool ForceServerPacks { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteBool(MustAccept);
            stream.WriteBool(HasScripts);
            stream.WriteBool(ForceServerPacks);
            stream.WriteShort(0); 
            stream.WriteShort(0); 
        }

        public override void Decode(BinaryStream stream)
        {
            MustAccept = stream.ReadBool();
            HasScripts = stream.ReadBool();
            ForceServerPacks = stream.ReadBool();
            stream.ReadShort(); 
            stream.ReadShort(); 
        }
    }
}
