using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    private int _playerHP;
    private PlayerID _playerID;
    private bool isDead = false;

    private Image _playerHpBar;

    void Start()
    {
        _playerHP = _maxHP;

        //–¼‘O‚©‚ç PlayerID ‚ðŽ©“®”»’è
        _playerID = gameObject.name.Contains("1") ? PlayerID.Player1 : PlayerID.Player2;

        _playerHpBar = HPBarManager.Instance.GetHPBar(_playerID);
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

    void UpdateHPBar()
    {
        if (_playerHpBar != null)
        {
            _playerHpBar.fillAmount = (float)_playerHP / _maxHP;
        }
    }

    void Die()
    {
        isDead = true;

        //ŸŽÒ‚ð PlayerID Œ^‚ÅŽæ“¾
        PlayerID winner = (_playerID == PlayerID.Player1) ? PlayerID.Player2 : PlayerID.Player1;

        // PlayerID ‚ð“n‚·‚æ‚¤‚É•ÏX
        FindObjectOfType<RoundManager>().OnRoundEnd(winner);
    }
}
