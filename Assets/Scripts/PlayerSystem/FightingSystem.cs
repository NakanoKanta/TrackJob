using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]
    [SerializeField] private PlayerID playerID; // �C���X�y�N�^�[�ł͎Q�Ƃ̂�
    public Animator animator;
    public Command[] commands;
    private InputCommandBuffer _buffer;

    private void Awake()
    {
        // �v���C���[�Z���N�g�V�[������ݒ肳�ꂽplayerID���擾
        SetPlayerIDFromSavedData();
    }

    public void Initialize()
    {
        _buffer = new InputCommandBuffer();
        SetUpDefaultCommands();
    }

    private void SetPlayerIDFromSavedData()
    {
        // ���@1: PlayerPrefs���g�p����ꍇ
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            int savedPlayerID = PlayerPrefs.GetInt("PlayerID");
            playerID = (PlayerID)savedPlayerID;
            Debug.Log($"PlayerID�ݒ�: {playerID}");
        }

    }

    // �O������playerID��ݒ肷�郁�\�b�h
    public void SetPlayerID(PlayerID id)
    {
        playerID = id;
        Debug.Log($"PlayerID�ݒ�: {playerID}");
    }

    // ���݂�playerID���擾���郁�\�b�h
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
        Debug.Log($"Player{(int)playerID + 1} �R�}���h����: {command._name}");
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
}

// �v���C���[�Z���N�g�V�[���Ŏg�p����X�N���v�g��
public class PlayerSelectManager : MonoBehaviour
{
    public void SelectPlayer1()
    {
        PlayerPrefs.SetInt("PlayerID", (int)PlayerID.Player1);
        PlayerPrefs.Save();
        Debug.Log("Player1��I�����܂���");
    }

    public void SelectPlayer2()
    {
        PlayerPrefs.SetInt("PlayerID", (int)PlayerID.Player2);
        PlayerPrefs.Save();
        Debug.Log("Player2��I�����܂���");
    }

    public void LoadBattleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }
}

// �V���O���g���p�^�[�����g�p����ꍇ��GameManager��
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