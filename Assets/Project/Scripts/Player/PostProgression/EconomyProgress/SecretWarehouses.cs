[System.Serializable]
public class SecretWarehouses : Progress
{
        public SecretWarehouses(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.TreftFactor -= 0.1f;
        }
}