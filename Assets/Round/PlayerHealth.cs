using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    private int _playerHP;

    [SerializeField] private PlayerID _playerID;  // Inspector�ɃZ�b�g����Ă��Ȃ��Ȃ�0(�f�t�H���g)

    private Image _playerHpBar;

    private bool isDead = false;

    void Start()
    {
        _playerHP = _maxHP;

        // PlayerID���f�t�H���g�l�Ȃ�e����T���iPlayerController��FightingSystem��playerID�j
        if (_playerID == 0)
        {
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null)
            {
                _playerID = pc.playerID;
            }
            else
            {
                FightingSystem fs = GetComponent<FightingSystem>();
                if (fs != null)
                {
                    _playerID = fs.playerID;
                }
                else
                {
                    Debug.LogError($"{gameObject.name} �� PlayerID ���ݒ肳��Ă��܂���BPlayerController��FightingSystem���m�F���Ă��������B");
                }
            }
        }

        _playerHpBar = HPBarManager.Instance.GetHPBar(_playerID);
        if (_playerHpBar == null)
        {
            Debug.LogError($"HP�o�[��PlayerID {_playerID} �ɑ΂��Č�����܂���");
        }

        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        _playerHP -= damage;
        _playerHP = Mathf.Max(_playerHP, 0);

        UpdateHPBar();

        if (_playerHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHPBar()
    {
        if (_playerHpBar != null)
        {
            _playerHpBar.fillAmount = (float)_playerHP / _maxHP;
        }
    }

    private void Die()
    {
        isDead = true;

        PlayerID winner = (_playerID == PlayerID.Player1) ? PlayerID.Player2 : PlayerID.Player1;

        FindObjectOfType<RoundManager>().OnRoundEnd(winner);
    }
}
