using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]
    public PlayerID playerID;
    public Animator animator;
    public Command[] commands;

    [Header("�L�����N�^�[�ݒ�")]
    public CharacterDatabase characterDatabase;
    private CharacterData selectedCharacter;

    private InputCommandBuffer _buffer;
    public PlayerController playerController; 

    public void Initialize()
    {
        _buffer = new InputCommandBuffer();
        LoadSelectedCharacter();
        SetUpCharacterSpecificCommands();
    }

    void LoadSelectedCharacter()
    {
        // CharacterSpawn����ݒ肳�ꂽ�L�����N�^�[�C���f�b�N�X���g�p
        int characterIndex = playerID == PlayerID.Player1 ?
            SelectDataManager.CurrentData.Data1Index :
            SelectDataManager.CurrentData.Data2Index;

        // �L�����N�^�[�f�[�^�x�[�X����Y���L�����N�^�[���擾
        if (characterDatabase != null && characterIndex < characterDatabase.CharacterCount)
        {
            selectedCharacter = characterDatabase.GetCharacter(characterIndex);
            Debug.Log($"Player{(int)playerID + 1} �L�����N�^�[�ǂݍ���: {selectedCharacter.characterName}");
        }
        else
        {
            Debug.LogError($"Player{(int)playerID + 1} �L�����N�^�[�f�[�^��������܂���");
        }
    }

    public void OnInputReceived(InputType input)
    {
        // �����ɉ����č��E���͂𔽓]
        bool isFacingLeft = playerController != null && playerController.GetFacingLeft();
        InputType convertedInput = PlayerController.InputConverter.ConvertInput(input, isFacingLeft);

        Debug.Log($"[InputReceived] {playerID}: {input} (Converted: {convertedInput})");
        _buffer.AddInput(convertedInput);
        CheckAllCommands();
    }

    void CheckAllCommands()
    {
        foreach (var command in commands)
        {
            if (_buffer.CheckCommand(command._inputs))
            {
                ExecuteCommand(command);
                _buffer.Clear();
                break;
            }
        }
    }

    void ExecuteCommand(Command command)
    {
        string characterName = selectedCharacter != null ? selectedCharacter.characterName : "Unknown";
        Debug.Log($"Player{(int)playerID + 1} ({characterName}) �R�}���h����: {command._name}");

        if (animator != null)
        {
            animator.SetTrigger(command._animation);
        }

        // �R�}���h�g�p�񐔂��L�^�i���v�p�j
        SaveCommandUsage(command._name);
    }

    void SetUpCharacterSpecificCommands()
    {
        if (selectedCharacter != null)
        {
            SetUpDefaultCommands();
        }
        else
        {
            SetUpDefaultCommands();
        }
    }

    void SetUpDefaultCommands()
    {
        commands = new Command[]
        {
            new Command
            {
                _name = "�^��g�����R�}���h",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Down, InputType.Right, InputType.Punch},
                _animation = "SinkuHadoken"
            },
            new Command
            {
                _name = "�g�����R�}���h",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Punch},
                _animation = "Hadoken"
            },
            new Command
            {
                _name = "�������R�}���h",
                _inputs = new [] {InputType.Right, InputType.Down, InputType.Punch},
                _animation = "Shoryuken"
            },
        };
    }

    // �R�}���h�g�p���v��ۑ�
    void SaveCommandUsage(string commandName)
    {
        string key = $"Player{(int)playerID + 1}_{commandName}_Count";
        int currentCount = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, currentCount + 1);
        PlayerPrefs.Save();
    }

    // �ݒ�ۑ��p���\�b�h�i�K�v�ɉ����āj
    public void SavePlayerSettings()
    {
        // �v���C���[�̐ݒ��ۑ�
        string playerPrefix = $"Player{(int)playerID + 1}";

        // ���ݑI������Ă���L�����N�^�[�̃C���f�b�N�X��ۑ�
        if (selectedCharacter != null)
        {
            for (int i = 0; i < characterDatabase.CharacterCount; i++)
            {
                if (characterDatabase.GetCharacter(i) == selectedCharacter)
                {
                    PlayerPrefs.SetInt($"{playerPrefix}_CharacterIndex", i);
                    break;
                }
            }
        }

        PlayerPrefs.Save();
    }

    // ���v���擾�p���\�b�h
    public int GetCommandUsageCount(string commandName)
    {
        string key = $"Player{(int)playerID + 1}_{commandName}_Count";
        return PlayerPrefs.GetInt(key, 0);
    }

    // ���v���Z�b�g�p���\�b�h
    public void ResetCommandStats()
    {
        string playerPrefix = $"Player{(int)playerID + 1}";
        foreach (var command in commands)
        {
            string key = $"{playerPrefix}_{command._name}_Count";
            PlayerPrefs.DeleteKey(key);
        }
        PlayerPrefs.Save();
    }
}