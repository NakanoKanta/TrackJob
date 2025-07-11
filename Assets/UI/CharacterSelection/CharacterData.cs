using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    //�L�����̖��O
    public string characterName;
    //�L�����̉摜
    public Sprite characterImage;
    //�L�����̃v���n�u
    public GameObject characterPrefab;
    //�L������HP
    public string characterHP;

}