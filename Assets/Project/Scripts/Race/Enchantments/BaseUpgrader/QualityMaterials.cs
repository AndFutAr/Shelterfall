using TMPro;

[System.Serializable]
public class QualityMaterials : BaseUpgrader
{
    public QualityMaterials(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount) : 
        base(_cycle, _storage, _shelter, _startPrice, _count, _maxCount) { }

    public override void Upgrade()
    {
        if (count == 1) shelter.PlusHP(0.2f * cycle.RaceData.MaxHpFactor);
        else shelter.PlusHP(0.1f * cycle.RaceData.MaxHpFactor);
    }

    public override void Worse()
    {
        if(count == 0) shelter.PlusHP(-0.2f * cycle.RaceData.MaxHpFactor);
        else shelter.PlusHP(-0.1f * cycle.RaceData.MaxHpFactor);
    }
}