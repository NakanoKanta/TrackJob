using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] int _damage = 0;
    private bool hasHit = false;
    public float _invalidTime = 0.1f;
    public void ResetHit()
    {
        hasHit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasHit) return; // ���łɃq�b�g���Ă��疳��
            Debug.Log("Hit!");
            PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {

                enemyHealth.TakeDamage(_damage);
                //Debug.Log("���݂�HP" + enemyHealth._playerHP);
                hasHit = true;
                StartCoroutine(DisableHitboxTemporarily());
                Debug.Log(hasHit);
            }
        }
    }
    private System.Collections.IEnumerator DisableHitboxTemporarily()
    {
        Debug.Log("�R���[�`���J�n");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(_invalidTime);
        GetComponent<Collider2D>().enabled = true;
        Debug.Log("�R���[�`���I���");
    }
}
