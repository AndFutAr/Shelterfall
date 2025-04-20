[System.Serializable]
public class RepairRobots : BaseUpgrader
{
    public RepairRobots(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        if(count == 1) cycle.PlusHeal(0.1f);
        else cycle.PlusHeal(0.05f);
    }

    public override void Worse()
    {
        if(count == 0) cycle.PlusHeal(-0.1f);
        else cycle.PlusHeal(-0.05f);
    }
}