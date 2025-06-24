using Project.Scripts.Race.Disaster.Animations;

public class EnergyStorm : Disaster
{
    public EnergyStorm(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, 
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.2f;
        rangeMaxHP = 0.1f;
        rangeTreft = 0.25f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetNightRange(1.3f);
    }
}