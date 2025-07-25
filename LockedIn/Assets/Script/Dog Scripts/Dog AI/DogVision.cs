using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DogVision : MonoBehaviour
{
    [SerializeField] private float detectionRange = 50f;
    [SerializeField] private int rays = 30;
    [SerializeField] private float fieldofView = 100f;
    [SerializeField] private Color gizmoColor = Color.red;

    [SerializeField] bool isPlayerDetected = false;


    private void Update()
    {
        isPlayerDetected = PlayerSpotted(Dog.Instance.player);
    }
    public bool PlayerSpotted(Transform player)
    {
        Vector3 origin = transform.position + new Vector3(0,2,0);
        Vector3 toPlayer = (player.position - origin).normalized;

        for (int i = 0; i < rays; i++)
        {
            float angleOffset = Mathf.Lerp(-fieldofView / 2, fieldofView / 2, i / (float)(rays - 1));
            /*fieldOfView / 2 = half left, half right(e.g., for 90°, you get - 45° to + 45°)
              Mathf.Lerp(start, end, t) picks a value between start and end, where t is 0 to 1.
              i / (rays - 1) makes t go from 0 to 1 across all rays.*/

            Vector3 dir = Quaternion.Euler(0, angleOffset, 0) * transform.forward;

            if (Physics.Raycast(origin, dir, out RaycastHit hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + Vector3.up;
        Gizmos.color = isPlayerDetected? Color.black : gizmoColor;

        for (int i = 0; i < rays; i++)
        {
            float angleOffset = Mathf.Lerp(-fieldofView / 2, fieldofView / 2, i / (float)(rays - 1));
            Vector3 dir = Quaternion.Euler(0, angleOffset, 0) * transform.forward;
            Gizmos.DrawRay(origin, dir * detectionRange);
        }
    }
}
