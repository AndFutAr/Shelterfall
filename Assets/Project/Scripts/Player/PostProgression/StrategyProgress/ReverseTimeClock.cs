[System.Serializable]
public class ReverseTimeClock : Progress
{
        public ReverseTimeClock(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.IsRerollDef = 1;
        }
}