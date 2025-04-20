public interface ITimeStateMachine 
{
    public void SwitchState<T>()
        where T : ITimeState;

    public void OpenTimeOperator<T>()
        where T : ITimeState;
}
