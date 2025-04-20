[System.Serializable]
public class SecretWarehouses : Progress
{
        public SecretWarehouses(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.TreftFactor -= 0.1f;
        }
}