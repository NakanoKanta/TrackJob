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
        //HP�̏�����
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //Q�������ƃ_���[�W
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage();
        }

        //E�������Ɖ�
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal();
        }
    }

    void Damage()
    {
        //0�������Ȃ��悤�ɂ���
        _currentHP = Mathf.Max(_currentHP - _changeValue, 0);

        //fillAmount�ɑ��
        _image.fillAmount = _currentHP / _maxHP;

        Debug.Log($"HP�̊���:{_currentHP / _maxHP}");
    }
    void Heal()
    {
        //_maxHP������Ȃ��悤�ɂ���
        _currentHP = Mathf.Min(_currentHP + _changeValue, _maxHP);

        //fillAmount�ɑ��
        _image.fillAmount = _currentHP / _maxHP;

        Debug.Log($"HP�̊���:{_currentHP / _maxHP}");
    }
}
