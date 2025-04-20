using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NightOperator : Operator
{
    private float damage;
    [SerializeField] private bool isBid = false;
    [SerializeField] private float bidRange = 0;
    private Disaster disaster, bidDisaster;
    private Dictionary<Int32, Disaster> _nightDisasters;
    
    public void SetOperator(float range) => damage = range;

    void Awake()
    {
        cycle = transform.parent.GetComponent<CycleComponent>();
        storage = transform.parent.GetComponent<PepperStorage>();
        shelter = transform.parent.GetComponent<Shelter>();
    }
    private void Start()
    {
        if(cycle.transform.parent.GetComponent<RaceController>().RaceData.IsRerollDef == 1 && cycle.CycleData.cycleNum % 5 == 0)
            cycle.RaceData.IsRerollBoss = 1;
        
        _nightDisasters = new Dictionary<Int32, Disaster>()
        {
            { 0, new MeteorShower(cycle, storage, shelter, damage, cycle.CycleData.meteorDefender) },
            { 1, new Earthquake(cycle, storage, shelter, damage, cycle.CycleData.earthDefender) },
            { 2, new EnergyStorm(cycle, storage, shelter, damage, cycle.CycleData.energyDefender) },
            { 3, new CyberInvasion(cycle, storage, shelter, damage, cycle.CycleData.cyberDefender) },
            { 4, new AnomalyFog(cycle, storage, shelter, damage, cycle.CycleData.fogDefender) },
            { 5, new MeteorShowerBoss(cycle, storage, shelter, damage, cycle.CycleData.meteorDefender) },
            { 6, new EarthquakeBoss(cycle, storage, shelter, damage, cycle.CycleData.earthDefender) },
            { 7, new EnergyStormBoss(cycle, storage, shelter, damage, cycle.CycleData.energyDefender) },
            { 8, new CyberInvasionBoss(cycle, storage, shelter, damage, cycle.CycleData.cyberDefender) },
            { 9, new AnomalyFogBoss(cycle, storage, shelter, damage, cycle.CycleData.fogDefender) }
        };
    }

    public void Bid(int type, int disasterNum)
    {
        bidDisaster = _nightDisasters[type];
        bidRange = storage.PepperCount / 2.0f;
        storage.SpendPepper(bidRange);
        isBid = true;
        SpinTheWheel(disasterNum);
    }
    public void BidCount() =>
        storage.AddPepper(bidRange * 2.0f);

    public void ReturnBid() =>
        storage.AddPepper(bidRange);

    public void SpinTheWheel(int disasterNum)
    {
        StopCoroutine(Waiter());
        if (cycle.DayNum() % 5 != 0 || cycle.DayNum() < 5)
            disaster = _nightDisasters[disasterNum];
        else
            disaster = _nightDisasters[disasterNum + 5];
        StartCoroutine(Waiter());
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        StartDisaster();
    }
    public void StartDisaster()
    {
        disaster.OpenDisaster();

        if (isBid)
        {
            if (bidDisaster == disaster) {BidCount(); Debug.Log("you win");}
        }
        else
        {
            int chance = Random.Range(0, 100);
            if(chance <= cycle.RaceData.ChanceAvoidLossBid * 100)
                ReturnBid();
        }
        Debug.Log(disaster);
    }
}
