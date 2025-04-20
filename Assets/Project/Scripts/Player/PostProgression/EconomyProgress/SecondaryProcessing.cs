[System.Serializable]
public class SecondaryProcessing : Progress
{
        public SecondaryProcessing(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.PepperFactor += 0.1f;
        }
}