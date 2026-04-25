namespace BedrockProtocol.Packets.Enums
{
    public enum TextType : byte
    {
        Raw = 0,
        Chat = 1,
        Translation = 2,
        Popup = 3,
        JukeboxPopup = 4,
        Tip = 5,
        System = 6,
        Whisper = 7,
        Announcement = 8,
        ObjectMessage = 9,
        ObjectWhisperMessage = 10
    }
}
