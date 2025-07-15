using System.Collections;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int _damage = 5;            // ダメージ量は小さめに調整
    [SerializeField] private float _invalidTime = 0.2f;  // 攻撃判定無効時間（連続ヒット防止）

    private bool hasHit = false;
    private GameObject owner; // 攻撃主

    public void SetOwner(GameObject ownerObject)
    {
        owner = ownerObject;
    }

    public void ResetHit()
    {
        hasHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (!other.CompareTag("Player")) return;

        // 自分自身(owner)にはヒットさせない
        if (owner != null && other.gameObject == owner) return;

        PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(_damage);
            hasHit = true;
            StartCoroutine(DisableHitboxTemporarily());
        }
    }

    private IEnumerator DisableHitboxTemporarily()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(_invalidTime);
        GetComponent<Collider2D>().enabled = true;
        hasHit = false;
    }
}
