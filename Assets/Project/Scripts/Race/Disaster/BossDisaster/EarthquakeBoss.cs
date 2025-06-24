using Project.Scripts.Race.Disaster.Animations;

public class EarthquakeBoss : Disaster
{
    public EarthquakeBoss(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, 
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.3f * cycle.RaceData.BossDamageFactor;
        rangeMaxHP = 0.2f * cycle.RaceData.BossDamageFactor;
        rangeTreft = 0.2f;
    }
    public override void Special()
    {
        cycle.ResetTime();
        int defenderNum = 0, count = 0;
        for (int i = 0; i <= 5; i++)
        {
            if (cycle.CycleData._BaseDictionaries[i].Count >= 1)
            {
                count++;
                defenderNum = i;
            }
        }
        while (cycle.CycleData._BaseDictionaries[defenderNum].Count == 0 && count > 0)
        {
            int chance = UnityEngine.Random.Range(0, 60);
            defenderNum = chance / 10;
        }
        cycle.CycleData._BaseDictionaries[defenderNum].LowerUpgrader();
        
        int defenderElNum = 0, countEl = 0;
        for (int i = 0; i <= 4; i++)
        {
            if (cycle.CycleData._ElDictionaries[i].Count >= 1)
            {
                countEl++;
                defenderElNum = i;
            }
        }
        while (cycle.CycleData._ElDictionaries[defenderElNum].Count == 0 && countEl > 0)
        {
            int chanceEl = UnityEngine.Random.Range(0, 50);
            defenderElNum = chanceEl / 10;
        }
        cycle.CycleData._ElDictionaries[defenderElNum].LowerUpgrader();
        
        cycle.transform.GetChild(0).GetComponent<NightOperator>().LostDefender = defenderNum;
        cycle.transform.GetChild(0).GetComponent<NightOperator>().LostDefEl = defenderElNum;
    }
}