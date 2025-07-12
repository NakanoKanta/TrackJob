using UnityEngine;

public class InputManager : MonoBehaviour
{
    public System.Action<InputType, PlayerID> OnInputDetected;

    private void Update()
    {
        // OnInputDetected��null�łȂ����`�F�b�N
        if (OnInputDetected == null) return;

        // �����L�[�i1P�j
        if (Input.GetKeyDown(KeyCode.W))
            OnInputDetected.Invoke(InputType.Up, PlayerID.Player1);
        if (Input.GetKeyDown(KeyCode.D))
            OnInputDetected.Invoke(InputType.Right, PlayerID.Player1);
        if (Input.GetKeyDown(KeyCode.A))
            OnInputDetected.Invoke(InputType.Left, PlayerID.Player1);
        if (Input.GetKeyDown(KeyCode.S))
            OnInputDetected.Invoke(InputType.Down, PlayerID.Player1);

        // �{�^���i1P�j
        if (Input.GetKeyDown(KeyCode.F))
            OnInputDetected.Invoke(InputType.Punch, PlayerID.Player1);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnInputDetected.Invoke(InputType.Guard, PlayerID.Player1);


        // �����L�[�i2P�j
        if (Input.GetKeyDown(KeyCode.I))
            OnInputDetected.Invoke(InputType.Up, PlayerID.Player2);
        if (Input.GetKeyDown(KeyCode.L))
            OnInputDetected.Invoke(InputType.Right, PlayerID.Player2);
        if (Input.GetKeyDown(KeyCode.J))
            OnInputDetected.Invoke(InputType.Left, PlayerID.Player2);
        if (Input.GetKeyDown(KeyCode.K))
            OnInputDetected.Invoke(InputType.Down, PlayerID.Player2);

        // �{�^���i2P�j
        if (Input.GetKeyDown(KeyCode.H))
            OnInputDetected.Invoke(InputType.Punch, PlayerID.Player2);
        if (Input.GetKeyDown(KeyCode.RightShift))
            OnInputDetected.Invoke(InputType.Guard, PlayerID.Player2);
    }
}
