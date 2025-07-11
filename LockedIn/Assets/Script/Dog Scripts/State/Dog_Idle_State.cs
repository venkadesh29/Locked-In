using UnityEngine;

public class Dog_Idle_State : DogBaseState
{
    private float timer;
    private float duration;
    private int randomIndex;
    private bool isDestinationReached;

    private float detectionRange = 10f;
    public Dog_Idle_State(Dog dog) : base(dog)
    {
        duration = Random.Range(2f, 5f);
    }

    public override void Enter()
    {
        timer = 0f;
        randomIndex = -1;
        isDestinationReached = false;
        Debug.Log("Enter");
    }

    public override void Execute()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Debug.Log("Dog has finished idling.");
            dog.StateMachine.ChangeState(new DogPatrolState(dog));
            return;
        }
        else
        {
            if (randomIndex == -1 && !isDestinationReached)
            {
                randomIndex = Random.Range(0, dog.patrolPoints.Length);
                agent.SetDestination(dog.patrolPoints[randomIndex].position);
                isDestinationReached = true;
            }

            if (isDestinationReached && !agent.pathPending && agent.remainingDistance < 0.2f)
            {
                Debug.Log("Dog has reached the destination.");
                agent.isStopped = true;
                agent.ResetPath();
                isDestinationReached = false;
            }
        }

        if (Vector3.Distance(dog.transform.position, dog.player.position) < detectionRange)
        {
            Debug.Log("Player detected within idle range.");
            dog.StateMachine.ChangeState(new DogChaseState(dog));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit");
        timer = 0f;
        randomIndex = -1;
        isDestinationReached = false;
    }
}