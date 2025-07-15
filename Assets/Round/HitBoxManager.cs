using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [Header("対象の FightingSystem（任意）")]
    public FightingSystem fightingSystem;

    [Header("コマンドに対応したオブジェクト（子オブジェクトで管理）")]
    public GameObject sinkuHadokenObject;
    public GameObject hadokenObject;
    public GameObject shoryukenObject;

    private Animator animator;

    private bool hasActivatedSinku = false;
    private bool hasActivatedHadoken = false;
    private bool hasActivatedShoryuken = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (sinkuHadokenObject != null) sinkuHadokenObject.SetActive(false);
        if (hadokenObject != null) hadokenObject.SetActive(false);
        if (shoryukenObject != null) shoryukenObject.SetActive(false);
    }

    void Update()
    {
        if (animator == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // SinkuHadoken
        if (stateInfo.IsName("Counter") && !hasActivatedSinku)
        {
            ActivateAttackObject(sinkuHadokenObject);
            hasActivatedSinku = true;
        }
        else if (!stateInfo.IsName("Counter"))
        {
            DeactivateAttackObject(sinkuHadokenObject);
            hasActivatedSinku = false;
        }

        // Hadoken
        if (stateInfo.IsName("Hadoken") && !hasActivatedHadoken)
        {
            ActivateAttackObject(hadokenObject);
            hasActivatedHadoken = true;
        }
        else if (!stateInfo.IsName("Hadoken"))
        {
            DeactivateAttackObject(hadokenObject);
            hasActivatedHadoken = false;
        }

        // Shoryuken
        if (stateInfo.IsName("Shoryuken") && !hasActivatedShoryuken)
        {
            ActivateAttackObject(shoryukenObject);
            hasActivatedShoryuken = true;
        }
        else if (!stateInfo.IsName("Shoryuken"))
        {
            DeactivateAttackObject(shoryukenObject);
            hasActivatedShoryuken = false;
        }
    }

    private void ActivateAttackObject(GameObject attackObject)
    {
        if (attackObject == null) return;

        attackObject.SetActive(true);

        AttackHitbox hitbox = attackObject.GetComponent<AttackHitbox>();
        if (hitbox != null)
        {
            hitbox.SetOwner(gameObject);
            hitbox.ResetHit();  // 攻撃判定リセット
        }
    }

    private void DeactivateAttackObject(GameObject attackObject)
    {
        if (attackObject == null) return;

        AttackHitbox hitbox = attackObject.GetComponent<AttackHitbox>();
        if (hitbox != null)
        {
            hitbox.ResetHit();  // 攻撃判定リセット
        }

        attackObject.SetActive(false);
    }
}
