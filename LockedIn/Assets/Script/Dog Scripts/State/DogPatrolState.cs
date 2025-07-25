using System.Collections.Generic;
using UnityEngine;

public class DogPatrolState : DogBaseState
{
    private float timer;
    private float duration;
    private HashSet<GameObject> visitedPatrolPoints;
    private Transform currentTarget;

    public DogPatrolState(Dog dog) : base(dog)
    {
        this.duration = Random.Range(50f, 120f);
    }

    public override void Enter()
    {
        visitedPatrolPoints = new HashSet<GameObject>();
        timer = 0f;

        PickaPointtoPatrol();
        Debug.Log("Dog has entered Patrol State");
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (dog.patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            PickaPointtoPatrol();
            Debug.Log("Dog is patrolling to point: " + currentTarget.position);
        }

        var hit = Physics.OverlapSphere(dog.transform.position, dog.detectionRange);

        foreach (Collider collider in hit)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Player detected within patrol range.");
                agent.isStopped = true;
                dog.StateMachine.ChangeState(new DogChaseState(dog));
            }

            else return;
        }
    }

    public override void Exit()
    {
        timer = 0f;
        agent.isStopped = false;
        visitedPatrolPoints.Clear();
        Debug.Log("Dog has exited Patrol State");
    }

    public void PickaPointtoPatrol()
    {
        if (dog.patrolPoints == null || dog.patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points available for the dog to patrol.");
            return;
        }

        if (visitedPatrolPoints.Count >= dog.patrolPoints.Length)
        {
            Debug.Log("All patrol points visited, switching to idle.");
            dog.StateMachine.ChangeState(new Dog_Idle_State(dog));
            return;
        }

        int index;
        Transform nextPoint;
        int safety = 0;

        do
        {
            index = Random.Range(0, dog.patrolPoints.Length);
            nextPoint = dog.patrolPoints[index];
            safety++;

            if (safety > 100)
            {
                Debug.LogWarning("Patrol point selection failed after too many attempts.");
                return;
            }

        } while (visitedPatrolPoints.Contains(nextPoint.gameObject));

        currentTarget = nextPoint;
        visitedPatrolPoints.Add(nextPoint.gameObject);
        agent.SetDestination(currentTarget.position);
    }
}
