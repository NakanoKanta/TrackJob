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
        // Thurst�A�j���[�V�������J�n����A�܂��ړ����Ă��Ȃ��ꍇ
        if (stateInfo.IsName("Shoryuken") && !_hasMoved)
        {
            Debug.Log("shoryuken");
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
        // �v���C���[�̌������m�F
        int direction = 1; // �E����
        if (playerController != null && playerController.GetFacingLeft())
        {
            direction = -1; // �������Ȃ甽�]
        }

        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentPos + new Vector3(3 * direction, 0, 0); // �� �����ɉ����Ĉړ�
        transform.DOMove(targetPos, 0.4f);
    }
}