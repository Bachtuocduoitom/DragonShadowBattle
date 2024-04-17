using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    //Create Instance
    public static ItemSpawner Instance { get; private set; }

    public enum ItemType
    {
        GreenBean,
        RedBean,
        BlueBean,
        Armor,
        Gold
    }

    [SerializeField] private Item greenBean;
    [SerializeField] private Item redBean;
    [SerializeField] private Item blueBean;
    [SerializeField] private Item armor;
    [SerializeField] private Item gold;

    private float waitingToSpawnTimer;
    private float waitingToSpawnTimerMax = 12f;
    private int numberOfItemsToSpawn;
    private int currentSpawnItem = 6;

    private const float xSpawnPosition = 12f;
    private float ySpawnPosition;
    private float ySpawnPositionMin = -4f;
    private float ySpawnPositionMax = 4f;

    private bool canSpawn = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one ItemSpawner Instance");
        }
        Instance = this;
    }

    private void Start()
    {
        waitingToSpawnTimer = waitingToSpawnTimerMax;
    }

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        waitingToSpawnTimer -= Time.deltaTime;
        if (waitingToSpawnTimer <= 0)
        {
            if (numberOfItemsToSpawn == 0)
            {
                float random = Random.Range(0f, 1f);
                if (random < 0.2f)
                {
                    numberOfItemsToSpawn = 1;
                }
                else if (random < 0.6f)
                {
                    numberOfItemsToSpawn = 2;
                } else
                {
                    numberOfItemsToSpawn = 3;
                }
                Debug.Log("Number of items to spawn: " + numberOfItemsToSpawn);
            }

            SpawnItem();
            numberOfItemsToSpawn--;

            if (numberOfItemsToSpawn == 0)
            {
                waitingToSpawnTimer = waitingToSpawnTimerMax;
                currentSpawnItem = 6;
            }
            else
            {
                waitingToSpawnTimer = Random.Range(0.5f, 3f);
            }

        }
    }

    private void SpawnItem()
    {
        int randomItem = Random.Range(0, 4);
        if (randomItem == currentSpawnItem)
        {
            randomItem = (randomItem + 1) % 5;
        }

        Vector2 spawnPosition = new Vector2(xSpawnPosition, Random.Range(ySpawnPositionMin, ySpawnPositionMax));

        Item item;
        currentSpawnItem = randomItem;
        switch (randomItem)
        {
            case 0:
                item = Instantiate(greenBean, spawnPosition, Quaternion.identity);
                item.SetItemType(ItemType.GreenBean);
                break;
            case 1:
                item = Instantiate(redBean, spawnPosition, Quaternion.identity);
                item.SetItemType(ItemType.RedBean);
                break;
            case 2:
                item = Instantiate(blueBean, spawnPosition, Quaternion.identity);
                item.SetItemType(ItemType.BlueBean);
                break;
            case 3:
                item = Instantiate(armor, spawnPosition, Quaternion.identity);
                item.SetItemType(ItemType.Armor);
                break;
            case 4:
                item = Instantiate(gold, spawnPosition, Quaternion.identity);
                item.SetItemType(ItemType.Gold);
                break;
        }
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
