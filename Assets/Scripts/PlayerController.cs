using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�L�����N�^�[�f�[�^")]
    public CharacterData characterData;    // �L�����N�^�[�f�[�^
    public PlayerID playerID;              // �v���C���[ID

    [Header("�ݒ�")]
    private Transform opponent;              // �����Transform
    public SpriteRenderer spriteRenderer;  // �X�v���C�g�����_���[
    public bool isFacingRight = true;      // ���݉E�������ǂ���
    public bool facingLeft => !isFacingRight;  // ���������ǂ���

    [Header("���������ݒ�")]
    public bool autoFindOpponent = true;   // ����������ŒT����
    public string opponentTag = "Player";  // ����̃^�O

    void Start()
    {
        // �X�v���C�g�����_���[�̎����擾
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // ����������ŒT��
        if (autoFindOpponent && opponent == null)
        {
            FindOpponent();
        }
    }

    void Update()
    {
        // ���肪�ݒ肳��Ă���Ό�������
        if (opponent != null)
        {
            FaceTowardOpponent();
        }
    }

    /// <summary>
    /// �L�����N�^�[�f�[�^��ݒ�
    /// </summary>
    public void SetCharacterData(CharacterData data, PlayerID id)
    {
        characterData = data;
        playerID = id;

        // �L�����N�^�[�f�[�^����ɏ�����
        if (characterData != null)
        {

            // �A�j���[�V�����R���g���[���[�̐ݒ�
            Animator animator = GetComponent<Animator>();
            if (animator != null && characterData.animatorController != null)
            {
                animator.runtimeAnimatorController = characterData.animatorController;
            }

            // ���̑��̃L�����N�^�[�ŗL�̐ݒ肪����΂����ōs��
            // ��F�ړ����x�A�W�����v�́A�̗͂Ȃ�
        }
    }

    /// <summary>
    /// ����̕���������
    /// </summary>
    private void FaceTowardOpponent()
    {
        // ����Ƃ̈ʒu�֌W���v�Z
        float directionToOpponent = opponent.position.x - transform.position.x;
        // ���肪�E�ɂ���ꍇ�͉E�����A���ɂ���ꍇ�͍�����
        bool shouldFaceRight = directionToOpponent > 0;
        // ���݂̌����ƈႤ�ꍇ�͌�����ς���
        if (shouldFaceRight != isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// �����𔽓]������
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // �X�v���C�g�𔽓]
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !isFacingRight;
        }
    }

    /// <summary>
    /// ����������ŒT��
    /// </summary>
    private void FindOpponent()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(opponentTag);
        foreach (GameObject player in players)
        {
            // �����ȊO�̃v���C���[�𑊎�Ƃ��Đݒ�
            if (player != this.gameObject)
            {
                opponent = player.transform;
                break;
            }
        }
        // ������Ȃ������ꍇ�̌x��
        if (opponent == null)
        {
            Debug.LogWarning($"���肪������܂���B�^�O '{opponentTag}' �̃I�u�W�F�N�g�����݂��邩�m�F���Ă��������B");
        }
    }

    /// <summary>
    /// �O�����瑊���ݒ�
    /// </summary>
    public void SetOpponent(Transform newOpponent)
    {
        opponent = newOpponent;
    }

    /// <summary>
    /// �O�����������ݒ�
    /// </summary>
    public void SetFacing(bool faceRight)
    {
        if (isFacingRight != faceRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// �������擾
    /// </summary>
    public bool GetFacingRight()
    {
        return isFacingRight;
    }

    public bool GetFacingLeft()
    {
        return facingLeft;
    }

    public class InputConverter
    {
        public static InputType ConvertInput(InputType input, bool facingLeft)
        {
            if (!facingLeft) return input; // �E�����̏ꍇ�͂��̂܂�
            // �������̏ꍇ�͍��E�𔽓]
            switch (input)
            {
                case InputType.Left:
                    return InputType.Right;
                case InputType.Right:
                    return InputType.Left;
                default:
                    return input;
            }
        }
    }
}