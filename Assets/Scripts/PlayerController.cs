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

    [Header("�ړ��ݒ�")]
    public float _moveSpeed = 5f;           // �ړ����x
    public float _jumpForce = 10f;          // �W�����v��
    public string _groundTag = "Ground";    // �n�ʂ̃^�O

    private Rigidbody2D _rb;
    private bool _isGrounded;
    private float _horizontal;
    private bool jumpInput;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        GetInput();
        UpdateAnimationParameters();
        // ���肪�ݒ肳��Ă���Ό�������
        if (opponent != null)
        {
            FaceTowardOpponent();
        }
    }

    void FixedUpdate()
    {
        Move();
        if (jumpInput && _isGrounded)
        {
            Jump();
        }
    }

    /// <summary>
    /// ���͂��擾
    /// </summary>
    private void GetInput()
    {
        // PlayerID�Ɋ�Â��ē��͕���������
        if (playerID == PlayerID.Player1)
        {
            // Player1: WASD����
            _horizontal = 0f;
            if (Input.GetKey(KeyCode.A)) _horizontal = -1f;  // GetKeyDown �� GetKey
            if (Input.GetKey(KeyCode.D)) _horizontal = 1f;   // GetKeyDown �� GetKey
            jumpInput = Input.GetKeyDown(KeyCode.W);
        }
        else if (playerID == PlayerID.Player2)
        {
            // Player2: IJKL����
            _horizontal = 0f;
            if (Input.GetKey(KeyCode.J)) _horizontal = -1f;  // GetKeyDown �� GetKey
            if (Input.GetKey(KeyCode.L)) _horizontal = 1f;   // GetKeyDown �� GetKey
            jumpInput = Input.GetKeyDown(KeyCode.I);
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        if (_rb != null)
        {
            // �����ړ��ivelocity.x �� velocity.y�ɏC���j
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
        }
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    private void Jump()
    {
        if (_rb != null)
        {
            // �W�����v�ivelocity.y �� velocity.x�ɏC���j
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }
    private void UpdateAnimationParameters()
    {
        if (_animator != null)
        {
            // �n�ʂɂ��邩�ǂ����ŃA�j���[�^�[�̗L��/������؂�ւ�
            _animator.enabled = _isGrounded;

            // �n�ʂɂ���ꍇ�̂݃A�j���[�V�����p�����[�^���X�V
            if (_isGrounded)
            {
                _animator.SetTrigger("SinkuHadoken");
                _animator.SetTrigger("hadoken");
                _animator.SetTrigger("Shoryuken");
            }
        }
    }
    /// <summary>
    /// �n�ʂƂ̐ڐG����iCollision�g�p�j
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �n�ʃ^�O�̃I�u�W�F�N�g�ƐڐG�����ꍇ
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    /// <summary>
    /// �n�ʂ��痣�ꂽ���̏���
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        // �n�ʃ^�O�̃I�u�W�F�N�g���痣�ꂽ�ꍇ
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    /// <summary>
    /// �n�ʂɐڐG���Ă���Ԃ̏���
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        // �n�ʃ^�O�̃I�u�W�F�N�g�ɐڐG�������Ă���ꍇ
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
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

    /// <summary>
    /// �n�ʂɂ��邩�ǂ������擾
    /// </summary>
    public bool IsGrounded()
    {
        return _isGrounded;
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