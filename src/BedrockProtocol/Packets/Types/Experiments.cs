using BedrockProtocol.Utils;
using System.Collections.Generic;

namespace BedrockProtocol.Packets.Types
{
    public class Experiments
    {
        public Dictionary<string, bool> ExperimentList { get; set; } = new();
        public bool HasPreviouslyUsedExperiments { get; set; }

        public void Encode(BinaryStream stream)
        {
            stream.WriteUnsignedInt((uint)ExperimentList.Count);

            foreach (var experiment in ExperimentList)
            {
                stream.WriteString(experiment.Key);
                stream.WriteBool(experiment.Value);
            }

            stream.WriteBool(HasPreviouslyUsedExperiments);
        }

        public void Decode(BinaryStream stream)
        {
            uint count = stream.ReadUnsignedInt();

            ExperimentList.Clear();

            for (int i = 0; i < count; i++)
            {
                string name = stream.ReadString();
                bool enabled = stream.ReadBool();

                ExperimentList[name] = enabled;
            }

            HasPreviouslyUsedExperiments = stream.ReadBool();
        }
    }
}