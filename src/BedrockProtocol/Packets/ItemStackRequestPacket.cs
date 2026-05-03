using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class ItemStackRequestPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ItemStackRequest;

        public List<ItemStackRequest> Requests { get; set; } = new List<ItemStackRequest>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Requests.Count);
            foreach (var request in Requests)
            {
                request.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedVarInt();
            Requests = new List<ItemStackRequest>((int)count);
            for (int i = 0; i < count; i++)
            {
                var request = new ItemStackRequest();
                request.Decode(stream);
                Requests.Add(request);
            }
        }
    }
}
