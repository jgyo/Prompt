namespace Prompt.ViewModel
{
    using System;

    [Flags]
    public enum EyelinePosition
    {
        None = 0,
        Left = 1,
        Right = 2,
        Both = Left | Right
    }
}