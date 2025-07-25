using UnityEngine;

public class Drink_Water_SubState : Dog_SubState
{
    private float timer;
    private float duration;

    private bool isDestinationSet;
    private bool isDestinationReached;
    public Drink_Water_SubState(Dog dog) : base(dog)
    {
    }

    public override void Enter()
    {
        timer = 0f;
        duration = 5f;

        isDestinationSet = false;
        isDestinationReached = false;
    }
    public override void Execute()
    {
        timer += Time.deltaTime;
        if (!isDestinationSet && dog.waterSpots.Length > 0)
        {
            agent.SetDestination(dog.waterSpots[Random.Range(0, dog.waterSpots.Length)].transform.position);
            isDestinationSet = true;
        }

        if(isDestinationSet && !agent.pathPending && agent.remainingDistance < 0.2f && !isDestinationReached)
        {
            isDestinationReached = true;
            agent.isStopped = true;
            Debug.Log("Dog Drinking Water");
            //TODO : play drinking animation
        }

        if (duration >= timer)
        {
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
        timer = 0f;
        duration = 0f;
        isDestinationSet = false;
        isDestinationReached = false;
    }
}
