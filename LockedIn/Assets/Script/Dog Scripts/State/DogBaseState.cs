using UnityEngine;
using UnityEngine.AI;

public abstract class DogBaseState : DogState
{
    protected Dog dog;
    protected NavMeshAgent agent;
    protected Transform player;

    public DogBaseState(Dog dog)
    {
        this.dog = dog;
        this.agent = dog.GetComponent<NavMeshAgent>();
        if (player == null)
        {
            this.player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}