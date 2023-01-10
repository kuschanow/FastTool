namespace FastTool.WPF
{
    public enum Error
    {
        None,
        IdentityName,
        ReservedName,
        HasPrefix
    }

    public enum TimerAction
    {
        None = 1,
        MessageWithName = 2,
        AnotherMessage = 4,
        TurnOff = 8,
        RestartTimer = 16,
    }
}
