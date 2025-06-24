using Project.Scripts.Race.Disaster.Animations;

public class EnergyStormBoss : Disaster
{
    public EnergyStormBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter,
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.3f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.25f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.5f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetNightRange(1.5f);
    }
}