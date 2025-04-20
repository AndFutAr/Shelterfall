using System;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    public Shelter(int MaxHp) => HP = MaxHp;
    private int maxHp = 100, hp;

    public void SetShelter(int MaxHp, int LastHp)
    {
        maxHp = MaxHp;
        hp = LastHp;
    }
    public int HP
    {
        get => hp;
        set => hp = value;
    }
    public int MaxHP => maxHp;

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
        maxHp += (int)(maxHp * range);
        hp += (int)(maxHp * range);
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
        transform.GetComponent<CycleComponent>().CycleData.maxHP = maxHp;
    }

    public void HealHP(float range)
    {
        hp += (int)(maxHp * range * transform.GetComponent<CycleComponent>().RaceData.HealFactor);
        transform.GetComponent<CycleComponent>().CycleData.lastHP = hp;
    }
}
