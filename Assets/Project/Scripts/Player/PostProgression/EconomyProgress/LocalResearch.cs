[System.Serializable]
public class LocalResearch : Progress
{
        public LocalResearch(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.AddTwists += 1;
        }
}