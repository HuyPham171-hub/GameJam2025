using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerMovementSpeed = 10f; // Tốc độ di chuyển
    public float PlayerRotateSpeed = 10f;
    public Transform firePoint;
    public float fireCooldown = 0.5f;

    Rigidbody rb;
    private float lastFireTime = 0f;

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
        if (Input.GetMouseButton(0) && Time.time >= lastFireTime + fireCooldown)
        {
            FireBullet();
            lastFireTime = Time.time; // Cập nhật thời gian lần bắn cuối
        }
    }

    void Move()
    {
        // Lấy Input
        float horizontal = Input.GetAxis("Horizontal"); // A/D hoặc Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S hoặc Up/Down

        // Tạo Vector di chuyển
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Di chuyển Player
        if (movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * PlayerMovementSpeed * Time.deltaTime);
        }

        // Xoay Player theo hướng chuột
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Lấy vị trí chuột trên màn hình
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            // Tính toán hướng quay
            Vector3 lookDirection = hitInfo.point - transform.position;
            lookDirection.y = 0f; // Không thay đổi góc quay theo trục Y
            if (lookDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, PlayerRotateSpeed * Time.deltaTime);
            }
        }
    }

    void FireBullet()
    {
        // Lấy một viên đạn từ BulletPool
        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            // Đặt vị trí và hướng bắn
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Kích hoạt đạn bắn ra
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = firePoint.forward * 20f; // Tốc độ bắn
            }

            // Trả đạn về pool sau một khoảng thời gian
            StartCoroutine(ReturnBulletToPool(bullet, 2f)); // Trả đạn sau 2 giây
        }
        else
        {
            Debug.LogWarning("Không có đạn trong BulletPool!");
        }
    }

    System.Collections.IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ một khoảng thời gian
        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Reset vận tốc
            BulletPool.Instance.ReturnBullet(bullet); // Trả đạn về pool
        }
    }
}
