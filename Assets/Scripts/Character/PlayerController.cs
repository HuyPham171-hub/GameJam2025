using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển
    Rigidbody rb;

    // Khởi tạo
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Gọi mỗi frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // Lấy Input
        float horizontal = Input.GetAxis("Horizontal"); // A/D hoặc Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S hoặc Up/Down

        // Tạo Vector di chuyển
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Di chuyển Player
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
