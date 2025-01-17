using UnityEngine;
using UnityEngine.AI;
public class Enemies : MonoBehaviour
{ public Transform player;          // Player cần theo dõi
    public float detectionRange = 10f; // Phạm vi phát hiện Player
    public float stopDistance = 2f;   // Khoảng cách dừng khi gần Player

    private NavMeshAgent agent;       // NavMeshAgent của Enemy

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        // Kiểm tra khoảng cách tới Player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Di chuyển về phía Player
            agent.SetDestination(player.position);

            // Quay mặt theo hướng di chuyển
            if (agent.velocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            // Dừng di chuyển nếu Player ngoài vùng phát hiện
            agent.ResetPath();
        }
    }
}