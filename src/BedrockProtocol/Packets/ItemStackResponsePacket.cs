using BedrockProtocol.Packets.Types;
using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class ItemStackResponsePacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.ItemStackResponse;

        public List<ItemStackResponse> Responses { get; set; } = new List<ItemStackResponse>();

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt((uint)Responses.Count);
            foreach (var response in Responses)
            {
                response.Encode(stream);
            }
        }

        public override void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedVarInt();
            Responses = new List<ItemStackResponse>((int)count);
            for (int i = 0; i < count; i++)
            {
                var response = new ItemStackResponse();
                response.Decode(stream);
                Responses.Add(response);
            }
        }
    }
}
