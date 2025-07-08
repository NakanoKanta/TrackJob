using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Character/Database")]
public class CharacterDatabase : ScriptableObject
{
    public CharacterData[] characters;

    public CharacterData GetCharacter(int index)
    {
        //�L������ԍ��ŊǗ�����
        return characters[index];
    }

    public int CharacterCount => characters.Length;
}

