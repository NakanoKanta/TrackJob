using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    int p1 = SelectDataManager.CurrentData.Data1Index;
    int p2 = SelectDataManager.CurrentData.Data2Index;
    public GameObject hondou_prefab;
    public GameObject miyamoto_prefab;
    // Start is called before the first frame update
    void Start()
    {
        if (p1 == 1)
        {
            Instantiate(hondou_prefab, new Vector2(-8, 0), Quaternion.identity);
        }
        if (p1 == 0)
        {
            Instantiate(miyamoto_prefab, new Vector2(-8, 0), Quaternion.identity);
        }
        if (p2 == 1)
        {
            Instantiate(hondou_prefab, new Vector2(8, 0), Quaternion.identity);
        }
        if (p2 == 0)
        {
            Instantiate(miyamoto_prefab, new Vector2(8, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
