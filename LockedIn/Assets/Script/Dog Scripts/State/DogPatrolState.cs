using UnityEngine;

public class DogPatrolState : DogBaseState
{
    private float timer;
    private float duration;

    public DogPatrolState(Dog dog) : base(dog)
    {
        this.duration = Random.Range(5f,10f);
    }

    public override void Enter()
    {
        timer = 0f;
        Debug.Log("Dog has entered Patrol State");
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (dog.patrolPoints.Length == 0) return;

        agent.SetDestination(dog.patrolPoints[dog.currentPatrolIndex].position);

        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            dog.currentPatrolIndex = (dog.currentPatrolIndex + 1) % dog.patrolPoints.Length;
        }

        if (timer > duration)
        {
            Debug.Log("Dog has finished patrolling.");
            dog.StateMachine.ChangeState(new Dog_Idle_State(dog));
        }

        if(Vector3.Distance(dog.transform.position, player.position)< dog.detectionRange)
        {
            Debug.Log("Player detected within patrol range.");
            dog.StateMachine.ChangeState(new DogChaseState(dog));
        }
    }
    public override void Exit()
    {
        timer = 0f;
        Debug.Log("Dog has exited Patrol State");
    }
}
