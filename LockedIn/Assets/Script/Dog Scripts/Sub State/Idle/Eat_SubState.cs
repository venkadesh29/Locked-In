using UnityEngine;

public class Eat_SubState : Dog_SubState
{
    private float timer;
    private float duration;

    private bool isDestinationSet;
    private bool isDestinationReached;

    public Eat_SubState(Dog dog) : base(dog)
    {
        duration = Random.Range(2f, 5f);
    }

    public override void Enter()
    {
        timer = 0f;
        duration = 10f;

        isDestinationSet = false;
        isDestinationReached = false;

        agent.isStopped = false;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;
        if (!isDestinationSet && dog.foodSpots.Length > 0)
        {
            agent.SetDestination(dog.foodSpots[Random.Range(0, dog.foodSpots.Length)].transform.position);
            isDestinationSet = true;
        }

        if(isDestinationSet && !agent.pathPending && agent.remainingDistance < 0.2f && !isDestinationReached)
        {
            isDestinationReached = true;
            agent.isStopped = true;
            Debug.Log("Dog Eating");
            //TODO : play eating animation
        }

        if(isDestinationReached && timer >= duration)
        {
            //TODO : play stand up animation
            dog.StateMachine.ChangeState(new DogPatrolState(dog));
        }

        /*if(call for backup heard)
        {
            Debug.Log("Dog has heard a call for backup.");
            dog.StateMachine.ChangeState(new DogChaseState(dog));
        }*/

        /*if (player sited)
        {
            dog.StateMachine.ChangeState(new DogChaseState(dog));
        }*/
    }

    public override void Exit()
    {
        agent.isStopped = false;  
        agent.ResetPath();

        timer = 0f;
        isDestinationSet = false;
        isDestinationReached = false;
        Debug.Log("EatSubState: Exit");
    }
}
