[System.Serializable]
public class PocketDimension : Progress
{
        public PocketDimension(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                if (count == 1) controller.RaceData.PassivePepper += 20;
                else controller.RaceData.PassivePepper = 10;
        }
}