using UnityEngine;

public class Lie_Down_SubState : Dog_SubState
{
    private float timer;
    private float duration;

    private bool isDestinationSet;
    private bool isDestinationReached;
    public Lie_Down_SubState(Dog dog) : base(dog)
    {
        float noise = Mathf.PerlinNoise(Time.time * 0.3f, dog.GetInstanceID() * 0.3f);
        duration = Mathf.Lerp(2f, 9f, noise);
    }

    public override void Enter()
    {
        timer = 0;

        isDestinationSet = false;
        isDestinationReached = false;

        dog.agent.isStopped = false;
    }
    public override void Execute()
    {
        timer += Time.deltaTime;
        if (!isDestinationSet && dog.lieDownSpots.Length > 0)
        {
            agent.SetDestination(dog.lieDownSpots[Random.Range(0, dog.lieDownSpots.Length)].transform.position);
            isDestinationSet = true;
        }

        if (isDestinationSet && !agent.pathPending && agent.remainingDistance < 0.2f && !isDestinationReached)
        {
            isDestinationReached = true;
            agent.isStopped = true;
            Debug.Log("Dog Lying Dow");
            //TODO : play lie down animation
        }

        if (isDestinationReached && timer >= duration)
        {
            Debug.Log("Dog has finished lying down.");
            //TODO : play stand up animation
            dog.StateMachine.ChangeState(new Dog_Idle_State(dog));
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

        isDestinationSet = false;
        isDestinationReached = false;

        dog.agent.isStopped = false;
        agent.ResetPath();

        Debug.Log("Dog has exited Lie Down SubState");
    }
}
