using System;
using Project.Scripts.Race.Disaster.Animations;
using UnityEngine;

public class Earthquake : Disaster
{
    public Earthquake(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter,
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation) : 
        base(_cycle, _storage, _shelter, range, _elementUpgrader, disasterAnimation)
    {
        rangeHP = 0.2f;
        rangeMaxHP = 0.1f;
        rangeTreft = 0.1f;
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

        cycle.transform.GetChild(0).GetComponent<NightOperator>().LostDefender = defenderNum;
    }
}