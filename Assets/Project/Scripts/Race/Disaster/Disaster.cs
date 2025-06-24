using System.Collections;
using Project.Scripts.Race.Disaster.Animations;
using UnityEngine;

public abstract class Disaster
{
    public float rangeHP;
    private readonly DisasterAnimation _disasterAnimation;
    public float rangeMaxHP, rangeTreft, disasterFactor = 1;
    public CycleComponent cycle;
    public PepperStorage storage;
    public Shelter shelter;

    public Disaster(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter,
        float range, ElementUpgrader _elementUpgrader, DisasterAnimation disasterAnimation)
    {
        cycle = _cycle;
        storage = _storage;
        shelter = _shelter;
        
        rangeHP *= range;
        _disasterAnimation = disasterAnimation;
        disasterFactor = _elementUpgrader.ElementFactor;
    }

    public void OpenDisaster()
    {
        storage.TreftPepper(rangeTreft * disasterFactor * cycle.RaceData.TreftFactor);
        shelter.SpendHP(rangeHP * disasterFactor);
        shelter.SpendMaxHP(rangeMaxHP * disasterFactor);
        Special();
    }

    public IEnumerator Animate()
    {
        yield return _disasterAnimation.Animate();
    }
    
    public abstract void Special();
}