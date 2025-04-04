using System;
using System.Collections;
using System.Collections.Generic;using UnityEngine;

[Serializable]
public class CycleComponent : MonoBehaviour, ITimeStateMachine
{
    public RaceStats _raceStats;
    public CycleTimeReader TimeReader;
    
    private Dictionary<Type, ITimeState> _timeStates;
    public ITimeState _currentTimeState;
    public int DayNum() => _raceStats.CycleNum;
    public void PlusDay() => _raceStats.CycleNum++;
    
    private void Start()
    {
        _currentTimeState = new NightState(this, TimeReader);
        _timeStates = new Dictionary<Type, ITimeState>()
        {
            { typeof(DayState), new DayState(this, TimeReader) },
            { typeof(EveningState), new EveningState(this, TimeReader) },
            { typeof(NightState), new NightState(this, TimeReader) },
        } ;
    }

    private void Update()
    {
        _currentTimeState?.Update();    
    }
    public void SwitchState<T>()
        where T : ITimeState
    {
        var type = typeof(T);
        _currentTimeState?.Exit();
        _timeStates.TryGetValue(type, out var state);
        _currentTimeState = state;
        
        state?.Open();
        Debug.Log(_currentTimeState.GetType().Name);
        if(TimeReader.IsDay()) PlusDay();
    }
}