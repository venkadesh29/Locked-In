using UnityEngine;

public class DogStateMachine
{
    private DogState currentState;

    public void ChangeState(DogState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public DogState GetCurrentState() => currentState;

}