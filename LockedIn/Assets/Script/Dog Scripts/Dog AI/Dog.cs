using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Dog : MonoBehaviour
{
    public static Dog Instance { get; set; }

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolIndex;

    [Header("Lie Down Spots")]
    public Transform[] lieDownSpots;

    [Header("Water Spots")]
    public Transform[] waterSpots;

    [Header("Food Spots")]
    public Transform[] foodSpots;

    [Header("NPC Settings")]
    public float detectionRange;
    public float attackRange;
    public float health;
    public float lowHealthThreshold;

    [Header("References")]
    public Transform player;
    public NavMeshAgent agent { get; private set; }
    public DogStateMachine StateMachine { get; private set; }
    public string currentState;
    public float timer;

    private void Awake()
    {
        GetObjects();
    }
    private void Start()
    {
        Instance = this;
        agent = GetComponent<NavMeshAgent>();
        StateMachine = new DogStateMachine();
        StateMachine.ChangeState(new DogPatrolState(this));

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        detectionRange = 10f;
        attackRange = 2f;
        health = 100f;
        lowHealthThreshold = 30f;
    }

    private void Update()
    {
        StateMachine.Update();
        currentState = StateMachine.GetCurrentState()?.GetType().Name ?? "None";
    }

    private void GetObjects()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints").Select(go => go.transform).ToArray();

        if (lieDownSpots == null || lieDownSpots.Length == 0)
            lieDownSpots = GameObject.FindGameObjectsWithTag("LieDownSpots").Select(go => go.transform).ToArray();

        if (waterSpots == null || waterSpots.Length == 0)
            waterSpots = GameObject.FindGameObjectsWithTag("WaterSpots").Select(go => go.transform).ToArray();

        if (foodSpots == null || foodSpots.Length == 0)
            foodSpots = GameObject.FindGameObjectsWithTag("FoodSpots").Select(go => go.transform).ToArray();
    }
}