namespace Game.Utilities
{
    public static class RewardFormatter
    {
        public static string FormatAmount(int amount)
        {
            return $"x{amount}";
        }

        public static string FormatReward(int amount, string rewardName)
        {
            return $"{amount}x {rewardName}";
        }
    }
}
