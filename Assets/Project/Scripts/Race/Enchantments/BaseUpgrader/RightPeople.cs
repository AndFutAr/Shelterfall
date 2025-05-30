[System.Serializable]
public class RightPeople : BaseUpgrader
{
    public RightPeople(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        if(count == 1) cycle.MinusCostFactor(0.05f);
        else cycle.MinusCostFactor(0.025f);
    }

    public override void Worse()
    {
        if(count == 0) cycle.MinusCostFactor(-0.05f);
        else cycle.MinusCostFactor(-0.025f);
    }
}