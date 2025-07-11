using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    int p1 = SelectDataManager.CurrentData.Data1Index;
    int p2 = SelectDataManager.CurrentData.Data2Index;

    [Header("�L�����N�^�[�f�[�^�x�[�X")]
    public CharacterDatabase characterDatabase;

    [Header("��{�L�����N�^�[�v���n�u")]
    public GameObject baseCharacterPrefab; // ��{�ƂȂ�L�����N�^�[�v���n�u

    void Start()
    {
        GameObject player1 = null;
        GameObject player2 = null;

        // Player1�̃L�����N�^�[����
        if (characterDatabase != null && p1 < characterDatabase.CharacterCount)
        {
            CharacterData player1Data = characterDatabase.GetCharacter(p1);
            player1 = CreateCharacter(player1Data, new Vector2(-8, 0), PlayerID.Player1);
        }

        // Player2�̃L�����N�^�[����
        if (characterDatabase != null && p2 < characterDatabase.CharacterCount)
        {
            CharacterData player2Data = characterDatabase.GetCharacter(p2);
            player2 = CreateCharacter(player2Data, new Vector2(8, 0), PlayerID.Player2);
        }

        // FightingSystem���擾
        FightingSystem fs1 = player1?.GetComponent<FightingSystem>();
        FightingSystem fs2 = player2?.GetComponent<FightingSystem>();

        // InputManager��T���ăC�x���g�o�^
        InputManager inputManager = FindObjectOfType<InputManager>();
        if (inputManager != null)
        {
            inputManager.OnInputDetected += (input, id) =>
            {
                if (id == PlayerID.Player1 && fs1 != null)
                {
                    fs1.OnInputReceived(input);
                }
                else if (id == PlayerID.Player2 && fs2 != null)
                {
                    fs2.OnInputReceived(input);
                }
            };
        }
        else
        {
            Debug.LogError("InputManager���V�[����Ɍ�����܂���ł����I");
        }
    }


    GameObject CreateCharacter(CharacterData characterData, Vector2 position, PlayerID playerID)
    {
        // ��{�v���n�u�𐶐�
        GameObject character = Instantiate(baseCharacterPrefab, position, Quaternion.identity);

        // PlayerController�R���|�[�l���g���擾
        PlayerController controller = character.GetComponent<PlayerController>();
        if (controller == null)
        {
            controller = character.AddComponent<PlayerController>();
        }

        // �L�����N�^�[�f�[�^��ݒ�
        controller.SetCharacterData(characterData, playerID);

        // FightingSystem�R���|�[�l���g���擾�E�ݒ�
        FightingSystem fightingSystem = character.GetComponent<FightingSystem>();
        if (fightingSystem == null)
        {
            fightingSystem = character.AddComponent<FightingSystem>();
        }

        // FightingSystem�̏����ݒ�
        fightingSystem.playerID = playerID;
        fightingSystem.characterDatabase = characterDatabase;

        // Animator�R���|�[�l���g���擾���Đݒ�
        Animator animator = character.GetComponent<Animator>();
        if (animator != null)
        {
            fightingSystem.animator = animator;
        }

        // FightingSystem��������
        fightingSystem.Initialize();

        return character;
    }

}