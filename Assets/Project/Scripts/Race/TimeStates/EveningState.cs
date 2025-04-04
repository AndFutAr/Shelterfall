[System.Serializable]
public class EveningState : ITimeState
{
    private readonly ITimeStateMachine _stateMachine;
    private readonly CycleTimeReader _timeReader;
    
    public EveningState(ITimeStateMachine stateMachine, CycleTimeReader timeReader)
    {
        _stateMachine = stateMachine;
        _timeReader = timeReader;
    }
    
    public void Open()
    {
        
    }
    public void Update()
    {
        if(_timeReader.IsNight()) _stateMachine.SwitchState<NightState>();
    }

    public void Exit()
    {
        
    }
}
