using UnityEngine;

public class DualingSystem : MonoBehaviour
{
    [Header("プレイヤー設定")]
    public FightingSystem player1System;
    public FightingSystem player2System;

    private InputManager _inputManager;

    void Start()
    {
        _inputManager = gameObject.AddComponent<InputManager>();

        // 各プレイヤーシステムを初期化
        player1System.Initialize();
        player2System.Initialize();

        // イベント登録
        _inputManager.OnInputDetected += OnInputReceived;
    }

    void OnInputReceived(InputType input, PlayerID player)
    {
        if (player == PlayerID.Player1 && player1System != null)
        {
            player1System.OnInputReceived(input);
        }
        else if (player == PlayerID.Player2 && player2System != null)
        {
            player2System.OnInputReceived(input);
        }
    }
}
