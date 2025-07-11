using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TwoPlayerSelector : MonoBehaviour
{
    public CharacterData[] characterList => characterDatabase.characters;

    [SerializeField]
    private CharacterDatabase characterDatabase;

    // プレイヤー1用
    public Image player1Image;
    public TMP_Text player1Name;
    private int player1Index = 0;

    // プレイヤー2用
    public Image player2Image;
    public TMP_Text player2Name;
    private int player2Index = 0;

    void Start()
    {
        ShowPlayer1();
        ShowPlayer2();
    }

    // プレイヤー1の操作
    public void Player1Next()
    {
        player1Index = (player1Index + 1) % characterList.Length;
        ShowPlayer1();
    }

    public void Player1Prev()
    {
        player1Index = (player1Index - 1 + characterList.Length) % characterList.Length;
        ShowPlayer1();
    }

    void ShowPlayer1()
    {
        player1Image.sprite = characterList[player1Index].characterImage;
        player1Name.text = characterList[player1Index].characterName;
    }

    // プレイヤー2の操作
    public void Player2Next()
    {
        player2Index = (player2Index + 1) % characterList.Length;
        ShowPlayer2();
    }

    public void Player2Prev()
    {
        player2Index = (player2Index - 1 + characterList.Length) % characterList.Length;
        ShowPlayer2();
    }

    void ShowPlayer2()
    {
        player2Image.sprite = characterList[player2Index].characterImage;
        player2Name.text = characterList[player2Index].characterName;
    }

    // OKボタンを押したとき（2人とも）
    /*public void Confirm()
    {
        PlayerPrefs.SetInt("Player1CharacterIndex", player1Index);
        PlayerPrefs.SetInt("Player2CharacterIndex", player2Index);
        SelectDataManager.SetSelectData(player1Index, player2Index);
        SceneManager.LoadScene("GameScene");
    }*/
    public void Confirm()
    {
        // 選択したキャラのインデックスを保存
        PlayerPrefs.SetInt("Player1CharacterIndex", player1Index);
        PlayerPrefs.SetInt("Player2CharacterIndex", player2Index);

        // プレイヤー情報として文字列でも保存（オプション：判定しやすくなる）
        PlayerPrefs.SetString("Player1CharacterRole", "Player1");
        PlayerPrefs.SetString("Player2CharacterRole", "Player2");

        // セレクトマネージャーにも保存（別の保存方法）
        SelectDataManager.SetSelectData(player1Index, player2Index);

        // ゲームシーンに移動
        SceneManager.LoadScene("GameScene");
    }
}
