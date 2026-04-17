using BedrockProtocol.Utils;
using BedrockProtocol.Packets.Types;
using System.Collections.Generic;

namespace BedrockProtocol.Packets
{
    public class VoxelShapesPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.VoxelShapes;

        public List<VoxelShape> Shapes { get; set; } = new();
        public Dictionary<string, ushort> NameMap { get; set; } = new();
        public ushort CustomShapeCount { get; set; }

        public override void Decode(BinaryStream stream)
        {
            Shapes = stream.ReadArray(() =>
            {
                byte xSize = stream.ReadByte();
                byte ySize = stream.ReadByte();
                byte zSize = stream.ReadByte();

                List<byte> storage = stream.ReadArray(stream.ReadByte);

                VoxelCells cells = new VoxelCells(xSize, ySize, zSize, storage);

                List<float> xCoordinates = stream.ReadArray(stream.ReadFloat);
                List<float> yCoordinates = stream.ReadArray(stream.ReadFloat);
                List<float> zCoordinates = stream.ReadArray(stream.ReadFloat);

                return new VoxelShape(cells, xCoordinates, yCoordinates, zCoordinates);
            });

            uint nameMapSize = stream.ReadUnsignedVarInt();
            NameMap = new Dictionary<string, ushort>();
            for (int i = 0; i < nameMapSize; i++)
            {
                string name = stream.ReadString();
                ushort value = stream.ReadUnsignedShort();
                NameMap[name] = value;
            }

            CustomShapeCount = stream.ReadUnsignedShort();
        }

        public override void Encode(BinaryStream stream)
        {
            stream.WriteArray(Shapes, (shape) =>
            {
                stream.WriteByte(shape.Cells.XSize);
                stream.WriteByte(shape.Cells.YSize);
                stream.WriteByte(shape.Cells.ZSize);

                stream.WriteArray(shape.Cells.Storage, stream.WriteByte);

                stream.WriteArray(shape.XCoordinates, stream.WriteFloat);
                stream.WriteArray(shape.YCoordinates, stream.WriteFloat);
                stream.WriteArray(shape.ZCoordinates, stream.WriteFloat);
            });

            stream.WriteUnsignedVarInt((uint)NameMap.Count);
            foreach (var kvP in NameMap)
            {
                stream.WriteString(kvP.Key);
                stream.WriteUnsignedShort(kvP.Value);
            }

            stream.WriteUnsignedShort(CustomShapeCount);
        }
    }
}
