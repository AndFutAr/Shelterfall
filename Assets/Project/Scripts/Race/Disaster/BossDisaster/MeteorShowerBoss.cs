public class MeteorShowerBoss : Disaster
{
    public MeteorShowerBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, float range, ElementUpgrader _elementUpgrader) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader)
    {
        rangeHP = 0.25f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.3f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.3f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        cycle.SetDayRange(0.65f);
    }
}