using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của Player
    public float rotationSpeed = 5f; // Tốc độ xoay của boss

    void Update()
    {
        if (player != null)
        {
            // Tính hướng từ boss đến player
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Giữ y cố định để boss không xoay lên/xuống

            // Kiểm tra xem player có ở ngoài phạm vi gần boss không
            if (direction.magnitude > 0.1f)
            {
                // Tạo rotation mới để nhìn về hướng player
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Interpolate giữa rotation hiện tại và rotation mục tiêu
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
