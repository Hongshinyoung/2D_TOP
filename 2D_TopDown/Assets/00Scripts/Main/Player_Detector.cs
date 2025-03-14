using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detector : MonoBehaviour
{
    [SerializeField] private LayerMask monsterLayer;
    private Collider2D[] targets;
    [SerializeField] private Transform nearbyTarget;
    [SerializeField] private float radius = 5f;

    private void FixedUpdate()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, radius, monsterLayer);
        nearbyTarget = GetNearbyMonster();
        if(nearbyTarget != null)
        {
            Vector2 directoin = (nearbyTarget.position - transform.position).normalized;
            BulletManager.Instance.ShootBullet(transform.position, directoin);
        }
    }

    private Transform GetNearbyMonster()
    {
        Transform result = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                result = target.transform;
            }
        }

        return result;
    }
}
