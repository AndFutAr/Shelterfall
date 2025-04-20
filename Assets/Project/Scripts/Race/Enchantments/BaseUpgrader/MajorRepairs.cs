[System.Serializable]
public class MajorRepairs : BaseUpgrader
{
    public MajorRepairs(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        shelter.HealHP(0.3f);
    }

    public override void Worse() { }
}