using System;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    public Shelter(int MaxHp) => hp = MaxHp;
    private int maxHp = 100, hp;

    public void SetShelter(int MaxHp, int LastHp)
    {
        maxHp = MaxHp;
        hp = LastHp;
    }
    public float HP => hp;
    public int MaxHP => maxHp;

    private void Update()
    {
        if(hp > maxHp) hp = maxHp;
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
    }

    public void SpendHP(float range)
    {
        hp -= (int)(hp * range);
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
    }
    public void SpendMaxHP(float range)
    {
        hp -= (int)(maxHp * range);
        transform.GetComponent<CycleComponent>().CycleData.maxHP = maxHp;
    }
    public void PlusHP(float range)
    {
        hp += (int)range;
        maxHp += (int)range;
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
        transform.GetComponent<CycleComponent>().CycleData.maxHP = maxHp;
    }

    public void HealHP(float range)
    {
        hp += (int)(maxHp * range * transform.GetComponent<CycleComponent>().RaceData.HealFactor);
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
    }
}
