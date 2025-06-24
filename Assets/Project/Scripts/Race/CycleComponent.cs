using System;
using System.Collections.Generic;
using UnityEngine;

public struct CycleData
{
    public int cycleNum;
    public float cycleTime;
    public int maxHP;
    public int lastHP;
    public float heal;
    public float lastPepper;
    public float costFactor;
    public float allPepper;

    public Dictionary<int, BaseUpgrader> _BaseDictionaries;
    public Dictionary<int, ElementUpgrader> _ElDictionaries;
    public int lastDisaster;

    public float dayRange;
    public float dayRange2;
    public float eveningRange;
    public float nightRange;

    public float dayBase;
    public float day2Base;
    public float eveningBase;
    public float nightBase;
}

[Serializable]
public class CycleComponent : MonoBehaviour, ITimeStateMachine
{
    private SaveLoadCycleData slCycle = new SaveLoadCycleData();
    public RaceData RaceData;
    public CycleTimeReader TimeReader;
    public PepperBank PepperBank;
    
    [SerializeField] private DisasterAnimationsMediator _disasterAnimationsMediator;

    public void IncreaseDay(float range)
    {
        CycleData.cycleTime += range;
        TimeReader.ChangeDayTime(CycleData.cycleTime);
    } 
    
    public PepperStorage PepperStorage;
    public Shelter Shelter;
    public CycleData CycleData;
    
    private Dictionary<Type, ITimeState> _timeStates;
    public ITimeState _currentTimeState;

    [SerializeField] private GameObject Canvas;
    public void SetCanvas(GameObject _canvas) => Canvas = _canvas;
    [SerializeField] private GameObject _operatorPrefab, _operator;
    private DayOperator day;
    private EveningOperator evening;
    private NightOperator night;
    
    public void SetDayRange(float range) => CycleData.dayRange = range;
    public void SetDayRange2(float range) => CycleData.dayRange2 = range;
    public void SetEveningRange(int range) => CycleData.eveningRange = range;
    public void SetNightRange(float range) => CycleData.nightRange = range;
    
    public void ResetTime()
    {
        CycleData.dayRange = CycleData.dayBase;
        CycleData.dayRange2 = CycleData.day2Base;
        CycleData.eveningRange = CycleData.eveningBase;
        CycleData.nightRange = CycleData.nightBase;
    }

    public void NewDayBase(float range)
    {
        CycleData.dayBase += range;
        CycleData.dayRange += range;
    }
    public void NewDay2Base(float range)
    {
        CycleData.day2Base += range;
        CycleData.dayRange2 += range;
    }
    public void NewEveningBase(float range)
    {
        CycleData.eveningBase += range;
        CycleData.eveningRange += range;
    }
    public void NewNightBase(float range)
    {
        CycleData.nightBase += range;
        CycleData.nightRange += range;
    }

    public void MinusCostFactor(float range)
    {
        CycleData.costFactor -= range;
        PepperStorage.GetFactor(CycleData.costFactor);
    }
    public void PlusHeal(float heal) => CycleData.heal += heal;
    public int DayNum() => CycleData.cycleNum;
    public void PlusDay()
    {
        CycleData.cycleNum++;
        Shelter.HealHP(CycleData.heal);
        PepperStorage.AddPepper(RaceData.PassivePepper);
    }

    private void Awake()
    {
        PepperStorage = transform.GetComponent<PepperStorage>();
        Shelter = transform.GetComponent<Shelter>();
    }

