[System.Serializable]
public class SecondaryProcessing : Progress
{
        public SecondaryProcessing(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.PepperFactor += 0.1f;
        }
}