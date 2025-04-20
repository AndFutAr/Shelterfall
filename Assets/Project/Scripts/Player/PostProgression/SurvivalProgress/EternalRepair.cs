[System.Serializable]
public class EternalRepair : Progress
{
        public EternalRepair(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.MaxHpFactor += 0.1f;
        }
}