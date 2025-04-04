[System.Serializable]
public class DayState : ITimeState
{
    private readonly ITimeStateMachine _stateMachine;
    private readonly CycleTimeReader _timeReader;
        
    public DayState(ITimeStateMachine stateMachine, CycleTimeReader timeReader)
    {
        _stateMachine = stateMachine;
        _timeReader = timeReader;
    }
        
    public void Open()
    {
        
    }
    public void Update()
    {
        if(_timeReader.IsEvening()) _stateMachine.SwitchState<EveningState>();
    }

    public void Exit()
    {
        
    }
}
