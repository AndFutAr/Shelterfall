[System.Serializable]
public class StudyOfGameTheory : Progress
{
        public StudyOfGameTheory(int _startPrice, int _count, bool _isOneTime) : base(_startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                if (count == 1) controller.RaceData.ChanceAvoidLossBid += 0.1f;
                else controller.RaceData.ChanceAvoidLossBid += 0.05f;
        }
}