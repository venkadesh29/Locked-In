using UnityEngine;

public class DogChaseState : DogBaseState
{
    public DogChaseState(Dog dog) : base(dog)
    {

    }

    public override void Enter()
    {
        agent.isStopped = false;
        Debug.Log("Enter");
    }

    public override void Execute()
    {
        Debug.Log("Execute");
        agent.SetDestination(dog.player.transform.position);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
