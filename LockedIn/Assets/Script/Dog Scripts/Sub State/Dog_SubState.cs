using UnityEngine;

public abstract class Dog_SubState : MonoBehaviour
{
    protected Dog dog;
    //protected Animator animator;

    public Dog_SubState(Dog dog)
    {
        this.dog = dog;
        //this.animator = dog.GetComponent<Animator>();
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
