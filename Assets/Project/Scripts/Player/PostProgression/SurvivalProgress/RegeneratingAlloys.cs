[System.Serializable]
public class RegeneratingAloys : Progress
{
        public RegeneratingAloys(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.HealFactor += 0.1f;
        }
}