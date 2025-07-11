using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Counter : MonoBehaviour
{
    Animator _counter;
    public GameObject _effect;
    public GameObject _playerPosition;
    void Update()
    {
        if (_counter == null)
        {
            _effect.SetActive(false);
        }
        if (_counter != null)
        {
            _effect.SetActive(true);
        }
    }
}
