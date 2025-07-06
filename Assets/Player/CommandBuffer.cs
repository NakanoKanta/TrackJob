using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CommandBuffer : MonoBehaviour
{
    private List<InputData> _buffer = new List<InputData>();
    private float _timeWindow = 1.0f;
    private int _maxSize = 8;

    public void AddInput(InputType input)
    {
        _buffer.Add(new InputData(input, Time.time));

        if (_buffer.Count > _maxSize)
        {
            //サイズ制限
            _buffer.RemoveAt(0);
        }
            //古い入力を削除
            CleanOldInputs();
    }

    public bool CheckCommand(InputType[] sequence)
    {
        if (_buffer.Count < sequence.Length) return false;

        //末尾からチェック
        int start = _buffer.Count - sequence.Length;
        for (int i = 0; i < sequence.Length; i++)
        {
            if (_buffer[start + i]._type != sequence[i])
            {
                return false;
            }
        }
        return true;
    }

    public void Clear() => _buffer.Clear();

    public void CleanOldInputs()
    {
        float currentTime = Time.time;
        _buffer.RemoveAll(Input => currentTime - Input._time > _timeWindow);
    }

    public string GetBufferString()
    {
        string result = "";
        foreach (var input in _buffer)
        {
            result += GetSymbol(input._type) + "";
        }
        return result;
    }

    private string GetSymbol(InputType type)
    {
        return type switch
        {
            InputType.Down => "↓",
            InputType.Up => "↑",
            InputType.Left => "←",
            InputType.Right => "→",
            InputType.Punch => "P",
            InputType.Guard => "K",
            _=> "?"
        };
    }
}