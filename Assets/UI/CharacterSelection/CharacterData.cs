using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    //キャラの名前
    public string characterName;
    //キャラの画像
    public Sprite characterImage;
    //キャラのプレハブ
    public GameObject characterPrefab;
    //キャラのHP
    public string characterHP;

}