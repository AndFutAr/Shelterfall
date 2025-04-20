using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SaveLoadCycleData
{
    public int cycleNum;
    public float cycleTime;
    public int maxHP;
    public int lastHP;
    public float heal;
    public float lastPepper;
    public float costFactor;
    public float allPepper;
    
    public int qualityMaterials;
    public int repairRobots;
    public int boer;
    public int artificialSun;
    public int rightPeople;
    public int majorRepairs;
    
    public int meteorDefender;
    public int earthDefender;
    public int energyDefender;
    public int cyberDefender;
    public int fogDefender;

    public float dayRange;
    public float dayRange2;
    public float eveningRange;
    public float nightRange;

    public float dayBase;
    public float day2Base;
    public float eveningBase;
    public float nightBase;
    

    public void SaveCycle(CycleData data, RaceController controller)
    {
        PlayerPrefs.SetInt("CycleNum", data.cycleNum);
        PlayerPrefs.SetFloat("CycleTime", data.cycleTime);
        PlayerPrefs.SetInt("MaxHP", data.maxHP);
        PlayerPrefs.SetInt("LastHP", data.lastHP);
        PlayerPrefs.SetFloat("Heal", data.heal);
        PlayerPrefs.SetFloat("LastPepper", data.lastPepper);
        PlayerPrefs.SetFloat("Costfactor", data.costFactor);
        PlayerPrefs.SetFloat("AllPepper", data.allPepper);
        
        PlayerPrefs.SetInt("QualityMaterials", data.qualityMaterials.Count);
        PlayerPrefs.SetInt("RepairRobots", data.repairRobots.Count);
        PlayerPrefs.SetInt("Boer", data.boer.Count);
        PlayerPrefs.SetInt("ArtificialSun", data.artificialSun.Count);
        PlayerPrefs.SetInt("RightPeople", data.rightPeople.Count);
        PlayerPrefs.SetInt("MajorRepairs", data.majorRepairs.Count);
        
        PlayerPrefs.SetInt("MeteorDefender", data.meteorDefender.Count);
        PlayerPrefs.SetInt("EarthDefender", data.earthDefender.Count);
        PlayerPrefs.SetInt("EnergyDefender", data.energyDefender.Count);
        PlayerPrefs.SetInt("CyberDefender", data.cyberDefender.Count);
        PlayerPrefs.SetInt("FogDefender", data.fogDefender.Count);
        
        PlayerPrefs.SetFloat("DayRange", data.dayRange);
        PlayerPrefs.SetFloat("DayRange2", data.dayRange2);
        PlayerPrefs.SetFloat("EveningRange", data.eveningRange - controller.RaceData.AddTwists);
        PlayerPrefs.SetFloat("NightRange", data.nightRange);
        
        PlayerPrefs.SetFloat("DayBase", data.dayBase);
        PlayerPrefs.SetFloat("Day2Base", data.day2Base);
        PlayerPrefs.SetFloat("EveningBase", data.eveningBase - controller.RaceData.AddTwists);
        PlayerPrefs.SetFloat("NightBase", data.nightBase);
    }
    public void LoadCycle()
    {
        cycleNum = PlayerPrefs.GetInt("CycleNum");
        cycleTime = PlayerPrefs.GetFloat("CycleTime");
        maxHP = PlayerPrefs.GetInt("MaxHP");
        lastHP = PlayerPrefs.GetInt("LastHP");
        heal = PlayerPrefs.GetFloat("Heal");
        lastPepper = PlayerPrefs.GetFloat("LastPepper");
        costFactor = PlayerPrefs.GetFloat("Costfactor");
        allPepper = PlayerPrefs.GetFloat("AllPepper");
        
        qualityMaterials = PlayerPrefs.GetInt("QualityMaterials");
        repairRobots = PlayerPrefs.GetInt("RepairRobots");
        boer = PlayerPrefs.GetInt("Boer");
        artificialSun = PlayerPrefs.GetInt("ArtificialSun");
        rightPeople = PlayerPrefs.GetInt("RightPeople");
        majorRepairs = PlayerPrefs.GetInt("MajorRepairs");
        
        meteorDefender = PlayerPrefs.GetInt("MeteorDefender");
        earthDefender = PlayerPrefs.GetInt("EarthDefender");
        energyDefender = PlayerPrefs.GetInt("EnergyDefender");
        cyberDefender = PlayerPrefs.GetInt("CyberDefender");
        fogDefender = PlayerPrefs.GetInt("FogDefender");
        
        dayRange = PlayerPrefs.GetFloat("DayRange");
        dayRange2 = PlayerPrefs.GetFloat("DayRange2");
        eveningRange = PlayerPrefs.GetFloat("EveningRange");
        nightRange = PlayerPrefs.GetFloat("NightRange");
        
        dayBase = PlayerPrefs.GetFloat("DayBase");
        day2Base = PlayerPrefs.GetFloat("Day2Base");
        eveningBase = PlayerPrefs.GetFloat("EveningBase");
        nightBase = PlayerPrefs.GetFloat("NightBase");
    }
}
