using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float PlayerMovementSpeed = 10f; // Tốc độ di chuyển
    CharacterController characterController;
    // Khởi tạo
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có 1 instance
            return;
        }

        DontDestroyOnLoad(gameObject); // Đảm bảo không bị phá hủy khi chuyển scene
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
        Vector3 dir = transform.forward * vertical + transform.right*horizontal;
        characterController.Move(dir*PlayerMovementSpeed*Time.deltaTime);
    }
    public void TakeDame()
    {
        Destroy(gameObject);
    }
}
