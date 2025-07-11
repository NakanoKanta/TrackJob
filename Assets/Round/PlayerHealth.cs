using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 100;
    [SerializeField] int _playerHP;
    [SerializeField] int _playerNumber = 0;
    private bool isDead = false;
    [SerializeField] Slider _playerHpBar;
    void Start()
    {
        _playerHP = _maxHP;
        if(_playerHpBar != null)
        {
            _playerHpBar.maxValue = _playerHP;
            _playerHpBar.value = _playerHP;
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        _playerHP -= damage;
        _playerHP = Mathf.Max(_playerHP, 0);
        if (_playerHpBar != null)
        {
            _playerHpBar.value = _playerHP;
        } 

        if (_playerHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        int WinPlayer = (_playerNumber == 1) ? 2 : 1;
        FindObjectOfType<RoundManager>().OnRoundEnd(WinPlayer);
    }

}
