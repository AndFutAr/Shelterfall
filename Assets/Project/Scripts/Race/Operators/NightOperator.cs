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
    private Dictionary<int, Disaster> _nightDisasters;

    private float lastHP, diffHP, lastPeppers, diffPeppers;
    public int DiffHP => (int)diffHP;
    public int DiffPeppers => (int)diffPeppers;
    
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
        
        _nightDisasters = new Dictionary<int, Disaster>()
        {
            { 0, new MeteorShower(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[0]) },
            { 1, new Earthquake(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[1]) },
            { 2, new EnergyStorm(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[2]) },
            { 3, new CyberInvasion(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[3]) },
            { 4, new AnomalyFog(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[4]) },
            { 5, new MeteorShowerBoss(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[0]) },
            { 6, new EarthquakeBoss(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[1]) },
            { 7, new EnergyStormBoss(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[2]) },
            { 8, new CyberInvasionBoss(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[3]) },
            { 9, new AnomalyFogBoss(cycle, storage, shelter, damage, cycle.CycleData._ElDictionaries[4]) }
        };
    }

    public void ChooseBid(int type)
    {
        if (cycle.CycleData.cycleNum % 5 != 0) bidDisaster = _nightDisasters[type];
        else bidDisaster = _nightDisasters[type + 5];
    }
    public void Bid(int bidSize)
    {
        if (bidDisaster != null)
        {
            bidRange = bidSize;
            storage.SpendPepper(bidSize);
            isBid = true;
        }
    }
    public void BidCount() =>
        storage.AddPepper(bidRange * 2.0f);

    public void ReturnBid() =>
        storage.AddPepper(bidRange);

    public void SpinTheWheel(int disasterNum)
    {
        StopCoroutine(Waiter(disasterNum));
        if (cycle.DayNum() % 5 != 0 || cycle.DayNum() < 5)
            disaster = _nightDisasters[disasterNum];
        else
            disaster = _nightDisasters[disasterNum + 5];
        Debug.Log(disaster);
        StartCoroutine(Waiter(disasterNum));
    }
    IEnumerator Waiter(int disasterNum)
    {
        yield return new WaitForSeconds(4);
        StartDisaster(disasterNum);
    }
    public void StartDisaster(int disasterNum)
    {
        lastHP = shelter.HP;
        lastPeppers = storage.PepperCount;
        disaster.OpenDisaster();
        diffHP = lastHP - shelter.HP;
        diffPeppers = lastPeppers - storage.PepperCount;
            
        cycle.CycleData.lastDisaster = disasterNum;
        if (isBid)
        {
            if (bidDisaster == disaster)
            {
                BidCount(); 
                Debug.Log("you win");
            }
            else
            {
                Debug.Log("you lose");
                int chance = Random.Range(0, 100);
                if (chance < cycle.RaceData.ChanceAvoidLossBid * 100)
                {
                    Debug.Log("return");
                    ReturnBid();
                }
            }
        }
    }
}
