using System.Collections.Generic;
using BedrockProtocol.Packets.Enums;
using BedrockProtocol.Utils;

namespace BedrockProtocol.Packets
{
    public class TextPacket : Packet
    {
        public override uint PacketId => (uint)PacketIds.Text;

        public TextType Type { get; set; }
        public bool NeedsTranslation { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> Parameters { get; set; } = new List<string>();
        public string Xuid { get; set; } = string.Empty;
        public string PlatformChatId { get; set; } = string.Empty;

        public override void Encode(BinaryStream stream)
        {
            stream.WriteByte((byte)Type);
            stream.WriteBool(NeedsTranslation);

            switch (Type)
            {
                case TextType.Chat:
                case TextType.Whisper:
                    stream.WriteString(SourceName);
                    stream.WriteString(Message);
                    break;
                case TextType.Translation:
                case TextType.Popup:
                    stream.WriteString(Message);
                    stream.WriteUnsignedVarInt((uint)Parameters.Count);
                    foreach (var param in Parameters)
                    {
                        stream.WriteString(param);
                    }
                    if (Type == TextType.Translation)
                    {
                        stream.WriteString(SourceName);
                        stream.WriteString(Xuid);
                    }
                    break;
                case TextType.JukeboxPopup:
                case TextType.Tip:
                case TextType.System:
                case TextType.Announcement:
                    stream.WriteString(Message);
                    break;
            }

            stream.WriteString(Xuid);
            stream.WriteString(PlatformChatId);
        }

        public override void Decode(BinaryStream stream)
        {
            Type = (TextType)stream.ReadByte();
            NeedsTranslation = stream.ReadBool();

            switch (Type)
            {
                case TextType.Chat:
                case TextType.Whisper:
                    SourceName = stream.ReadString();
                    Message = stream.ReadString();
                    break;
                case TextType.Translation:
                case TextType.Popup:
                    Message = stream.ReadString();
                    uint count = stream.ReadUnsignedVarInt();
                    Parameters = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        Parameters.Add(stream.ReadString());
                    }
                    if (Type == TextType.Translation)
                    {
                        SourceName = stream.ReadString();
                        Xuid = stream.ReadString();
                    }
                    break;
                case TextType.JukeboxPopup:
                case TextType.Tip:
                case TextType.System:
                case TextType.Announcement:
                    Message = stream.ReadString();
                    break;
            }

            Xuid = stream.ReadString();
            PlatformChatId = stream.ReadString();
        }
    }
}
