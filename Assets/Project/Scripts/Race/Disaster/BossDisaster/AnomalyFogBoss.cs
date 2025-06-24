using Project.Scripts.Race.Disaster.Animations;

public class AnomalyFogBoss : Disaster
{
    public AnomalyFogBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter,
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.1f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.25f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.8f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetDayRange2(0f);
        shelter.HealHP(0.2f);
    }
}