using UnityEngine;
using UnityEngine.AI;

public abstract class Dog_SubState
{
    protected Dog dog;
    protected NavMeshAgent agent;
    //protected Animator animator;

    public Dog_SubState(Dog dog)
    {
        this.dog = dog;
        this.agent = dog.agent;
        //this.animator = dog.GetComponent<Animator>();
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
