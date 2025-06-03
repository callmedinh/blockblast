using System;

namespace Events
{
    public class GameplayEvent
    {
        public static Action OnSettingButtonClicked;
        public static Action<int> OnScoreChanged;
    }
}