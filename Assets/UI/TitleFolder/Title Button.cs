using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�V�[����ǂݍ��ނ��߂ɕK�v
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
