using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class VoxelShape
    {
        public VoxelCells Cells { get; set; } = new();
        public List<float> XCoordinates { get; set; } = new();
        public List<float> YCoordinates { get; set; } = new();
        public List<float> ZCoordinates { get; set; } = new();

        public VoxelShape() { }

        public VoxelShape(VoxelCells cells, List<float> xCoords, List<float> yCoords, List<float> zCoords)
        {
            Cells = cells;
            XCoordinates = xCoords;
            YCoordinates = yCoords;
            ZCoordinates = zCoords;
        }
    }
}
