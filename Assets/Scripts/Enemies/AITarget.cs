using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public Transform Target; // Vị trí mục tiêu (Player)
    public float ChaseDistance = 10f; // Khoảng cách để bắt đầu đuổi theo
    public float AttackDistance = 2f; // Khoảng cách để tấn công

    private NavMeshAgent m_Agent; // Agent dùng để di chuyển
    private float m_DistanceToTarget; // Khoảng cách giữa enemy và Player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();

        // Tìm đối tượng Player thông qua Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Target = player.transform;
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng với tag 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;

        // Tính khoảng cách tới Player
        m_DistanceToTarget = Vector3.Distance(m_Agent.transform.position, Target.position);

        if (m_DistanceToTarget <= ChaseDistance)
        {
            if (m_DistanceToTarget <= AttackDistance)
            {
                // Dừng lại để tấn công
                m_Agent.isStopped = true;
                Debug.Log("Attack!");
            }
            else
            {
                // Đuổi theo Player
                m_Agent.isStopped = false;
                m_Agent.destination = Target.position;
            }
        }
        else
        {
            // Enemy dừng di chuyển nếu Player ở ngoài vùng ChaseDistance
            m_Agent.isStopped = true;
        }
    }

    // Hiển thị vùng ChaseDistance và AttackDistance bằng Gizmos
    private void OnDrawGizmos()
    {
        // Vẽ vùng ChaseDistance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ChaseDistance);

        // Vẽ vùng AttackDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
