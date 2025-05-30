[System.Serializable]
public class RegeneratingAloys : Progress
{
        public RegeneratingAloys(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.HealFactor += 0.1f;
        }
}