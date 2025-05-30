[System.Serializable]
public class AnomalousShield : Progress
{
        public AnomalousShield(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.BossDamageFactor -= 0.05f;
        }
}