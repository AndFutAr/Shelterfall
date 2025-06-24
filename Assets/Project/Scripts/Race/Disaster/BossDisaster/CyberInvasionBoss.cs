using Project.Scripts.Race.Disaster.Animations;

public class CyberInvasionBoss : Disaster
{
    public CyberInvasionBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter,
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.1f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.3f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.4f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetEveningRange(0);
    }
}