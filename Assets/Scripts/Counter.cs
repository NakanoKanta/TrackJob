using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [Header("�p�[�e�B�N���V�X�e��")]
    public new ParticleSystem particleSystem;

    [Header("�A�j���[�^�[")]
    public Animator animator;

    [Header("�ݒ�")]
    public string animationTriggerName = "StartAnimation";
    public string animationStateName = "YourAnimationState";

    void Start()
    {
        // �p�[�e�B�N���V�X�e�������ݒ�̏ꍇ�A�����擾
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();

        // �A�j���[�^�[�����ݒ�̏ꍇ�A�����擾
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

        // �w�肵���A�j���[�V�������J�n���ꂽ���`�F�b�N
        if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime <= 0.1f)
        {
            // �A�j���[�V�����J�n���Ƀp�[�e�B�N�����Đ�
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}