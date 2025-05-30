[System.Serializable]
public class ArtificialSun : BaseUpgrader
{
    public ArtificialSun(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        if (count == 1) cycle.IncreaseDay(3f);
        else cycle.IncreaseDay(1.5f);
    }

    public override void Worse()
    {
        if (count == 0) cycle.IncreaseDay(-3f);
        else cycle.IncreaseDay(-1.5f);
    }
}