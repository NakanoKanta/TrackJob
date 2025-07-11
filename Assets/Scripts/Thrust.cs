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
        // Thurst�A�j���[�V�������J�n����A�܂��ړ����Ă��Ȃ��ꍇ
        if (stateInfo.IsName("Shoryuken") && !_hasMoved)
        {
            _hasMoved = true;
            MovePlayer();
        }
        // Thurst�A�j���[�V�������I�������ꍇ�A�t���O�����Z�b�g
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
        // ���݈ʒu���瑊�ΓI�Ɉړ�
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + new Vector3(3, 0, 0);
        transform.DOMove(targetPos, 0.4f);
    }
}