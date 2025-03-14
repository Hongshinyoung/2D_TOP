using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;
    [SerializeField] private Bullet bulletPrefab;
    private ObjectPool<Bullet> bulletPool;
    private string path = "Prefabs/Projectile";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        bulletPrefab = Resources.Load<Bullet>(path);
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, 20); // 불렛 20개 생성
    }


    public void ShootBullet(Vector2 position, Vector2 direction)
    {
        Bullet bullet = bulletPool.GetObject();
        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);
            bullet.Shoot(direction, () => bulletPool.ReturnObject(bullet));
        }
    }
}
