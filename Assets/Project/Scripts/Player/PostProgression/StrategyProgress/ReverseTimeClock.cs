[System.Serializable]
public class ReverseTimeClock : Progress
{
        public ReverseTimeClock(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.IsRerollDef = 1;
        }
}