using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct RaceData
{
    public int BestRace;
    public int RaceNum;
    public int PepperFragments;
    public int IsRace;

    public int MaxHp;
    
    public float MaxHpFactor;
    public float BossDamageFactor;
    public float HealFactor;
    
    public float PepperFactor;
    public float TreftFactor;
    public int AddTwists;
    public int PassivePepper;

    public int IsRerollBoss;
    public int IsRerollDef;
    public int IsAvoidDeath;
    public float ChanceAvoidLossBid;
    
    public EternalRepair eternalRepair;
    public AnomalousShield anomalousShield;
    public RegeneratingAloys regeneratingAloys;
    
    public SecondaryProcessing secondaryProcessing;
    public SecretWarehouses secretWarehouses;
    public LocalResearch localResearch;
    public PocketDimension pocketDimension;
    
    public AnomalousRadioReceiver anomalousRadioReceiver;
    public ReverseTimeClock reverseTimeClock;
    public SchrodingerHat schrodingerHat;
    public StudyOfGameTheory studyOfGameTheory;
}
public class RaceController : MonoBehaviour
{
    private SaveLoadRaceData slRace = new SaveLoadRaceData();
    public void SaveRace() => slRace.SaveRace(RaceData);
    public RaceData RaceData;
    public CycleTimeReader timeReader;
    public PepperBank pepperBank;
    public UI_Controller ui;
    
    private CycleComponent _cycle;
    private Shelter shelter;
    
    [SerializeField] private GameObject _cycleControllerPrefab, _cycleController;
    private Dictionary<Int32, Progress> _progressList;
    
    [SerializeField] private ShelterView _shelterView;

