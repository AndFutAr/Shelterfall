[System.Serializable]
public class AnomalousShield : Progress
{
        public AnomalousShield(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.BossDamageFactor -= 0.05f;
        }
}