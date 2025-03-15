using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;
    [SerializeField] private Bullet bulletPrefab;
    private ObjectPool<Bullet> bulletPool;
    public float ShootRate = 0.1f;
    [SerializeField] private int poolSize = 20;
    private string path = "Prefabs/Projectile";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        bulletPrefab = Resources.Load<Bullet>(path);
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, poolSize);
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