    void Awake()
    {
        if (slRace.LoadRace() == 0)
        {
            RaceData.BestRace = 0;
            RaceData.RaceNum = 1;
            RaceData.PepperFragments = 0;
            RaceData.IsRace = 0;

            RaceData.MaxHpFactor = 1;
            RaceData.BossDamageFactor = 1;
            RaceData.HealFactor = 1;
            RaceData.MaxHp = (int)(100 * RaceData.MaxHpFactor);

            RaceData.PepperFactor = 1;
            RaceData.TreftFactor = 1;
            RaceData.AddTwists = 0;
            RaceData.PassivePepper = 0;

            RaceData.IsRerollBoss = 0;
            RaceData.IsRerollDef = 0;
            RaceData.IsAvoidDeath = 0;
            RaceData.ChanceAvoidLossBid = 1;

            _progressList = new Dictionary<int, Progress>()
            {
                { 0, RaceData.eternalRepair = new EternalRepair(100, 0, false) },
                { 1, RaceData.anomalousShield = new AnomalousShield(125, 0, false) },
                { 2, RaceData.regeneratingAloys = new RegeneratingAloys(100, 0, false) },
                { 3, RaceData.secondaryProcessing = new SecondaryProcessing(100, 0, false) },
                { 4, RaceData.secretWarehouses = new SecretWarehouses(100, 0, false)},
                { 5, RaceData.localResearch = new LocalResearch(200, 0, false) },
                { 6, RaceData.pocketDimension = new PocketDimension(150, 0, false) },
                { 7, RaceData.anomalousRadioReceiver = new AnomalousRadioReceiver(400, 0, true) },
                { 8, RaceData.reverseTimeClock = new ReverseTimeClock(600, 0, true) },
                { 9, RaceData.schrodingerHat = new SchrodingerHat(800, 0, true) },
                { 10, RaceData.studyOfGameTheory = new StudyOfGameTheory(150, 0, false) }
            };
        }
        else
        {
            RaceData.BestRace = slRace.BestRace;
            RaceData.RaceNum = slRace.RaceNum;
            RaceData.PepperFragments = slRace.PepperFragments;
            RaceData.IsRace = slRace.IsRace;

            RaceData.MaxHpFactor = slRace.MaxHpFactor;
            RaceData.BossDamageFactor = slRace.BossDamageFactor;
            RaceData.HealFactor = slRace.HealFactor;
            RaceData.MaxHp = (int)(100 * slRace.MaxHpFactor);

            RaceData.PepperFactor = slRace.PepperFactor;
            RaceData.TreftFactor = slRace.TreftFactor;
            RaceData.AddTwists = slRace.AddTwists;
            RaceData.PassivePepper = slRace.PassivePepper;

            RaceData.IsRerollBoss = slRace.IsRerollBoss;
            RaceData.IsRerollDef = slRace.IsRerollDef;
            RaceData.IsAvoidDeath = slRace.IsAvoidDeath;
            RaceData.ChanceAvoidLossBid = slRace.ChanceAvoidLossBid;

            _progressList = new Dictionary<int, Progress>()
            {
                { 0, RaceData.eternalRepair = new EternalRepair(100, slRace.eternalRepair, false) },
                { 1, RaceData.anomalousShield = new AnomalousShield(125, slRace.anomalousShield, false) },
                { 2, RaceData.regeneratingAloys = new RegeneratingAloys(100, slRace.regeneratingAloys, false) },
                { 3, RaceData.secondaryProcessing = new SecondaryProcessing(100, slRace.secondaryProcessing, false) },
                { 4, RaceData.secretWarehouses = new SecretWarehouses(100, slRace.secretWarehouses, false) },
                { 5, RaceData.localResearch = new LocalResearch(200, slRace.localResearch, false) },
                { 6, RaceData.pocketDimension = new PocketDimension(150, slRace.pocketDimension, false) },
                { 7, RaceData.anomalousRadioReceiver = new AnomalousRadioReceiver(400, slRace.anomalousRadioReceiver, true) },
                { 8, RaceData.reverseTimeClock = new ReverseTimeClock(600, slRace.reverseTimeClock, true) },
                { 9, RaceData.schrodingerHat = new SchrodingerHat(800, slRace.schrodingerHat, true) },
                { 10, RaceData.studyOfGameTheory = new StudyOfGameTheory(150, slRace.studyOfGameTheory, false) }
            };
        }
        slRace.SaveRace(RaceData);
    }
    private void Update()
    {
        if (_cycle != null)
        {
            if (shelter.HP <= 0 && timeReader.IsNight())
            {
                if (_cycle.RaceData.IsAvoidDeath == 1)
                {
                    _cycle.RaceData.IsAvoidDeath = 0;
                    shelter.SetShelter(RaceData.MaxHp, shelter.MaxHP / 2);
                }
                else LoseRace();
            }
        }
    }

    public void UpgradeProgress(int t)
    {
        Debug.Log(_progressList[t]);
        _progressList[t].UpgradeProgress();
    }

    public void LoseRace()
    {
        if (_cycle.CycleData.cycleNum > RaceData.BestRace) RaceData.BestRace = _cycle.CycleData.cycleNum;
        if(timeReader.IsRacing()) timeReader.InverseRace();
        RaceData.PepperFragments += (int)(_cycle.CycleData.cycleNum * 5 + _cycle.CycleData.allPepper * 0.1f);
        _cycle.Final(); 
        RaceData.IsRace = 0;
        SaveRace();
    }
    public void StartRace()
    {
        if (!timeReader.IsRacing())
        {
            timeReader.InverseRace();
            if (_cycleController != null) Destroy(_cycleController);
            RaceData.RaceNum++;
            _cycleController = Instantiate(_cycleControllerPrefab, transform);
            _cycle = _cycleController.GetComponent<CycleComponent>();
            shelter = _cycleController.GetComponent<Shelter>();
            _cycle.TimeReader = timeReader;
            _cycle.RaceData = RaceData;
            _cycle.PepperBank = pepperBank;
            _cycle.SetCanvas(ui.Canvas());

            _shelterView.GetShelter(_cycleController.GetComponent<Shelter>());
            ui.SetUI(_cycle);
            ui.StartGame();
        }
    }
}