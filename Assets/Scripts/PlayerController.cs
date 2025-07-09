using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("基本設定")]
    public PlayerID _playerID;
    public LayerMask _layerMask;
    public Transform _transform;
    [Header("移動パラメーター")]
    public float _playerSpeed;
    public float _jumpForce;
    public float _dashDuration;
    public float _dashCoolDown;
    [Header("フラグ")]
    public bool isGround = true;
    public bool canMove = false;
    public bool isJump = false;
    public bool isDash = false;
    public bool isFacingRight;
    [Header("コンポーネント")]
    public Rigidbody _rigidbody;
    public SpriteRenderer _spriteRenderer;
    public Animator _animator;

    private float _horizontalInput;
    private bool _jumpInput;
    private bool _dashInput;
    private float _lastDashSpeed;
    private PlayerController opponent;

    private int _animMoveSpeed = Animator.StringToHash("MoveSpeed");
    private int _animIsGround = Animator.StringToHash("IsGroundg");
    private int _animJump = Animator.StringToHash("Jump");
    private int _animDash = Animator.StringToHash("Dash");

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {

        }
    }
}
