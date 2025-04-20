[System.Serializable]
public class AnomalousRadioReceiver : Progress
{
        public AnomalousRadioReceiver(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.IsRerollBoss = 1;
        }
}