using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float shootSpeed = 6f;
    private Action onReturn;
   
    public void Shoot(Vector2 direction, Action returnCallback)
    {
        onReturn = returnCallback;
        StartCoroutine(MoveBullet(direction));
    }

    private IEnumerator MoveBullet(Vector2 direction)
    {
        float lifeTime = 3f;
        float timer = 0f;

        while(timer < lifeTime)
        {
            transform.position += (Vector3)direction * shootSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        onReturn?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
    }
}
