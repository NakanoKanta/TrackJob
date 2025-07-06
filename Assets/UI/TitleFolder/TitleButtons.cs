using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
//シーンを変えるときに使用
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{

    public void StartBtn()
    {
        //次の画面のシーンを入力
        SceneManager.LoadScene("");
    }

}

