using UnityEngine;
using DG.Tweening;

public class Thrust : MonoBehaviour
{
    public Animator _thurst;
    bool _hasMoved = false;

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
        // 現在位置から相対的に移動
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + new Vector3(3, 0, 0);
        transform.DOMove(targetPos, 0.4f);
    }
}