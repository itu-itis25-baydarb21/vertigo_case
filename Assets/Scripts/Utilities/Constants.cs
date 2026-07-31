using UnityEngine;

namespace Game.Utilities
{
    public static class GameConstants
    {
        public const int SAFE_ZONE_INTERVAL = 5;
        public const int SUPER_ZONE_INTERVAL = 30;
    }

    public static class WheelConstants
    {
        public const float SPIN_DURATION = 3.5f;
        public const int SPIN_REVOLUTIONS = 5;
        public const float SLICE_RADIUS = 175f;
        public const int TOTAL_SLICES = 8;
        public const float WHEEL_STOP_OFFSET = -360f;
    }

    public static class UIConstants
    {
        public const string ZONE_PREFIX = "ZONE ";
        public const string BRONZE_SPIN_HEADER = "BRONZE SPIN";
        public const string SILVER_SPIN_HEADER = "SILVER SPIN";
        public const string GOLDEN_SPIN_HEADER = "GOLDEN SPIN";
        public const string INFO_SPIN_TO_WIN = "SPIN TO WIN";
        public const string INFO_UP_TO_2X = "UP TO 2X";
        public const string INFO_UP_TO_10X = "UP TO 10X";
        
        public static readonly Color32 BRONZE_COLOR = new Color32(205, 127, 50, 255);
        public static readonly Color32 SILVER_COLOR = new Color32(192, 192, 192, 255);
        public static readonly Color32 GOLDEN_COLOR = new Color32(255, 215, 0, 255);
    }

    public static class AnimationConstants
    {
        public const float POPUP_REVEAL_DURATION = 0.5f;
        public const float POPUP_HIDE_DURATION = 0.3f;
        public const float POPUP_DISPLAY_DELAY = 1.5f;
        public const float SHINE_ROTATION_DURATION = 4f;
    }

    public static class EconomyConstants
    {
        public const int DEFAULT_REVIVE_COST = 100;
        public const float SAFE_ZONE_MULTIPLIER = 2f;
        public const float SUPER_ZONE_MULTIPLIER = 10f;
    }
}
