using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    private int _playerHP;

    [SerializeField] private PlayerID _playerID;  // Inspectorにセットされていないなら0(デフォルト)

    private Image _playerHpBar;

    private bool isDead = false;

    void Start()
    {
        _playerHP = _maxHP;

        // PlayerIDがデフォルト値なら親から探す（PlayerControllerかFightingSystemのplayerID）
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
                    Debug.LogError($"{gameObject.name} の PlayerID が設定されていません。PlayerControllerかFightingSystemを確認してください。");
                }
            }
        }

        _playerHpBar = HPBarManager.Instance.GetHPBar(_playerID);
        if (_playerHpBar == null)
        {
            Debug.LogError($"HPバーがPlayerID {_playerID} に対して見つかりません");
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
