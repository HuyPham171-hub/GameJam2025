using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{

    public Transform Target;
    public float AttackDistance;

    private NavMeshAgent m_Agent;
    private float m_Distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);
        if (m_Distance < AttackDistance)
        {
            m_Agent.isStopped = true;
            Debug.Log("Attack!");
        }
        else
        {
            m_Agent.isStopped = false;
            m_Agent.destination = Target.position;
        }
    }
}
