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

    void Start()
    {
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
        // 相手が設定されていれば向き合う
        if (opponent != null)
        {
            FaceTowardOpponent();
        }
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