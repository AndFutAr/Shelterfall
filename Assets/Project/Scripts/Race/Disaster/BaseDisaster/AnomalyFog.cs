using Project.Scripts.Race.Disaster.Animations;

public class AnomalyFog : Disaster
{
    public AnomalyFog(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, float range, 
        ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.1f;
        rangeMaxHP = 0.1f;
        rangeTreft = 0.4f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetDayRange2(0.6f);
        shelter.HealHP(0.05f);
    }
}