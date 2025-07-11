using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TwoPlayerSelector : MonoBehaviour
{
    public CharacterData[] characterList => characterDatabase.characters;

    [SerializeField]
    private CharacterDatabase characterDatabase;

    // �v���C���[1�p
    public Image player1Image;
    public TMP_Text player1Name;
    private int player1Index = 0;

    // �v���C���[2�p
    public Image player2Image;
    public TMP_Text player2Name;
    private int player2Index = 0;

    void Start()
    {
        ShowPlayer1();
        ShowPlayer2();
    }

    // �v���C���[1�̑���
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

    // �v���C���[2�̑���
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

    // OK�{�^�����������Ƃ��i2�l�Ƃ��j
    /*public void Confirm()
    {
        PlayerPrefs.SetInt("Player1CharacterIndex", player1Index);
        PlayerPrefs.SetInt("Player2CharacterIndex", player2Index);
        SelectDataManager.SetSelectData(player1Index, player2Index);
        SceneManager.LoadScene("GameScene");
    }*/
    public void Confirm()
    {
        // �I�������L�����̃C���f�b�N�X��ۑ�
        PlayerPrefs.SetInt("Player1CharacterIndex", player1Index);
        PlayerPrefs.SetInt("Player2CharacterIndex", player2Index);

        // �v���C���[���Ƃ��ĕ�����ł��ۑ��i�I�v�V�����F���肵�₷���Ȃ�j
        PlayerPrefs.SetString("Player1CharacterRole", "Player1");
        PlayerPrefs.SetString("Player2CharacterRole", "Player2");

        // �Z���N�g�}�l�[�W���[�ɂ��ۑ��i�ʂ̕ۑ����@�j
        SelectDataManager.SetSelectData(player1Index, player2Index);

        // �Q�[���V�[���Ɉړ�
        SceneManager.LoadScene("GameScene");
    }
}
