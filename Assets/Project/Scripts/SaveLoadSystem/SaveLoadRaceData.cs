﻿using UnityEngine;
using System;

public class SaveLoadRaceData
{
    public int returner;

    public float SoundVolume;
    public int BestRace;
    public int RaceNum;
    public int PepperFragments;
    public int IsRace;
    
    public float MaxHpFactor;
    public float BossDamageFactor;
    public float HealFactor;
    public int MaxHp;
    
    public float PepperFactor;
    public float TreftFactor;
    public int AddTwists;
    public int PassivePepper;

    public int IsRerollBoss;
    public int IsRerollDef;
    public int IsAvoidDeath;
    public float ChanceAvoidLossBid;
    
    public int eternalRepair;
    public int anomalousShield;
    public int regeneratingAloys;   
    
    public int secondaryProcessing;
    public int secretWarehouses;
    public int localResearch;
    public int pocketDimension;
    
    public int anomalousRadioReceiver;
    public int reverseTimeClock;
    public int schrodingerHat;
    public int studyOfGameTheory;

    public void SaveRace(RaceData data)
    {
        PlayerPrefs.SetFloat("SoundVolume", data.SoundVolume);
        PlayerPrefs.SetInt("BestRace", data.BestRace);
        PlayerPrefs.SetInt("RaceNum", data.RaceNum);
        PlayerPrefs.SetInt("PepperFragments", data.PepperFragments);
        PlayerPrefs.SetInt("IsRace", data.IsRace);
        PlayerPrefs.SetInt("MaxHp", data.MaxHp);
        
        PlayerPrefs.SetFloat("MaxHpFactor", data.MaxHpFactor);
        PlayerPrefs.SetFloat("BossDamageFactor", data.BossDamageFactor);
        PlayerPrefs.SetFloat("HealFactor", data.HealFactor);
        
        PlayerPrefs.SetFloat("PepperFactor", data.PepperFactor);
        PlayerPrefs.SetFloat("TreftFactor", data.TreftFactor);
        PlayerPrefs.SetInt("AddTwists", data.AddTwists);
        PlayerPrefs.SetInt("PassivePepper", data.PassivePepper);
        
        PlayerPrefs.SetInt("IsRerollBoss", data.IsRerollBoss);
        PlayerPrefs.SetInt("IsRerollDef", data.IsRerollDef);
        PlayerPrefs.SetInt("IsAvoidDeath", data.IsAvoidDeath);
        PlayerPrefs.SetFloat("ChanceAvoidLossBid", data.ChanceAvoidLossBid);
        
        PlayerPrefs.SetInt("eternalRepair", data._progressList[0].Count);
        PlayerPrefs.SetInt("anomalousShield", data._progressList[1].Count);
        PlayerPrefs.SetInt("regeneratingAloys", data._progressList[2].Count);
        
        PlayerPrefs.SetInt("secondaryProcessing", data._progressList[3].Count);
        PlayerPrefs.SetInt("secretWarehouses", data._progressList[4].Count);
        PlayerPrefs.SetInt("localResearch", data._progressList[5].Count);
        PlayerPrefs.SetInt("pocketDimension", data._progressList[6].Count);
        
        PlayerPrefs.SetInt("anomalousRadioReceiver", data._progressList[7].Count);
        PlayerPrefs.SetInt("reverseTimeClock", data._progressList[8].Count);
        PlayerPrefs.SetInt("schrodingerHat", data._progressList[9].Count);
        PlayerPrefs.SetInt("studyOfGameTheory", data._progressList[10].Count);
    }

    public int LoadRace()
    {
        if (!PlayerPrefs.HasKey("RaceNum")) return 0;
        
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        BestRace = PlayerPrefs.GetInt("BestRace");
        RaceNum = PlayerPrefs.GetInt("RaceNum");
        PepperFragments = PlayerPrefs.GetInt("PepperFragments");
        IsRace = PlayerPrefs.GetInt("IsRace");
        MaxHp = PlayerPrefs.GetInt("MaxHp");
        
        MaxHpFactor = PlayerPrefs.GetFloat("MaxHpFactor");
        BossDamageFactor = PlayerPrefs.GetFloat("BossDamageFactor");
        HealFactor = PlayerPrefs.GetFloat("HealFactor");
        
        PepperFactor = PlayerPrefs.GetFloat("PepperFactor");
        TreftFactor = PlayerPrefs.GetFloat("TreftFactor");
        AddTwists = PlayerPrefs.GetInt("AddTwists");
        PassivePepper = PlayerPrefs.GetInt("PassivePepper");
        
        IsRerollBoss = PlayerPrefs.GetInt("IsRerollBoss");
        IsRerollDef = PlayerPrefs.GetInt("IsRerollDef");
        IsAvoidDeath = PlayerPrefs.GetInt("IsAvoidDeath");
        ChanceAvoidLossBid = PlayerPrefs.GetFloat("ChanceAvoidLossBid");
        
        eternalRepair = PlayerPrefs.GetInt("eternalRepair");
        anomalousShield = PlayerPrefs.GetInt("anomalousShield");
        regeneratingAloys = PlayerPrefs.GetInt("regeneratingAloys");
        
        secondaryProcessing = PlayerPrefs.GetInt("secondaryProcessing");
        secretWarehouses = PlayerPrefs.GetInt("secretWarehouses");
        localResearch = PlayerPrefs.GetInt("localResearch");
        pocketDimension = PlayerPrefs.GetInt("pocketDimension");
        
        anomalousRadioReceiver = PlayerPrefs.GetInt("anomalousRadioReceiver");
        reverseTimeClock = PlayerPrefs.GetInt("reverseTimeClock");
        schrodingerHat = PlayerPrefs.GetInt("schrodingerHat");
        studyOfGameTheory = PlayerPrefs.GetInt("studyOfGameTheory");

        return 1;
    }
}