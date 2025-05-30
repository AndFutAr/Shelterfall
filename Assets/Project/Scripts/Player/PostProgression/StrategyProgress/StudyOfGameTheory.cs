[System.Serializable]
public class StudyOfGameTheory : Progress
{
        public StudyOfGameTheory(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                if (count == 1) controller.RaceData.ChanceAvoidLossBid += 0.1f;
                else controller.RaceData.ChanceAvoidLossBid += 0.05f;
        }
}