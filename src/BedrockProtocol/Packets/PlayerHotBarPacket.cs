using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class PlayerHotBarPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.PlayerHotBar;

        public uint SelectedHotBarSlot { get; set; }
        public byte WindowId { get; set; }
        public bool SelectHotBarSlot { get; set; }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedVarInt(SelectedHotBarSlot);
            stream.WriteByte(WindowId);
            stream.WriteBool(SelectHotBarSlot);
        }

        public override void Decode(BinaryStream stream)
        {
            SelectedHotBarSlot = stream.ReadUnsignedVarInt();
            WindowId = stream.ReadByte();
            SelectHotBarSlot = stream.ReadBool();
        }
    }
}
