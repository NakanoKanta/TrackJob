using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//シーンを読み込むために必要
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
