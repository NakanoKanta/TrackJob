using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;        // ���O
    public Sprite characterImage;       // �G�iSprite�j
    public GameObject characterPrefab;  // �{�ԂŎg���L�����̃v���n�u
}
