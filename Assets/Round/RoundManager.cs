using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] int winsToWinMatch = 2;
    private int _player1Wins = 0;
    private int _player2Wins = 0;
    [SerializeField] GameObject _matchEndUI;
    
    public void Start()
    {
        Debug.Log(_player1Wins);
        Debug.Log(_player2Wins);
    }
    public void OnRoundEnd(int WinPlayer)
    {
        if (WinPlayer == 1)
        {
            _player1Wins++;
        }
        else if (WinPlayer == 2)
        {
            _player2Wins++;
        }
        if (_player1Wins >= winsToWinMatch)
        {
            FinishRound(1);
        }
        else if (_player2Wins >= winsToWinMatch)
        {
            FinishRound(2);
        }
        else 
        {
            Invoke("NextRound", 3f);
        }

    }
    public void NextRound()
    {
        Debug.Log("Next Round");
    }
    public void FinishRound(int Winner)
    {
        Debug.Log("èüé“ Player" + Winner);
    }
}
