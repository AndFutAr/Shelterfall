using UnityEngine;

public abstract class Disaster
{
    public float rangeHP, rangeMaxHP, rangeTreft, disasterFactor = 1;
    public CycleComponent cycle;
    public PepperStorage storage;
    public Shelter shelter;

    public Disaster(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, float range, ElementUpgrader _elementUpgrader)
    {
        cycle = _cycle;
        storage = _storage;
        shelter = _shelter;
        
        rangeHP *= range;
        disasterFactor = _elementUpgrader.ElementFactor;
    }

    public void OpenDisaster()
    {
        storage.TreftPepper(rangeTreft * disasterFactor * cycle.RaceData.TreftFactor);
        shelter.SpendHP(rangeHP * disasterFactor);
        shelter.SpendMaxHP(rangeMaxHP * disasterFactor);
        Special();
    }
    public abstract void Special();
}