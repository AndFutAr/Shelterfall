[System.Serializable]
public class NightState : ITimeState
{
    private readonly ITimeStateMachine _stateMachine;
    private readonly CycleTimeReader _timeReader;
    
    public NightState(ITimeStateMachine stateMachine, CycleTimeReader timeReader)
    {
        _stateMachine = stateMachine;
        _timeReader = timeReader;
    }
    public void Open()
    {
        
    }
    public void Update()
    {
        if(_timeReader.IsDay()) _stateMachine.SwitchState<DayState>();
    }

    public void Exit()
    {
        
    }
}
