namespace BedrockProtocol.Packets.Enums
{
    public enum SwingSource
    {
        None,
        Build,
        Mine,
        Interact,
        Attack,
        UseItem,
        ThrowItem,
        DropItem,
        Event
    }

    public static class SwingSourceExtensions
    {
        public static string GetName(this SwingSource source)
        {
            return source switch
            {
                SwingSource.None => "none",
                SwingSource.Build => "build",
                SwingSource.Mine => "mine",
                SwingSource.Interact => "interact",
                SwingSource.Attack => "attack",
                SwingSource.UseItem => "useitem",
                SwingSource.ThrowItem => "throwitem",
                SwingSource.DropItem => "dropitem",
                SwingSource.Event => "event",
                _ => "none"
            };
        }

        public static SwingSource FromName(string name)
        {
            return name switch
            {
                "none" => SwingSource.None,
                "build" => SwingSource.Build,
                "mine" => SwingSource.Mine,
                "interact" => SwingSource.Interact,
                "attack" => SwingSource.Attack,
                "useitem" => SwingSource.UseItem,
                "throwitem" => SwingSource.ThrowItem,
                "dropitem" => SwingSource.DropItem,
                "event" => SwingSource.Event,
                _ => SwingSource.None
            };
        }
    }
}
