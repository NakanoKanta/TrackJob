using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField] int winsToWinMatch = 2;

    private static int _player1Wins = 0;
    private static int _player2Wins = 0;

    [SerializeField] GameObject _matchEndUI;
    [SerializeField] GameObject _titleButtonUI;

    [Header("Ÿ—˜”•\¦UI")]
    [SerializeField] Text player1WinsText;
    [SerializeField] Text player2WinsText;

    void Start()
    {
        UpdateWinUI();
    }

    public void OnRoundEnd(PlayerID winnerId)
    {
        if (winnerId == PlayerID.Player1)
        {
            _player1Wins++;
            Debug.Log("ŸÒ Player 1");
        }
        else if (winnerId == PlayerID.Player2)
        {
            _player2Wins++;
            Debug.Log("ŸÒ Player 2");
        }

        UpdateWinUI();

        if (_player1Wins == winsToWinMatch)
        {
            FinishRound(PlayerID.Player1);
        }
        else if (_player2Wins == winsToWinMatch)
        {
            FinishRound(PlayerID.Player2);
        }
        else
        {
            Invoke(nameof(NextRound), 1f);
        }
    }

    void UpdateWinUI()
    {
        if (player1WinsText != null)
            player1WinsText.text = $"Player 1 Wins: {_player1Wins}";
        else
            Debug.LogWarning("player1WinsText‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");

        if (player2WinsText != null)
            player2WinsText.text = $"Player 2 Wins: {_player2Wins}";
        else
            Debug.LogWarning("player2WinsText‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
    }

    public void NextRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Next Round");
    }

    public void FinishRound(PlayerID winner)
    {
        //Time.timeScale = 0f;
        Debug.Log($"Š®‘SŸ—˜: Player {(winner == PlayerID.Player1 ? "1" : "2")}");

        _player1Wins = 0;
        _player2Wins = 0;

        if (_titleButtonUI != null)
            _titleButtonUI.SetActive(true);

        if (_matchEndUI != null)
            _matchEndUI.SetActive(true);
    }
}
