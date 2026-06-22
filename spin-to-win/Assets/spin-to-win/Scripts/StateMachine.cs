using System;

public class State
{
    public Action OnEnter;
    public Action OnExit;
    public Action Update;
}

public class StateMachine
{
    public State Current { get; private set; }

    public void Transition(State next)
    {
        Current?.OnExit?.Invoke();
        Current = next;
        Current?.OnEnter?.Invoke();
    }
}
