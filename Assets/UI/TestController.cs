using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField]
    private CharacterDatabase _characterDatabase;

    void Start()
    {
        SelectDataManager.SelectData data = SelectDataManager.CurrentData;

        CharacterData[] characters = _characterDatabase.characters;

        Debug.Log($"{characters[data.Data1Index].name} {characters[data.Data2Index].name}");
    }
}
