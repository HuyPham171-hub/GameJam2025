using UnityEngine;

public class ApplyHealth : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Touch Player");
            PlayerController.Instance.TakeDame();
        }
    }
}
