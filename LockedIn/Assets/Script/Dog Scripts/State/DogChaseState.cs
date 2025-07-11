using UnityEngine;

public class DogChaseState : DogBaseState
{
    public DogChaseState(Dog dog) : base(dog)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Execute()
    {
        Debug.Log("Execute");
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
