using UnityEngine;

public class Eat_SubState : Dog_SubState
{
    private float timer;
    private float duration;

    public Eat_SubState(Dog dog) : base(dog)
    {
        duration = Random.Range(2f, 5f);
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {

    }
}
