using UnityEngine;

public class FaceOpponent : MonoBehaviour
{
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
}