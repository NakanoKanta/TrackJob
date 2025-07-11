using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    [Header("プレイヤー設定")]
    [SerializeField] private PlayerID playerID; // インスペクターでは参照のみ
    public Animator animator;
    public Command[] commands;
    private InputCommandBuffer _buffer;

    private void Awake()
    {
        // プレイヤーセレクトシーンから設定されたplayerIDを取得
        SetPlayerIDFromSavedData();
    }

    public void Initialize()
    {
        _buffer = new InputCommandBuffer();
        SetUpDefaultCommands();
    }

    private void SetPlayerIDFromSavedData()
    {
        // 方法1: PlayerPrefsを使用する場合
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            int savedPlayerID = PlayerPrefs.GetInt("PlayerID");
            playerID = (PlayerID)savedPlayerID;
            Debug.Log($"PlayerID設定: {playerID}");
        }

    }

    // 外部からplayerIDを設定するメソッド
    public void SetPlayerID(PlayerID id)
    {
        playerID = id;
        Debug.Log($"PlayerID設定: {playerID}");
    }

    // 現在のplayerIDを取得するメソッド
    public PlayerID GetPlayerID()
    {
        return playerID;
    }

    public void OnInputReceived(InputType input)
    {
        _buffer.AddInput(input);
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
        Debug.Log($"Player{(int)playerID + 1} コマンド発動: {command._name}");
        if (animator != null)
        {
            animator.SetTrigger(command._animation);
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
}

// プレイヤーセレクトシーンで使用するスクリプト例
public class PlayerSelectManager : MonoBehaviour
{
    public void SelectPlayer1()
    {
        PlayerPrefs.SetInt("PlayerID", (int)PlayerID.Player1);
        PlayerPrefs.Save();
        Debug.Log("Player1を選択しました");
    }

    public void SelectPlayer2()
    {
        PlayerPrefs.SetInt("PlayerID", (int)PlayerID.Player2);
        PlayerPrefs.Save();
        Debug.Log("Player2を選択しました");
    }

    public void LoadBattleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }
}

// シングルトンパターンを使用する場合のGameManager例
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerID selectedPlayerID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerID(PlayerID id)
    {
        selectedPlayerID = id;
    }

    public PlayerID GetPlayerID()
    {
        return selectedPlayerID;
    }
}