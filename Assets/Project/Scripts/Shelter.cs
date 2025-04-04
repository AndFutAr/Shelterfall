using System;
using UnityEngine;

[Serializable] 
public class Shelter
{
    public Shelter(int MaxHp) => HP = MaxHp;
    private int maxHp = 100, hp;
    
    public int HP
    {
        get => hp;
        set => hp = value;
    }
}
