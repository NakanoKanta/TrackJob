using UnityEngine;
using UnityEngine.UI;
public class HPGauge : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float _maxHP = 100;
    [SerializeField] float _currentHP = 100;
    [SerializeField] float _changeValue = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HP‚Ì‰Šú‰»
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //Q‚ğ‰Ÿ‚·‚Æƒ_ƒ[ƒW
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage();
        }

        //E‚ğ‰Ÿ‚·‚Æ‰ñ•œ
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal();
        }
    }

    void Damage()
    {
        //0‚ğ‰º‰ñ‚ç‚È‚¢‚æ‚¤‚É‚·‚é
        _currentHP = Mathf.Max(_currentHP - _changeValue, 0);

        //fillAmount‚É‘ã“ü
        _image.fillAmount = _currentHP / _maxHP;

        Debug.Log($"HP‚ÌŠ„‡:{_currentHP / _maxHP}");
    }
    void Heal()
    {
        //_maxHP‚ğã‰ñ‚ç‚È‚¢‚æ‚¤‚É‚·‚é
        _currentHP = Mathf.Min(_currentHP + _changeValue, _maxHP);

        //fillAmount‚É‘ã“ü
        _image.fillAmount = _currentHP / _maxHP;

        Debug.Log($"HP‚ÌŠ„‡:{_currentHP / _maxHP}");
    }
}
