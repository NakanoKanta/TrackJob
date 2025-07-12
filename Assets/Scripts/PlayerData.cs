using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("��{���")]
    public string characterName;
    public Sprite characterImage;
    public GameObject characterPrefab;

    [Header("�L�����N�^�[�ŗL�R�}���h")]
    public Command[] Commands;

    [Header("�X�e�[�^�X")]
    public int health = 100;
    public int attack = 10;
    public float speed = 1.0f;

    [Header("�A�j���[�V����")]
    public RuntimeAnimatorController animatorController;
}

[System.Serializable]
public class Command
{
    public string _name;
    public InputType[] _inputs;
    public string _animation;
    public int damage = 10;
}

public enum InputType
{
    Up,
    Down,
    Left,
    Right,
    Punch,
    Guard,
}

public enum PlayerID
{
    Player1,
    Player2
}
