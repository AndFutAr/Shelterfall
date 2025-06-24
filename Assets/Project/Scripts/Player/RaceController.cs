using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct RaceData
{
    public float SoundVolume;
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
    
    public Dictionary<Int32, Progress> _progressList;
}
public class RaceController : MonoBehaviour
{
    private SaveLoadRaceData slRace = new SaveLoadRaceData();

    public void SaveRace()
    {
        RaceData.SoundVolume = ui.SoundVolume;
        slRace.SaveRace(RaceData);
    }

    public DisasterAnimationsMediator DisasterAnimationsMediator;
    public RaceData RaceData;
    public CycleTimeReader timeReader;
    public PepperBank pepperBank;
    public UI_Controller ui;
    
    private CycleComponent _cycle;
    private Shelter shelter;
    public CycleComponent Cycle => _cycle;
    
    [SerializeField] private GameObject _cycleControllerPrefab, _cycleController;
    
    [SerializeField] private ShelterView _shelterView;

    void Awake()
    { 
        if (slRace.LoadRace() == 0)
        {
            RaceData.SoundVolume = 1;
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
            RaceData.ChanceAvoidLossBid = 0;

            RaceData._progressList = new Dictionary<int, Progress>()
            {
                { 0, new EternalRepair(this, 100, 0, false) },
                { 1, new AnomalousShield(this, 125, 0, false) },
                { 2, new RegeneratingAloys(this, 100, 0, false) },
                { 3, new SecondaryProcessing(this, 100, 0, false) },
                { 4, new SecretWarehouses(this, 100, 0, false)},
                { 5, new LocalResearch(this, 200, 0, false) },
                { 6, new PocketDimension(this, 150, 0, false) },
                { 7, new AnomalousRadioReceiver(this, 400, 0, true) },
                { 8, new ReverseTimeClock(this, 600, 0, true) },
                { 9, new SchrodingerHat(this, 800, 0, true) },
                { 10, new StudyOfGameTheory(this, 150, 0, false) }
            };
        }
        else
        {
            RaceData.SoundVolume = slRace.SoundVolume;
            RaceData.BestRace = slRace.BestRace;
            RaceData.RaceNum = slRace.RaceNum;
            RaceData.PepperFragments = slRace.PepperFragments;
            RaceData.IsRace = slRace.IsRace;

            RaceData.MaxHpFactor = slRace.MaxHpFactor;
            RaceData.BossDamageFactor = slRace.BossDamageFactor;
            RaceData.HealFactor = slRace.HealFactor;
            RaceData.MaxHp = (int)(100 * RaceData.MaxHpFactor);

            RaceData.PepperFactor = slRace.PepperFactor;
            RaceData.TreftFactor = slRace.TreftFactor;
            RaceData.AddTwists = slRace.AddTwists;
            RaceData.PassivePepper = slRace.PassivePepper;

            RaceData.IsRerollBoss = slRace.IsRerollBoss;
            RaceData.IsRerollDef = slRace.IsRerollDef;
            RaceData.IsAvoidDeath = slRace.IsAvoidDeath;
            RaceData.ChanceAvoidLossBid = slRace.ChanceAvoidLossBid;

            RaceData._progressList = new Dictionary<int, Progress>()
            {
                { 0, new EternalRepair(this, 100, slRace.eternalRepair, false) },
                { 1, new AnomalousShield(this, 125, slRace.anomalousShield, false) },
                { 2, new RegeneratingAloys(this, 100, slRace.regeneratingAloys, false) },
                { 3, new SecondaryProcessing(this, 100, slRace.secondaryProcessing, false) },
                { 4, new SecretWarehouses(this, 100, slRace.secretWarehouses, false) },
                { 5, new LocalResearch(this, 200, slRace.localResearch, false) },
                { 6, new PocketDimension(this, 150, slRace.pocketDimension, false) },
                { 7, new AnomalousRadioReceiver(this, 400, slRace.anomalousRadioReceiver, true) },
                { 8, new ReverseTimeClock(this, 600, slRace.reverseTimeClock, true) },
                { 9, new SchrodingerHat(this, 800, slRace.schrodingerHat, true) },
                { 10, new StudyOfGameTheory(this, 150, slRace.studyOfGameTheory, false) }
            };
        }
        SaveRace();
    }
    private void Update()
    {
        if (_cycle != null)
        {
            if ((shelter.HP <= 0 || _cycle.CycleData.cycleNum >= 50) && timeReader.IsNight())
            {
                if (_cycle.CycleData.cycleNum >= 50) ui.RaceWin();
                else if (_cycle.RaceData.IsAvoidDeath == 1 && _cycle.CycleData.cycleNum < 50)
                {
                    _cycle.RaceData.IsAvoidDeath = 0;
                    shelter.SetShelter(shelter.MaxHP, shelter.MaxHP / 2);
                }
                else ui.RaceOver();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) Debug.Log(RaceData.MaxHpFactor);
    }

    public void UpgradeProgress(int t)
    {
        RaceData._progressList[t].UpgradeProgress();
    }

    public void LoseRace(int fragmentFactor)
    {
        if (_cycle.CycleData.cycleNum > RaceData.BestRace) RaceData.BestRace = _cycle.CycleData.cycleNum;
        if(timeReader.IsRacing()) timeReader.InverseRace();
        RaceData.PepperFragments += fragmentFactor * (int)(_cycle.CycleData.cycleNum * 5 + _cycle.CycleData.allPepper * 0.1f);
        _cycle.Final(); 
        RaceData.IsRace = 0;
        SaveRace();
        ui.RaceExit();
    }
    public void StartRace()
    {
        if (!timeReader.IsRacing())
        {
            if (_cycleController != null) Destroy(_cycleController);
            RaceData.RaceNum++;
            SaveRace();
            
            _cycleController = Instantiate(_cycleControllerPrefab, transform);
            _cycle = _cycleController.GetComponent<CycleComponent>();
            shelter = _cycleController.GetComponent<Shelter>();
            _cycle.TimeReader = timeReader;
            _cycle.RaceData = RaceData;
            _cycle.PepperBank = pepperBank;
            _cycle.SetCanvas(ui.Canvas());
            _cycle.SetAnimationMediator(DisasterAnimationsMediator);

            _shelterView.GetShelter(_cycleController.GetComponent<Shelter>());
            ui.SetUI(_cycle);
            ui.StartGame();
            
            RaceData.IsRace = 1;
            SaveRace();
        }
    }
}