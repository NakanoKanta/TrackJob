using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] int _damage = 0;
    private bool hasHit = false;
    public float _invalidTime = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return; // ���łɃq�b�g���Ă��疳��
        PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(_damage);
            hasHit = true;
            StartCoroutine(DisableHitboxTemporarily());

        }
    }
    private System.Collections.IEnumerator DisableHitboxTemporarily()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(_invalidTime);
        gameObject.SetActive(true);
        hasHit = false;
    }
}
