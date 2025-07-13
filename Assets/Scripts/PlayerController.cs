using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("キャラクターデータ")]
    public CharacterData characterData;    // キャラクターデータ
    public PlayerID playerID;              // プレイヤーID

    [Header("設定")]
    private Transform opponent;              // 相手のTransform
    public SpriteRenderer spriteRenderer;  // スプライトレンダラー
    public bool isFacingRight = true;      // 現在右向きかどうか
    public bool facingLeft => !isFacingRight;  // 左向きかどうか

    [Header("自動検索設定")]
    public bool autoFindOpponent = true;   // 相手を自動で探すか
    public string opponentTag = "Player";  // 相手のタグ

    [Header("移動設定")]
    public float _moveSpeed = 5f;           // 移動速度
    public float _jumpForce = 10f;          // ジャンプ力
    public LayerMask _groundLayer = 1;      // 地面のレイヤー
    public float _groundCheckDistance = 0.1f; // 地面チェックの距離

    private Rigidbody2D _rb;
    private bool _isGrounded;
    private float _horizontal;
    private bool jumpInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // スプライトレンダラーの自動取得
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // 相手を自動で探す
        if (autoFindOpponent && opponent == null)
        {
            FindOpponent();
        }
    }

    void Update()
    {
        GetInput();
        CheckGrounded();
        // 相手が設定されていれば向き合う
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
    /// 入力を取得
    /// </summary>
    private void GetInput()
    {
        // PlayerIDに基づいて入力方式を決定
        if (playerID == PlayerID.Player1)
        {
            // Player1: WASD入力
            _horizontal = 0f;
            if (Input.GetKeyDown(KeyCode.A)) _horizontal = -1f;
            if (Input.GetKeyDown(KeyCode.D)) _horizontal = 1f;
            jumpInput = Input.GetKeyDown(KeyCode.W);
        }
        else if (playerID == PlayerID.Player2)
        {
            // Player2: IJKL入力
            _horizontal = 0f;
            if (Input.GetKeyDown(KeyCode.J)) _horizontal = -1f;
            if (Input.GetKeyDown(KeyCode.L)) _horizontal = 1f;
            jumpInput = Input.GetKeyDown(KeyCode.I);
        }
    }
    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        if (_rb != null)
        {
            // 水平移動
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.x);
        }
    }
    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        if (_rb != null)
        {
            _rb.velocity = new Vector2(_rb.velocity.y, _jumpForce);
        }
    }
    /// <summary>
    /// 地面チェック
    /// </summary>
    private void CheckGrounded()
    {
        // キャラクターの足元から下向きにレイキャストを飛ばす
        Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.size.y / 2);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, _groundCheckDistance, _groundLayer);
        _isGrounded = hit.collider != null;
    }
    /// <summary>
    /// キャラクターデータを設定
    /// </summary>
    public void SetCharacterData(CharacterData data, PlayerID id)
    {
        characterData = data;
        playerID = id;

        // キャラクターデータを基に初期化
        if (characterData != null)
        {

            // アニメーションコントローラーの設定
            Animator animator = GetComponent<Animator>();
            if (animator != null && characterData.animatorController != null)
            {
                animator.runtimeAnimatorController = characterData.animatorController;
            }

            // その他のキャラクター固有の設定があればここで行う
            // 例：移動速度、ジャンプ力、体力など
        }
    }

    /// <summary>
    /// 相手の方向を向く
    /// </summary>
    private void FaceTowardOpponent()
    {
        // 相手との位置関係を計算
        float directionToOpponent = opponent.position.x - transform.position.x;
        // 相手が右にいる場合は右向き、左にいる場合は左向き
        bool shouldFaceRight = directionToOpponent > 0;
        // 現在の向きと違う場合は向きを変える
        if (shouldFaceRight != isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// 向きを反転させる
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // スプライトを反転
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !isFacingRight;
        }
    }

    /// <summary>
    /// 相手を自動で探す
    /// </summary>
    private void FindOpponent()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(opponentTag);
        foreach (GameObject player in players)
        {
            // 自分以外のプレイヤーを相手として設定
            if (player != this.gameObject)
            {
                opponent = player.transform;
                break;
            }
        }
        // 見つからなかった場合の警告
        if (opponent == null)
        {
            Debug.LogWarning($"相手が見つかりません。タグ '{opponentTag}' のオブジェクトが存在するか確認してください。");
        }
    }

    /// <summary>
    /// 外部から相手を設定
    /// </summary>
    public void SetOpponent(Transform newOpponent)
    {
        opponent = newOpponent;
    }

    /// <summary>
    /// 外部から向きを設定
    /// </summary>
    public void SetFacing(bool faceRight)
    {
        if (isFacingRight != faceRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// 向きを取得
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
    /// 地面にいるかどうかを取得
    /// </summary>
    public bool IsGrounded()
    {
        return _isGrounded;
    }
    public class InputConverter
    {
        public static InputType ConvertInput(InputType input, bool facingLeft)
        {
            if (!facingLeft) return input; // 右向きの場合はそのまま
            // 左向きの場合は左右を反転
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