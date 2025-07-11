using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolIndex;

    [Header("NPC Settings")]
    public float detectionRange;
    public float attackRange;
    public float health;
    public float lowHealthThreshold;

    [Header("References")]
    public Transform player;
    public NavMeshAgent agent{ get; private set; }
    public DogStateMachine StateMachine { get;private set; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        StateMachine = new DogStateMachine();
        StateMachine.ChangeState(new DogPatrolState(this));

        detectionRange = 10f;
        attackRange = 2f;
        health = 100f;
        lowHealthThreshold = 30f;
    }

    private void Update()
    {
        StateMachine.Update();
    }
}