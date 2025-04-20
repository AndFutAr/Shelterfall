[System.Serializable]
public class SchrodingerHat : Progress
{
        public SchrodingerHat(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.IsAvoidDeath = 1;
        }
}