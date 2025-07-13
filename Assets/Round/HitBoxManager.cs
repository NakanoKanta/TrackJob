using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [Header("対象の FightingSystem")]
    public FightingSystem fightingSystem;

    [Header("コマンドに対応したオブジェクト")]
    public GameObject sinkuHadokenObject;
    public GameObject hadokenObject;
    public GameObject shoryukenObject;

    public Animator animator;

    private bool hasActivatedSinku = false;
    private bool hasActivatedHadoken = false;
    private bool hasActivatedShoryuken = false;

    private AttackHitbox sinkuHitbox;
    private AttackHitbox hadokenHitbox;
    private AttackHitbox shoryukenHitbox;

    void Start()
    {
        // 初期化：オブジェクト無効化
        sinkuHadokenObject.SetActive(false);
        hadokenObject.SetActive(false);
        shoryukenObject.SetActive(false);

        // ヒットボックスの参照取得
        sinkuHitbox = sinkuHadokenObject.GetComponent<AttackHitbox>();
        hadokenHitbox = hadokenObject.GetComponent<AttackHitbox>();
        shoryukenHitbox = shoryukenObject.GetComponent<AttackHitbox>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (animator == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // SinkuHadoken（Counter アニメーション）
        if (stateInfo.IsName("Counter") && !hasActivatedSinku)
        {
            sinkuHadokenObject.SetActive(true);
            sinkuHitbox?.ResetHit();
            hasActivatedSinku = true;
        }
        else if (!stateInfo.IsName("Counter"))
        {
            sinkuHadokenObject.SetActive(false);
            hasActivatedSinku = false;
        }

        // Hadoken
        if (stateInfo.IsName("Hadoken") && !hasActivatedHadoken)
        {
            hadokenObject.SetActive(true);
            hadokenHitbox?.ResetHit();
            hasActivatedHadoken = true;
        }
        else if (!stateInfo.IsName("Hadoken"))
        {
            hadokenObject.SetActive(false);
            hasActivatedHadoken = false;
        }

        // Shoryuken
        if (stateInfo.IsName("Shoryuken") && !hasActivatedShoryuken)
        {
            shoryukenObject.SetActive(true);
            shoryukenHitbox?.ResetHit();
            hasActivatedShoryuken = true;
        }
        else if (!stateInfo.IsName("Shoryuken"))
        {
            shoryukenObject.SetActive(false);
            hasActivatedShoryuken = false;
        }
    }
}
