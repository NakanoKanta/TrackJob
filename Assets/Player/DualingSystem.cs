using UnityEngine;

public class DualingSystem : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]
    public FightingSystem player1System;
    public FightingSystem player2System;

    private InputManager _inputManager;

    void Start()
    {
        _inputManager = gameObject.AddComponent<InputManager>();

        // �e�v���C���[�V�X�e����������
        player1System.Initialize();
        player2System.Initialize();

        // �C�x���g�o�^
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
