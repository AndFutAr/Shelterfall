using System;
using Unity.VisualScripting;
using UnityEngine;

public struct RaceStats
{
    public int RaceNum;
    public int CycleNum;
}
public class RaceController : MonoBehaviour
{
    public RaceStats raceStats;
    public CycleTimeReader timeReader;
    
    [SerializeField] private GameObject _cycleControllerPrefab, _cycleController;
    private CycleComponent _cycle;
    public int day;

    void Awake()
    {
        raceStats.RaceNum = 1;
        raceStats.CycleNum = 0;
    }
    private void Update()
    {
        if(_cycle != null) day = _cycle.DayNum();
    }

    public void LoseRace()
    {
        if(timeReader.IsRacing()) timeReader.InverseRace();
        Destroy(_cycleController);
        _cycle = null;
    }

    public void StartRace()
    {
        if (!timeReader.IsRacing())
        {
            timeReader.InverseRace();
            if(_cycleController != null) Destroy(_cycleController);
            _cycleController = Instantiate(_cycleControllerPrefab, transform);
            _cycle = _cycleController.GetComponent<CycleComponent>();
            _cycle.TimeReader = timeReader;
            _cycle._raceStats = raceStats;
        }
    }

    public void GoToNight() => timeReader.GoNight();
    public void GoToDay() => timeReader.FinalNight();
}