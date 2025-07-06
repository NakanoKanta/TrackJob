[System.Serializable]
public enum InputType
{
    Down,
    Up,
    Left,
    Right,
    Punch,
    Guard,
}

[System.Serializable]
public struct InputData
{
    public InputType _type;
    public float _time;
    public InputData(InputType inputType, float timestamp)
    {
        _type = inputType;
        _time = timestamp;
    }
}

[System.Serializable]
public struct Command
{
    public string _name;
    public InputType[] _inputs;
    public string _animation;
}