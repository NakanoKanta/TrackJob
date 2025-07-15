using System.Collections;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int _damage = 5;            // �_���[�W�ʂ͏����߂ɒ���
    [SerializeField] private float _invalidTime = 0.2f;  // �U�����薳�����ԁi�A���q�b�g�h�~�j

    private bool hasHit = false;
    private GameObject owner; // �U����

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

        // �������g(owner)�ɂ̓q�b�g�����Ȃ�
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
