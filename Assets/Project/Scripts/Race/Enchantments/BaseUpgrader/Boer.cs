using UnityEngine;

[System.Serializable]
public class Boer : BaseUpgrader
{
    public Boer(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        if (count == 1) cycle.NewDayBase(0.5f);
        else cycle.NewDayBase(0.25f);
    }

    public override void Worse()
    {
        if (count == 0) cycle.NewDayBase(-0.5f);
        else cycle.NewDayBase(-0.25f);
    }
}