using UnityEngine;

// アニメーション情報を保持するコンポーネント
public class AnimationViewer : MonoBehaviour
{
    [Header("アニメーション設定")]
    public AnimationClip[] animationClips;
    public float playSpeed = 1.0f;
    public bool loop = true;

    [Header("プレビュー設定")]
    public bool showPreview = true;
    public float previewTime = 0f;

    private Animation animationComponent;

    void Start()
    {
        animationComponent = GetComponent<Animation>();
        if (animationComponent == null)
        {
            animationComponent = gameObject.AddComponent<Animation>();
        }
    }

    public void PlayAnimation(int index)
    {
        if (animationClips != null && index < animationClips.Length && animationClips[index] != null)
        {
            if (animationComponent == null)
                animationComponent = GetComponent<Animation>();

            animationComponent.clip = animationClips[index];
            animationComponent.Play();
        }
    }

    public void StopAnimation()
    {
        if (animationComponent != null)
        {
            animationComponent.Stop();
        }
    }
}
