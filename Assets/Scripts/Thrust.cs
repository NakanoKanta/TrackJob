using UnityEngine;
using DG.Tweening;

public class Thrust : MonoBehaviour
{
    public Animator _thurst;
    bool _hasMoved = false;
    public PlayerController playerController;

    void Start()
    {
        _thurst = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = _thurst.GetCurrentAnimatorStateInfo(0);
        // Thurstアニメーションが開始され、まだ移動していない場合
        if (stateInfo.IsName("Shoryuken") && !_hasMoved)
        {
            Debug.Log("shoryuken");
            _hasMoved = true;
            MovePlayer();
        }
        // Thurstアニメーションが終了した場合、フラグをリセット
        else if (!stateInfo.IsName("Shoryuken"))
        {
            if (_hasMoved)
            {
                _hasMoved = false;
            }
        }
    }

    void MovePlayer()
    {
        // プレイヤーの向きを確認
        int direction = 1; // 右向き
        if (playerController != null && playerController.GetFacingLeft())
        {
            direction = -1; // 左向きなら反転
        }

        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + new Vector3(3 * direction, 0, 0); // ← 方向に応じて移動
        transform.DOMove(targetPos, 0.4f);
    }
}