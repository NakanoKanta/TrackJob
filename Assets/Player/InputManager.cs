using UnityEngine;

public class InputManager : MonoBehaviour
{
    public System.Action<InputType> OnInputDetected;

    private void Update()
    {
        // OnInputDetected��null�łȂ����`�F�b�N
        if (OnInputDetected == null) return;

        // �����L�[�i2P�Ή��j
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.I))
            OnInputDetected.Invoke(InputType.Up);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.J))
            OnInputDetected.Invoke(InputType.Right);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
            OnInputDetected.Invoke(InputType.Left);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K))
            OnInputDetected.Invoke(InputType.Down);

        // �{�^���i2P�Ή��j
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.H))
            OnInputDetected.Invoke(InputType.Punch);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            OnInputDetected.Invoke(InputType.Guard);
    }
}
