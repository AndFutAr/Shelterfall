public interface ITimeStateMachine 
{
    public void SwitchState<T>()
        where T : ITimeState;
}