    private void Start()
    {
        _currentTimeState = new NightState(this, TimeReader);
        _timeStates = new Dictionary<Type, ITimeState>()
        {
            { typeof(DayState), new DayState(this, TimeReader) },
            { typeof(EveningState), new EveningState(this, TimeReader) },
            { typeof(NightState), new NightState(this, TimeReader) },
        };

        if (RaceData.IsRace == 0)
        {
            RaceData.IsRace = 1;
            CycleData.cycleNum = 0;
            CycleData.cycleTime = 10f;
            CycleData.maxHP = RaceData.MaxHp;
            CycleData.lastHP = RaceData.MaxHp;
            CycleData.heal = 0;
            CycleData.lastPepper = 0;
            CycleData.costFactor = 1;
            CycleData.allPepper = 0;

            CycleData._BaseDictionaries = new Dictionary<int, BaseUpgrader>()
            {
                { 0, new QualityMaterials(this, PepperStorage, Shelter, 40, 0, 100) },
                { 1, new RepairRobots(this, PepperStorage, Shelter, 50, 0, 9) },
                { 2, new Boer(this, PepperStorage, Shelter, 30, 0, 100) },
                { 3, new ArtificialSun(this, PepperStorage, Shelter, 60, 0, 100) },
                { 4, new RightPeople(this, PepperStorage, Shelter, 50, 0, 14) },
                { 5, new MajorRepairs(this, PepperStorage, Shelter, 60, 0, 100) },
            };
            CycleData._ElDictionaries = new Dictionary<int, ElementUpgrader>()
            {
                { 0, new MeteorDefender(this, PepperStorage, Shelter, 70, 0) },
                { 1, new EarthDefender(this, PepperStorage, Shelter, 70, 0) },
                { 2, new EnergyDefender(this, PepperStorage, Shelter, 70, 0) },
                { 3, new CyberDefender(this, PepperStorage, Shelter, 70, 0) },
                { 4, new FogDefender(this, PepperStorage, Shelter, 70, 0) }, 
            };

            CycleData.lastDisaster = 10;
            CycleData.dayRange = 1;
            CycleData.dayRange2 = 1;
            CycleData.eveningRange = 2 + RaceData.AddTwists;
            CycleData.nightRange = 1;

            CycleData.dayBase = 1;
            CycleData.day2Base = 1;
            CycleData.eveningBase = 2 + RaceData.AddTwists;
            CycleData.nightBase = 1;
        }
        else
        {
            slCycle.LoadCycle();
            RaceData.IsRace = 1;
            
            CycleData.cycleNum = slCycle.cycleNum;
            CycleData.cycleTime = slCycle.cycleTime;
            CycleData.maxHP = slCycle.maxHP;
            CycleData.lastHP = slCycle.lastHP;
            CycleData.heal = slCycle.heal;
            CycleData.lastPepper = slCycle.lastPepper;
            CycleData.costFactor = slCycle.costFactor;
            CycleData.allPepper = slCycle.allPepper;

            CycleData._BaseDictionaries = new Dictionary<int, BaseUpgrader>()
            {
                { 0, new QualityMaterials(this, PepperStorage, Shelter, 40, slCycle.qualityMaterials, 100) },
                { 1, new RepairRobots(this, PepperStorage, Shelter, 50, slCycle.repairRobots, 9) },
                { 2, new Boer(this, PepperStorage, Shelter, 30, slCycle.boer, 100) },
                { 3, new ArtificialSun(this, PepperStorage, Shelter, 60, slCycle.artificialSun, 100) },
                { 4, new RightPeople(this, PepperStorage, Shelter, 50, slCycle.rightPeople, 14) },
                { 5, new MajorRepairs(this, PepperStorage, Shelter, 60, slCycle.majorRepairs, 100) },
            };
            CycleData._ElDictionaries = new Dictionary<int, ElementUpgrader>()
            {
                { 0, new MeteorDefender(this, PepperStorage, Shelter, 70, slCycle.meteorDefender) },
                { 1, new EarthDefender(this, PepperStorage, Shelter, 70, slCycle.earthDefender) },
                { 2, new EnergyDefender(this, PepperStorage, Shelter, 70, slCycle.energyDefender) },
                { 3, new CyberDefender(this, PepperStorage, Shelter, 70, slCycle.cyberDefender) },
                { 4, new FogDefender(this, PepperStorage, Shelter, 70, slCycle.fogDefender) }, 
            };

            CycleData.lastDisaster = slCycle.lastDisaster;
            CycleData.dayRange = slCycle.dayRange;
            CycleData.dayRange2 = slCycle.dayRange2;
            CycleData.eveningRange = slCycle.eveningRange + RaceData.AddTwists;
            CycleData.nightRange = slCycle.nightRange;

            CycleData.dayBase = slCycle.dayBase;
            CycleData.day2Base = slCycle.day2Base;
            CycleData.eveningBase = slCycle.eveningBase + RaceData.AddTwists;
            CycleData.nightBase = slCycle.nightBase;
        }
        slCycle.SaveCycle(CycleData, transform.parent.GetComponent<RaceController>());
        
        Shelter.SetShelter(CycleData.maxHP, CycleData.lastHP);
        PepperStorage.SetupPepper(CycleData.lastPepper);
        PepperBank.NewStorage(PepperStorage, CycleData.lastPepper);
        TimeReader.ChangeDayTime(CycleData.cycleTime);
    }

    public void Final()
    {  
        Destroy(gameObject);
    }
    private void Update()
    {
        _currentTimeState?.Update();    
        if(Input.GetKeyDown(KeyCode.E)) Debug.Log(_currentTimeState);
    }

    public void OpenTimeOperator<T>()
        where T : ITimeState
    {
        var type = typeof(T);
        switch (type.Name)
        {
            case "DayState": 
                slCycle.SaveCycle(CycleData, transform.parent.GetComponent<RaceController>());
                if(_operator != null) Destroy(_operator);
                _operator = Instantiate(_operatorPrefab, transform);
                
                day = _operator.GetComponent<DayOperator>();
                evening = _operator.GetComponent<EveningOperator>();
                night = _operator.GetComponent<NightOperator>();
                night.SetDisasterAnimationMediator(_disasterAnimationsMediator);
                
                night.enabled = false; 
                day.enabled = true; 
                day.SetOperator(CycleData.dayRange, CycleData.dayRange2);
                PlusDay();
                break;
            case "EveningState":
                day.enabled = false;
                evening.enabled = true;
                evening.SetOperator(CycleData.eveningRange);
                evening.SetCanvas(Canvas);
                break;
            case "NightState":
                evening.DestroyCards();
                evening.enabled = false;
                night.enabled = true;
                night.SetOperator(CycleData.nightRange);
                break;
        }
    }
    public void SwitchState<T>()
        where T : ITimeState
    {
        var type = typeof(T);
        _currentTimeState?.Exit();
        _timeStates.TryGetValue(type, out var state);
        _currentTimeState = state;
        
        state?.Open();
    }

    public void SetAnimationMediator(DisasterAnimationsMediator disasterAnimationsMediator)
    {
        _disasterAnimationsMediator = disasterAnimationsMediator;
    }
}