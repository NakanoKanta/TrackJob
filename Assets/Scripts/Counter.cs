using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [Header("�p�[�e�B�N���V�X�e��")]
    public ParticleSystem particles;

    [Header("�A�j���[�^�[")]
    public Animator animator;

    [Header("�ݒ�")]
    public string animationStateName = "counteranimation"; // �A�j���[�V������Ԗ��𒼐ڐݒ�

    [Header("�v���C���[�Q��")]
    public PlayerController playerController; // PlayerController�̎Q��

    private bool hasPlayedParticle = false;
    private Vector3 originalParticlePosition; // �p�[�e�B�N���̌��̃X�P�[��

    void Start()
    {
        // �A�j���[�^�[�̎����擾
        if (animator == null)
            animator = GetComponent<Animator>();

        // �p�[�e�B�N���V�X�e���̎����擾�i�q�I�u�W�F�N�g�������j
        if (particles == null)
            particles = GetComponentInChildren<ParticleSystem>();

        // PlayerController�̎����擾
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        // �p�[�e�B�N���̌��̃X�P�[����ۑ�
        if (particles != null)
        {
            originalParticlePosition = particles.transform.position;
        }
    }

    void Update()
    {
        if (animator == null || particles == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �w�肵���A�j���[�V�������J�n����A�܂��p�[�e�B�N�����Đ�����Ă��Ȃ��ꍇ
        if (stateInfo.IsName(animationStateName) && !hasPlayedParticle)
        {
            hasPlayedParticle = true;
            PlayParticle();
        }
        // �A�j���[�V�������I�������ꍇ�A�t���O�����Z�b�g
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
        // PlayerController�����݂���ꍇ�̂ݕ�������
        if (playerController != null)
        {
            SetParticleDirection();
        }

        particles.gameObject.SetActive(true);
        particles.Play();
    }

    void SetParticleDirection()
    {
        // PlayerController����������擾
        bool isFacingLeft = GetFacingLeft();

        // �p�[�e�B�N����X���X�P�[���𔽓]
        Vector3 newScale = originalParticlePosition;
        if (isFacingLeft)
        {
            newScale.x = -Mathf.Abs(originalParticlePosition.x); // ���������͕��̒l
        }
        else
        {
            newScale.x = Mathf.Abs(originalParticlePosition.x);  // �E�������͐��̒l
        }
        particles.transform.position = newScale;
    }
    // PlayerController����������擾����w���p�[���\�b�h
    private bool GetFacingLeft()
    {
        if (playerController != null)
        {
            return playerController.facingLeft;
        }
        // PlayerController�������ꍇ��transform.localScale�Ŕ���
        return transform.position.x < 0;
    }

    // �L�����N�^�[�̌������蓮�Őݒ肷��ꍇ�̃w���p�[���\�b�h
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

    // �ǉ��FPlayerController�̌������𒼐ڎ擾���郁�\�b�h
    public bool GetPlayerFacingRight()
    {
        if (playerController != null)
        {
            return playerController.isFacingRight;
        }
        return transform.position.x > 0;
    }

    // �f�o�b�O�p�F���݂̃A�j���[�V������Ԃ�\��
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