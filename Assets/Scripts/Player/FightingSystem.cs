using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    [Header("プレイヤー設定")]
    public PlayerID playerID;
    public Animator animator;
    public Command[] commands;

    [Header("キャラクター設定")]
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
        // CharacterSpawnから設定されたキャラクターインデックスを使用
        int characterIndex = playerID == PlayerID.Player1 ?
            SelectDataManager.CurrentData.Data1Index :
            SelectDataManager.CurrentData.Data2Index;

        // キャラクターデータベースから該当キャラクターを取得
        if (characterDatabase != null && characterIndex < characterDatabase.CharacterCount)
        {
            selectedCharacter = characterDatabase.GetCharacter(characterIndex);
            Debug.Log($"Player{(int)playerID + 1} キャラクター読み込み: {selectedCharacter.characterName}");
        }
        else
        {
            Debug.LogError($"Player{(int)playerID + 1} キャラクターデータが見つかりません");
        }
    }

    public void OnInputReceived(InputType input)
    {
        // 向きに応じて左右入力を反転
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
        Debug.Log($"Player{(int)playerID + 1} ({characterName}) コマンド発動: {command._name}");

        if (animator != null)
        {
            animator.SetTrigger(command._animation);
        }

        // コマンド使用回数を記録（統計用）
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
                _name = "真空波動拳コマンド",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Down, InputType.Right, InputType.Punch},
                _animation = "SinkuHadoken"
            },
            new Command
            {
                _name = "波動拳コマンド",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Punch},
                _animation = "Hadoken"
            },
            new Command
            {
                _name = "昇竜拳コマンド",
                _inputs = new [] {InputType.Right, InputType.Down, InputType.Punch},
                _animation = "Shoryuken"
            },
        };
    }

    // コマンド使用統計を保存
    void SaveCommandUsage(string commandName)
    {
        string key = $"Player{(int)playerID + 1}_{commandName}_Count";
        int currentCount = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, currentCount + 1);
        PlayerPrefs.Save();
    }

    // 設定保存用メソッド（必要に応じて）
    public void SavePlayerSettings()
    {
        // プレイヤーの設定を保存
        string playerPrefix = $"Player{(int)playerID + 1}";

        // 現在選択されているキャラクターのインデックスを保存
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

    // 統計情報取得用メソッド
    public int GetCommandUsageCount(string commandName)
    {
        string key = $"Player{(int)playerID + 1}_{commandName}_Count";
        return PlayerPrefs.GetInt(key, 0);
    }

    // 統計リセット用メソッド
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