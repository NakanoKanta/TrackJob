using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;        // 名前
    public Sprite characterImage;       // 絵（Sprite）
    public GameObject characterPrefab;  // 本番で使うキャラのプレハブ
}
