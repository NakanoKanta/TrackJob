using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [Header("パーティクルシステム")]
    public ParticleSystem particles;

    [Header("アニメーター")]
    public Animator animator;

    [Header("設定")]
    public string animationStateName = "counteranimation"; // アニメーション状態名を直接設定

    [Header("プレイヤー参照")]
    public PlayerController playerController; // PlayerControllerの参照

    private bool hasPlayedParticle = false;
    private Vector3 originalParticlePosition; // パーティクルの元のスケール

    void Start()
    {
        // アニメーターの自動取得
        if (animator == null)
            animator = GetComponent<Animator>();

        // パーティクルシステムの自動取得（子オブジェクトも検索）
        if (particles == null)
            particles = GetComponentInChildren<ParticleSystem>();

        // PlayerControllerの自動取得
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        // パーティクルの元のスケールを保存
        if (particles != null)
        {
            originalParticlePosition = particles.transform.position;
        }
    }

    void Update()
    {
        if (animator == null || particles == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 指定したアニメーションが開始され、まだパーティクルが再生されていない場合
        if (stateInfo.IsName(animationStateName) && !hasPlayedParticle)
        {
            hasPlayedParticle = true;
            PlayParticle();
        }
        // アニメーションが終了した場合、フラグをリセット
        else if (!stateInfo.IsName(animationStateName))
        {
            if (hasPlayedParticle)
            {
                hasPlayedParticle = false;
            }
        }
    }

    void PlayParticle()
    {
        // PlayerControllerが存在する場合のみ方向調整
        if (playerController != null)
        {
            SetParticleDirection();
        }

        particles.gameObject.SetActive(true);
        particles.Play();
    }

    void SetParticleDirection()
    {
        // PlayerControllerから向きを取得
        bool isFacingLeft = GetFacingLeft();

        // パーティクルのX軸スケールを反転
        Vector3 newScale = originalParticlePosition;
        if (isFacingLeft)
        {
            newScale.x = -Mathf.Abs(originalParticlePosition.x); // 左向き時は負の値
        }
        else
        {
            newScale.x = Mathf.Abs(originalParticlePosition.x);  // 右向き時は正の値
        }
        particles.transform.position = newScale;
    }
    // PlayerControllerから向きを取得するヘルパーメソッド
    private bool GetFacingLeft()
    {
        if (playerController != null)
        {
            return playerController.facingLeft;
        }
        // PlayerControllerが無い場合はtransform.localScaleで判定
        return transform.position.x < 0;
    }

    // キャラクターの向きを手動で設定する場合のヘルパーメソッド
    public void SetCharacterDirection(bool facingLeft)
    {
        if (playerController != null)
        {
            playerController.SetFacing(!facingLeft);
        }
        else
        {
            if (facingLeft)
            {
                transform.position = new Vector3(-Mathf.Abs(transform.position.x), transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Abs(transform.position.x), transform.position.y, transform.position.z);
            }
        }
    }

    // 追加：PlayerControllerの向き情報を直接取得するメソッド
    public bool GetPlayerFacingRight()
    {
        if (playerController != null)
        {
            return playerController.isFacingRight;
        }
        return transform.position.x > 0;
    }

    // デバッグ用：現在のアニメーション状態を表示
    [ContextMenu("Show Current Animation State")]
    public void ShowCurrentAnimationState()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"Current Animation: {stateInfo.IsName(animationStateName)}");
            Debug.Log($"Facing Left: {GetFacingLeft()}");
            Debug.Log($"Player Facing Right: {GetPlayerFacingRight()}");
            if (particles != null)
            {
                Debug.Log($"Particle Scale: {particles.transform.localScale}");
            }
        }
    }
}