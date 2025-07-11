using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] int _damage = 0;
    private bool hasHit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return; // ���łɃq�b�g���Ă��疳��
        PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(_damage);
            hasHit = true;

        }
    }
}
