using UnityEngine;
public class FightingSystem : MonoBehaviour
{
    [Header("プレイヤー設定")]
    public PlayerID playerID;
    public Animator animator;
    public Command[] commands;

    private InputCommandBuffer _buffer;

    public void Initialize()
    {
        _buffer = new InputCommandBuffer();
        SetUpDefaultCommands();
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