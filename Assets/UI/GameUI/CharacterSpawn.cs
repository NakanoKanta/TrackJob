using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    int p1 = SelectDataManager.CurrentData.Data1Index;
    int p2 = SelectDataManager.CurrentData.Data2Index;

    [Header("キャラクターデータベース")]
    public CharacterDatabase characterDatabase;

    [Header("基本キャラクタープレハブ")]
    public GameObject baseCharacterPrefab; // 基本となるキャラクタープレハブ

    void Start()
    {
        GameObject player1 = null;
        GameObject player2 = null;

        // Player1のキャラクター生成
        if (characterDatabase != null && p1 < characterDatabase.CharacterCount)
        {
            CharacterData player1Data = characterDatabase.GetCharacter(p1);
            player1 = CreateCharacter(player1Data, new Vector2(-8, 0), PlayerID.Player1);
        }

        // Player2のキャラクター生成
        if (characterDatabase != null && p2 < characterDatabase.CharacterCount)
        {
            CharacterData player2Data = characterDatabase.GetCharacter(p2);
            player2 = CreateCharacter(player2Data, new Vector2(8, 0), PlayerID.Player2);
        }

        // FightingSystemを取得
        FightingSystem fs1 = player1?.GetComponent<FightingSystem>();
        FightingSystem fs2 = player2?.GetComponent<FightingSystem>();

        // InputManagerを探してイベント登録
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
            Debug.LogError("InputManagerがシーン上に見つかりませんでした！");
        }
    }


    GameObject CreateCharacter(CharacterData characterData, Vector2 position, PlayerID playerID)
    {
        // 基本プレハブを生成
        GameObject character = Instantiate(baseCharacterPrefab, position, Quaternion.identity);

        // PlayerControllerコンポーネントを取得
        PlayerController controller = character.GetComponent<PlayerController>();
        if (controller == null)
        {
            controller = character.AddComponent<PlayerController>();
        }

        // キャラクターデータを設定
        controller.SetCharacterData(characterData, playerID);

        // FightingSystemコンポーネントを取得・設定
        FightingSystem fightingSystem = character.GetComponent<FightingSystem>();
        if (fightingSystem == null)
        {
            fightingSystem = character.AddComponent<FightingSystem>();
        }

        // FightingSystemの初期設定
        fightingSystem.playerID = playerID;
        fightingSystem.characterDatabase = characterDatabase;

        // Animatorコンポーネントを取得して設定
        Animator animator = character.GetComponent<Animator>();
        if (animator != null)
        {
            fightingSystem.animator = animator;
        }

        // FightingSystemを初期化
        fightingSystem.Initialize();

        return character;
    }

}