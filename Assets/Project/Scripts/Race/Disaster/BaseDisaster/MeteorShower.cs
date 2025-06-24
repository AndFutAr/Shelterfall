using Project.Scripts.Race.Disaster.Animations;

public class MeteorShower : Disaster
{
    public MeteorShower(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, 
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.2f;
        rangeMaxHP = 0.15f;
        rangeTreft = 0.2f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetDayRange(0.65f);
    }
}