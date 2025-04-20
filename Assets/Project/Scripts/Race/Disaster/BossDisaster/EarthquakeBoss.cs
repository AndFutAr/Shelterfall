public class EarthquakeBoss : Disaster
{
    public EarthquakeBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, float range, ElementUpgrader _elementUpgrader) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader)
    {
        rangeHP = 0.3f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.2f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.2f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        int chance = UnityEngine.Random.Range(0, 60);
        int defenderNum = chance / 10;
        switch (defenderNum)
        {
            case 0: cycle.CycleData.qualityMaterials.LowerUpgrader(); break;
            case 1: cycle.CycleData.repairRobots.LowerUpgrader(); break;
            case 2: cycle.CycleData.boer.LowerUpgrader(); break;
            case 3: cycle.CycleData.artificialSun.LowerUpgrader(); break;
            case 4: cycle.CycleData.rightPeople.LowerUpgrader(); break;
            case 5: cycle.CycleData.majorRepairs.LowerUpgrader(); break;
        }
        
        int chanceEl = UnityEngine.Random.Range(0, 50);
        int defenderElNum = chance / 10;
        switch (defenderElNum)
        {
            case 0: cycle.CycleData.meteorDefender.LowerUpgrader(); break;
            case 1: cycle.CycleData.earthDefender.LowerUpgrader(); break;
            case 2: cycle.CycleData.energyDefender.LowerUpgrader(); break;
            case 3: cycle.CycleData.cyberDefender.LowerUpgrader(); break;
            case 4: cycle.CycleData.fogDefender.LowerUpgrader(); break;
        }
    }
}