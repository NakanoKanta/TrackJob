using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBarManager : MonoBehaviour
{
    public Image player1HPBar;
    public Image player2HPBar;

    public static HPBarManager Instance;

    void Awake() => Instance = this;

    public Image GetHPBar(PlayerID id)
    {
        return (id == PlayerID.Player1) ? player1HPBar : player2HPBar;
    }
}
