using UnityEngine;
using UnityEngine.AI;

public class NPC_Enum
{
    public enum NPCState
    {
        Idle,
        Patrol,
        Investigate,
        Detect,
        Chase,
        Attack,
        Flank,
        Retreat,
        CallforBackUp,
        Hunt,
        Regroup,
    }
}