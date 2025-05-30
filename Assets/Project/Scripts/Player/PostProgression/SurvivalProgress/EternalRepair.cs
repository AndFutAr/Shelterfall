[System.Serializable]
public class EternalRepair : Progress
{
        public EternalRepair(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.MaxHpFactor += 0.1f;
                controller.RaceData.MaxHp = (int)(100 * controller.RaceData.MaxHpFactor);
        }
}