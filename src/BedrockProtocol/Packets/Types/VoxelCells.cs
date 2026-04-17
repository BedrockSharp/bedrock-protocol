using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class VoxelCells
    {
        public byte XSize { get; set; }
        public byte YSize { get; set; }
        public byte ZSize { get; set; }
        public List<byte> Storage { get; set; } = new();

        public VoxelCells() { }

        public VoxelCells(byte xSize, byte ySize, byte zSize, List<byte> storage)
        {
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;
            Storage = storage;
        }
    }
}
