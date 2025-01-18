using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerMovementSpeed = 10f; // Tốc độ di chuyển
    public float PlayerRotateSpeed = 10f;
    Rigidbody rb;

    // Khởi tạo
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Gọi mỗi frame
    void Update()
    {

    }
    void FixedUpdate()
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
        if(movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * PlayerMovementSpeed * Time.deltaTime);
            Quaternion PlayerRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation,PlayerRotation,PlayerRotateSpeed*Time.fixedDeltaTime);
        }
    }
}
