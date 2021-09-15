namespace DroneLibrary.Facades.SimpleData
{
    public readonly struct SimpleDroneResult
    {
        public bool FinishedCorrectly { get; }
        public SimpleDroneInfo EndInfo { get; }
        public bool WentBackToBase { get; }
        public SimpleDroneInfo CurrentInfo { get; }

        public SimpleDroneResult(bool finishedCorrectly, SimpleDroneInfo endInfo,
            bool wentBackToBase, SimpleDroneInfo currentInfo)
        {
            FinishedCorrectly = finishedCorrectly;
            EndInfo = endInfo;
            WentBackToBase = wentBackToBase;
            CurrentInfo = currentInfo;
        }
    }
}