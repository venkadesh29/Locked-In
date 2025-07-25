using UnityEngine;

public class Dog_Idle_State : DogBaseState
{
    private float timer;
    private float duration;
    private int randomIndex;
    private bool isDestinationReached;

    private float detectionRange = 10f;

    private Dog_SubState subState;
    public Dog_Idle_State(Dog dog) : base(dog)
    {
        float noise = Mathf.PerlinNoise(Time.time * 0.3f, dog.GetInstanceID() * 0.3f);
        duration = Mathf.Lerp(2f, 10f, noise);
    }

    public override void Enter()
    {
        timer = 0f;
        randomIndex = -1;
        isDestinationReached = false;

        ChooseSubstate();
        subState?.Enter();
    }
    
    public override void Execute()
    {
        timer += Time.deltaTime;

        subState?.Execute();

        if(!isDestinationReached && dog.patrolPoints.Length>0)
        {
            randomIndex = Random.Range(0, dog.patrolPoints.Length);
            dog.agent.SetDestination(dog.patrolPoints[randomIndex].position);
            isDestinationReached = true;
        }

        if(isDestinationReached && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            agent.isStopped = true;
            agent.ResetPath();
            isDestinationReached = false;
        }

        if(timer>duration)
        {
            subState?.Exit();
            dog.StateMachine.ChangeState(new DogPatrolState(dog));
            return;
        }

        if(Vector3.Distance(dog.transform.position, player.position) < detectionRange)
        {
            subState?.Exit();
            dog.StateMachine.ChangeState(new DogChaseState(dog));
        }
    }

    public override void Exit()
    {
        subState?.Exit();
        agent.isStopped = true;
        isDestinationReached = false;
        timer = 0f;
        randomIndex = -1;
    }
    
    public void ChooseSubstate()
    {
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0:
                subState = new Eat_SubState(dog);
                break;
            case 1:
                subState = new Drink_Water_SubState(dog);
                break;
            case 2:
                subState = new Lie_Down_SubState(dog);
                break;
        }
    }
}


/*public override void Enter()
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
    }*/