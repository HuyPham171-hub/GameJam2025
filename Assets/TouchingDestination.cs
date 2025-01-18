using UnityEngine;

public class TouchingDestination : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the "Player" tag
        if (other.CompareTag("Destination"))
        {
            Debug.LogWarning("Touch Destination");
            GameController.Instance.Winner();
        }
    }
}
