using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [Header("パーティクルシステム")]
    public new ParticleSystem particleSystem;

    [Header("アニメーター")]
    public Animator animator;

    [Header("設定")]
    public string animationTriggerName = "StartAnimation";
    public string animationStateName = "YourAnimationState";

    void Start()
    {
        // パーティクルシステムが未設定の場合、自動取得
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();

        // アニメーターが未設定の場合、自動取得
        if (animator == null)
            animator = GetComponent<Animator>();
    }
    void Update()
    {
        ParticlePlay();
    }

    public void ParticlePlay()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 指定したアニメーションが開始されたかチェック
        if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime <= 0.1f)
        {
            // アニメーション開始時にパーティクルを再生
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}