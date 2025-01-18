using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance; // Singleton instance
    public GameObject bulletPrefab; // Prefab của đạn
    public int poolSize = 20; // Số lượng đạn tối đa trong pool

    private Queue<GameObject> bulletPool = new Queue<GameObject>(); // Hàng đợi lưu các object đạn

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

    private void Start()
    {
        // Khởi tạo pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); // Ẩn đạn khi khởi tạo
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // Lấy đạn từ pool
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true); // Kích hoạt đạn
            return bullet;
        }
        else
        {
            Debug.LogWarning("Pool is empty! Consider increasing pool size.");
            return null;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        // Trả đạn về pool
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
