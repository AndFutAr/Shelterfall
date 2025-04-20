public class AnomalyFog : Disaster
{
    public AnomalyFog(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, float range, ElementUpgrader _elementUpgrader) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader)
    {
        rangeHP = 0.1f;
        rangeMaxHP = 0.1f;
        rangeTreft = 0.4f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetDayRange2(0.6f);
        shelter.HealHP(0.1f);
    }
}