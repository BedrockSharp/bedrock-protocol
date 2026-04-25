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
        public List<string> Parameters { get; set; } = new();

        public string Xuid { get; set; } = string.Empty;
        public string PlatformChatId { get; set; } = string.Empty;

        public string FilteredMessage { get; set; } = string.Empty;

        public override void Decode(BinaryStream stream)
        {
            NeedsTranslation = stream.ReadBool();
            TextMode mode = (TextMode)stream.ReadByte();
            Type = (TextType)stream.ReadByte();

            switch (mode)
            {
                case TextMode.MessageOnly:
                    Message = stream.ReadString();
                    break;

                case TextMode.AuthorAndMessage:
                    SourceName = stream.ReadString();
                    Message = stream.ReadString();
                    break;

                case TextMode.MessageAndParams:
                    Message = stream.ReadString();
                    uint count = stream.ReadUnsignedVarInt();

                    Parameters = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        Parameters.Add(stream.ReadString());
                    }
                    break;

                default:
                    throw new System.Exception($"Unknown TextPacket mode: {mode}");
            }

            Xuid = stream.ReadString();
            PlatformChatId = stream.ReadString();

            FilteredMessage = stream.ReadString();
        }

        public override void Encode(BinaryStream stream)
        {
            bool needsTranslation = NeedsTranslation || Type == TextType.Translation;
            stream.WriteBool(needsTranslation);

            switch (Type)
            {
                case TextType.Raw:
                case TextType.Tip:
                case TextType.System:
                case TextType.ObjectMessage:
                case TextType.ObjectWhisperMessage:
                    stream.WriteByte(0);
                    stream.WriteByte((byte)Type);
                    stream.WriteString(string.IsNullOrEmpty(Message) ? " " : Message);
                    break;

                case TextType.Chat:
                case TextType.Whisper:
                case TextType.Announcement:
                    stream.WriteByte(1);
                    stream.WriteByte((byte)Type);
                    stream.WriteString(SourceName);
                    stream.WriteString(string.IsNullOrEmpty(Message) ? " " : Message);
                    break;

                case TextType.Translation:
                case TextType.Popup:
                case TextType.JukeboxPopup:
                    stream.WriteByte(2);
                    stream.WriteByte((byte)Type);
                    stream.WriteString(string.IsNullOrEmpty(Message) ? " " : Message);

                    stream.WriteUnsignedVarInt((uint)Parameters.Count);
                    foreach (var param in Parameters)
                    {
                        stream.WriteString(param);
                    }
                    break;

                default:
                    throw new System.Exception($"Unknown TextPacket type: {Type}");
            }

            stream.WriteString(Xuid);
            stream.WriteString(PlatformChatId);

            stream.WriteString(FilteredMessage);
        }
    }

    public enum TextMode : byte
    {
        MessageOnly,
        AuthorAndMessage,
        MessageAndParams,
        Translation,
        Popup,
        JukeboxPopup
    }
}