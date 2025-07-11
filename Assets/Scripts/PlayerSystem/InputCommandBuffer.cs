using System.Collections.Generic;
using UnityEngine;

public class InputCommandBuffer
{
    private List<InputData> _buffer = new List<InputData>();
    private List<InputType> _inputTypes = new List<InputType>();
    private float _timeWindow = 1.0f;
    private int _maxSize = 8;
    private bool _facingLeft;
    public void AddInput(InputType input)
    { 
        // 入力を向きに応じて変換してからバッファに追加
        InputType ConvertInput = InputConverter.ConvertInput(input, _facingLeft);
        _inputTypes.Add(ConvertInput);
        _buffer.Add(new InputData(input, Time.time));
        if (_buffer.Count > _maxSize)
        {
            _buffer.RemoveAt(0);
        }
        CleanOldInputs();
    }

    public bool CheckCommand(InputType[] sequence)
    {
        if (_buffer.Count < sequence.Length) return false;
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
        _buffer.RemoveAll(input => currentTime - input._time > _timeWindow);
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
            _ => "?"
        };
    }
}