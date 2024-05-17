using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSpawner;

public class GoldItemArray : MonoBehaviour
{

    [SerializeField] private StaticGold staticGold;

    private StaticGold[] staticGoldList;
    private int numberOfItemsToSpawn = 8;


    private void Start()
    {
        staticGoldList = new StaticGold[numberOfItemsToSpawn];
       
        StartCoroutine(SpawnItems());
    }

    private void Update()
    {
        if (staticGoldList.Length == 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnItems()
    {
        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            if (staticGoldList[i] == null)
            {
                staticGoldList[i] = Instantiate(staticGold, transform.position, Quaternion.identity);
                staticGoldList[i].transform.SetParent(transform);
                staticGoldList[i].SetItemType(ItemType.Gold);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
