[System.Serializable]
public class LocalResearch : Progress
{
        public LocalResearch(RaceController _controller, int _startPrice, int _count, bool _isOneTime) : 
                base(_controller, _startPrice, _count, _isOneTime) { }
        public override void ProgressUp()
        {
                controller.RaceData.AddTwists += 1;
        }
}