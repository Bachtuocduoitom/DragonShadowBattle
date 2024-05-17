using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }


    #region Key Constants
    private const string GOLD_AMOUNT = "GOLD_AMOUNT";
    private const string BEAN_AMOUNT = "BEAN_AMOUNT";
    private const string CURRENT_CHARACTER = "CURRENT_CHARACTER";
    private const string UNLOCKED_GOKU_TRANSFORM_COUNT = "UNLOCKED_GOKU_TRANSFORM_COUNT";
    private const string UNLOCKED_VEGETA_TRANSFORM_COUNT = "UNLOCKED_VEGETA_TRANSFORM_COUNT";
    private const string UNLOCKED_TRUNK_TRANSFORM_COUNT = "UNLOCKED_TRUNK_TRANSFORM_COUNT";
    private const string UNLOCKED_GOHAN_TRANSFORM_COUNT = "UNLOCKED_GOHAN_TRANSFORM_COUNT";
    private const string CURRENT_LEVEL = "CURRENT_LEVEL";
    private const string PREVIOUS_LEVEL = "PREVIOUS_LEVEL";
    private const string LIKED_PAGE = "LIKED_PAGE";
    private const string WATCHED_VIDEO = "WATCHED_VIDEO";
    #endregion

    // Player Constant Values
    public const string GOKU_CHARACTER = "GOKUZ";
    public const string VEGETA_CHARACTER = "VEZETA";
    public const string TRUNK_CHARACTER = "TRUNZ";
    public const string GOHAN_CHARACTER = "ZOHAN";
    public const string VEZITO_CHARACTER = "VEZITO";
    public static readonly string[] CHARACTERS = { GOKU_CHARACTER, VEGETA_CHARACTER, TRUNK_CHARACTER, VEZITO_CHARACTER, GOHAN_CHARACTER };

    // Enemy Constant Values
    public static string PICOLO_ENEMY = "PICOLO_ENEMY";
    public const string FREEZER_ENEMY = "FREEZER_ENEMY";
    public const string CELL_ENEMY = "CELL_ENEMY";
    public const string BUU_ENEMY = "BUU_ENEMY";
    public const string TAUPAIPAI_ENEMY = "TAUPAIPAI_ENEMY";
    public static readonly List<string> ENEMIES = new() { TAUPAIPAI_ENEMY, PICOLO_ENEMY };

    // Game Instance Values
    private int goldAmount = 96969696;
    private int beanAmount = 10;
    private string currentCharacter = GOKU_CHARACTER;
    private string preCharacter = GOKU_CHARACTER;
    private List<int> unlockedGokuTransform = new() { 0, 1, 2, 3, 4, 5, 6, 7};
    private List<int> unlockedVegetaTransform = new() { 0, 1 };
    private List<int> unlockedTrunkTransform = new() { 0, 1 };
    private List<int> unlockedGohanTransform = new() { 0, 1 };
    private List<int> unlockedVezitoTransform = new() { 0, 1 };
    private int currentLevel = 1;
    private int preLevel = 0;
    private bool likedPage = false;
    private bool watchedVideo = false;

    // Player List Transform SO
    [SerializeField] private List<TransformSO> gokuTransformSOList;
    [SerializeField] private List<TransformSO> vegetaTransformSOList;
    [SerializeField] private List<TransformSO> gohanTransformSOList;
    [SerializeField] private List<TransformSO> trunkTransformSOList;
    [SerializeField] private List<TransformSO> vezitoTransformSOList;
    [SerializeField] private List<PlayerSkillSO> playerSkillSOList;
    [SerializeField] private List<CharacterSkillsSO> characterSkillsSOList;

    // Enemy List Transform SO
    [SerializeField] private List<EnemySO> enemySOList;
    [SerializeField] private List<Transform> enemyPrefabList;

    // Level Data
    [SerializeField] private List<LevelData> levelDatas;

    // Data in one level
    private int highestTransformIndex = 0;
    private int earnCoin = 0;
    private int currentEnemyIndex = 0;

    // Music and Sound, can modify here to turn on or off music and sound
    private bool isMusicOn = false;
    private bool isSoundOn = true;

    // Show rate popup after 5 times play
    private int countPlay = 0;  
    private const int timeShowRatePopup = 5;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        LoadDatas();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    // Gold amount
    public int GetGoldAmount()
    {
        return goldAmount;
    }
    public void IncreaseGoldAmount(int amount)
    {
        goldAmount += amount;

        Save();
    }
    public void DecreaseGoldAmount(int amount)
    {
        goldAmount -= amount;

        Save();
    }
    public bool TryDecreaseGoldAmount(int amount)
    {
        if (goldAmount < amount)
        {
            return false;
        }

        goldAmount -= amount;
        Save();
        return true;
    }

    // Bean amount
    public int GetBeanAmount()
    {
        return beanAmount;
    }
    public void IncreaseBeanAmount(int amount)
    {
        beanAmount += amount;

        Save();
    }
    public bool TryDecreaseBeanAmount(int amount)
    {
        if (beanAmount < amount)
        {
            return false;
        } 

        beanAmount -= amount;
        Save();
        return true;

    }

    // Character
    public List<int> GetUnLockedTransforms()
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return unlockedGokuTransform;
            case VEGETA_CHARACTER:
                return unlockedVegetaTransform;
            case TRUNK_CHARACTER:
                return unlockedTrunkTransform;
            case GOHAN_CHARACTER:
                return unlockedGohanTransform;
            case VEZITO_CHARACTER:
                return unlockedVezitoTransform;
            default:
                return null;
        }
    }
    public string GetCurrentCharacter()
    {       
        return currentCharacter;
    }
    public CharacterSkillsSO GetCurrentCharacterSkillsPrefabs()
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return characterSkillsSOList[0];
            case VEGETA_CHARACTER:
                return characterSkillsSOList[1];
            case TRUNK_CHARACTER:
                return characterSkillsSOList[2];
            case GOHAN_CHARACTER:
                return characterSkillsSOList[3];
            default:
                return null;
        }
    }
    public string GetCharacterByIndex(int index)
    {
        int currentCharacterIndex = System.Array.IndexOf(CHARACTERS, currentCharacter);
        return CHARACTERS[(currentCharacterIndex + index) % CHARACTERS.Length];
    }
    public void SetCurrentCharacter(string character)
    {
        if (character == VEZITO_CHARACTER)
        {
            preCharacter = currentCharacter;
            currentCharacter = character;
        } else
        {
            currentCharacter = character;
            Save();
        }
    }
    public bool HasCharacter(string characterName)
    {
        if (characterName != VEZITO_CHARACTER)
        {
            return true;
        }
        return false;
    }
    public void ResetCurrentCharacter()
    {
        if (currentCharacter == VEZITO_CHARACTER)
        {
            currentCharacter = preCharacter;
        }
    }
    public Sprite GetSpriteForCharacter(string characterName)
    {
        switch (characterName)
        {
            case GOKU_CHARACTER:
                return gokuTransformSOList[0].transformSprite;
            case VEGETA_CHARACTER:
                return vegetaTransformSOList[0].transformSprite;
            case TRUNK_CHARACTER:
                return trunkTransformSOList[0].transformSprite;
            case GOHAN_CHARACTER:
                return gohanTransformSOList[0].transformSprite;
            case VEZITO_CHARACTER:
                return vezitoTransformSOList[0].transformSprite;
            default:
                return null;
        }
    }

    // Level
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public int GetPreLevel()
    {
        return preLevel;
    }
    public void UpdataCurrentLevelAndPreLevelOnVictory()
    {
        if (currentLevel < levelDatas.Count)
        {
            preLevel = currentLevel;
            currentLevel++;
        } 

        Save();
    }
    public void UpdateCurrentLevelAndPreLevelOnGameOver()
    {
        preLevel = currentLevel;

        if (currentLevel - 2 >= 1)
        {
            currentLevel -= 2;
        } else
        {
            currentLevel = 1;
        }

        Save();
    }

    // Enemy
    public int GetNumberOfLevel()
    {
        return levelDatas.Count;
    }
    public List<Sprite> GetLevelEnemySpriteList(int level)
    {
        return levelDatas[level - 1].enemySprites;
    }
    public List<Sprite> GetEnemySpritesForCurrentLevel()
    {
        return levelDatas[currentLevel - 1].enemySprites;
    }
    public Transform GetEnemyPrefabForCurrentLevel()
    {
        return levelDatas[currentLevel - 1].enemies[currentEnemyIndex];
    }
    public bool HasNextEnemyPrefabForCurrentLevel()
    {
        currentEnemyIndex++;
        if (currentEnemyIndex < levelDatas[currentLevel - 1].enemies.Count)
        {
            return true;
        }
        return false;
    }
    public List<EnemySO> GetEnemySOList()
    {
        return enemySOList;
    }
    public EnemySO GetCurrentEnemySO()
    {
        return enemySOList[currentLevel - 1];
    }
    public Sprite GetSpriteForCurrentEnemy()
    {
        if (enemySOList.Count >= currentLevel)
        {
            return enemySOList[currentLevel - 1].sprite;
        }
        return null;
    }
    public Transform GetPrefabForCurrentEnemy()
    {
        if (enemyPrefabList.Count >= currentLevel)
        {
            return enemyPrefabList[currentLevel - 1];
        }
        return null;
    }

    // Transform
    public bool HasNextTransform(int currentTransform)
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return unlockedGokuTransform.Contains(currentTransform + 1);
            case VEGETA_CHARACTER:
                return unlockedVegetaTransform.Contains(currentTransform + 1);
            case TRUNK_CHARACTER:
                return unlockedTrunkTransform.Contains(currentTransform + 1);
            case GOHAN_CHARACTER:
                return unlockedGohanTransform.Contains(currentTransform + 1);
            case VEZITO_CHARACTER:
                return unlockedVezitoTransform.Contains(currentTransform + 1);
            default:
                return false;
        }
    }
    public bool IsTransformLocked(int transformIndex)
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return !unlockedGokuTransform.Contains(transformIndex);
            case VEGETA_CHARACTER:
                return !unlockedVegetaTransform.Contains(transformIndex);
            case TRUNK_CHARACTER:
                return !unlockedTrunkTransform.Contains(transformIndex);
            case GOHAN_CHARACTER:
                return !unlockedGohanTransform.Contains(transformIndex);
            case VEZITO_CHARACTER:
                return !unlockedVezitoTransform.Contains(transformIndex);
            default:
                return false;
        }
    }
    public void UnlockTransform(int transformIndex)
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                unlockedGokuTransform.Add(transformIndex);
                break;
            case VEGETA_CHARACTER:
                unlockedVegetaTransform.Add(transformIndex);
                break;
            case TRUNK_CHARACTER:
                unlockedTrunkTransform.Add(transformIndex);
                break;
            case GOHAN_CHARACTER:
                unlockedGohanTransform.Add(transformIndex);
                break;
        }

        Save();
    }
    public void IncreaseTransformsForCurrentCharracter(int numberTransformToUnlock)
    {
        for (int i = 0; i < numberTransformToUnlock; i++)
        {
            switch (currentCharacter)
            {
                case GOKU_CHARACTER:
                    if (unlockedGokuTransform.Count >= gokuTransformSOList.Count)
                    {
                        return;
                    }
                    UnlockTransform(unlockedGokuTransform.Count);
                    break;
                case VEGETA_CHARACTER:
                    if (unlockedVegetaTransform.Count >= vegetaTransformSOList.Count)
                    {
                        return;
                    }
                    UnlockTransform(unlockedVegetaTransform.Count);
                    break;
                case TRUNK_CHARACTER:
                    if (unlockedTrunkTransform.Count >= trunkTransformSOList.Count)
                    {
                        return;
                    }
                    UnlockTransform(unlockedTrunkTransform.Count);
                    break;
                case GOHAN_CHARACTER:
                    if (unlockedGohanTransform.Count >= gohanTransformSOList.Count)
                    {
                        return;
                    }
                    UnlockTransform(unlockedGohanTransform.Count);
                    break;
            }
        }
    }
    public TransformSO GetTransformSO(int transformIndex)
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return gokuTransformSOList[transformIndex];
            case VEGETA_CHARACTER:
                return vegetaTransformSOList[transformIndex];
            case TRUNK_CHARACTER:
                return trunkTransformSOList[transformIndex];
            case GOHAN_CHARACTER:
                return gohanTransformSOList[transformIndex];
            case VEZITO_CHARACTER:
                return vezitoTransformSOList[transformIndex];
            default:
                return null;
        }
    }
    public Sprite GetSpriteForCurrentTransform(int currentTransform)
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return gokuTransformSOList[currentTransform].transformSprite;
            case VEGETA_CHARACTER:
                return vegetaTransformSOList[currentTransform].transformSprite;
            case TRUNK_CHARACTER:
                return trunkTransformSOList[currentTransform].transformSprite;
            case GOHAN_CHARACTER:
                return gohanTransformSOList[currentTransform].transformSprite;
            case VEZITO_CHARACTER:
                return vezitoTransformSOList[currentTransform].transformSprite;
            default:
                return null;
        }
    }
    public float GetMaxHealthForCurrentTransform(int currentTransform)
    {
        if (gokuTransformSOList.Count >= currentTransform)
        {
            return gokuTransformSOList[currentTransform].maxHealth;
        }
        return 0;
    }
    public float GetMaxManaForCurrentTransform(int currentTransform)
    {
        if (gokuTransformSOList.Count >= currentTransform)
        {
            return gokuTransformSOList[currentTransform].maxMana;
        }
        return 0;
    }
    public float GetPowerScaleForCurrentTransform(int currentTransform)
    {
        if (gokuTransformSOList.Count >= currentTransform)
        {
            return gokuTransformSOList[currentTransform].powerScale;
        }
        return 0;
    }
    public float GetManaCostForSkill(Player.PlayerSkill skill)
    {
        switch (skill)
        {
            case Player.PlayerSkill.KamehaSkill:
                return playerSkillSOList[0].manaCost;
            case Player.PlayerSkill.SpamSKill:
                return playerSkillSOList[1].manaCost;
            case Player.PlayerSkill.DonSkill:
                return playerSkillSOList[2].manaCost;
            case Player.PlayerSkill.SpiritBoomSkill:
                return playerSkillSOList[3].manaCost;
            case Player.PlayerSkill.DragonSkill:
                return playerSkillSOList[4].manaCost;

        }
        return 0;
    }
    public List<TransformSO> GetCurrentTransformSOList()
    {
        switch (currentCharacter)
        {
            case GOKU_CHARACTER:
                return gokuTransformSOList;
            case VEGETA_CHARACTER:
                return vegetaTransformSOList;
            case TRUNK_CHARACTER:
                return trunkTransformSOList;
            case GOHAN_CHARACTER:
                return gohanTransformSOList;
            case VEZITO_CHARACTER:
                return vezitoTransformSOList;
            default:
                return null;
        }
    }

    // Get data in one level
    public Sprite GetHighestTransform()
    {
        return GetSpriteForCurrentTransform(highestTransformIndex);
    }
    public void SetHighestTransform(int transformIndex)
    {
        highestTransformIndex = transformIndex;
    }
    public int GetLevelBonus()
    {
        return preLevel * 20;
    }
    public int GetEarnCoin()
    {
        return earnCoin;
    }
    public void AddEarnCoin()
    {
        earnCoin += 20;
    }
    public int GetTotalScore()
    {
        return GetLevelBonus() + GetEarnCoin();
    }
    public int GetCurrentEnemyIndex()
    {
        return currentEnemyIndex;
    }
    public void IncreaseCurrentEnemyIndex()
    {
        currentEnemyIndex++;
    }
    public void ResetDataLevel()
    {
        highestTransformIndex = 0;
        earnCoin = 0;
        currentEnemyIndex = 0;

        // Increase count play
        countPlay++;
    }
    public bool CanShowRatePopup()
    {
        return (countPlay % timeShowRatePopup) == 0;
    }

    #region Music and Sound
    public bool IsMusicOn()
    {
        return isMusicOn;
    }
    public bool IsSoundOn()
    {
        return isSoundOn;
    }
    public void TurnOnMusic()
    {
        isMusicOn = true;
    }
    public void TurnOffMusic()
    {
        isMusicOn = false;
    }
    public void TurnOnSound()
    {
        isSoundOn = true;
    }
    public void TurnOffSound()
    {
        isSoundOn = false;
    }
    #endregion

    #region Like and Watched Video
    public bool IsLikedPage()
    {
        return likedPage;
    }
    public void SetLikedPage()
    {
        likedPage = true;
    }
    public bool IsWatchedVideo()
    {
        return watchedVideo;
    }
    public void SetWatchedVideo()
    {
        watchedVideo = true;
    }
    #endregion

    #region Save Methods
    public void SaveUnlockedGokuTransformList()
    {
        /*
        for (int i = 0; i < unlockedGokuTransform.Count; i++)
        {
            PlayerPrefs.SetInt(UNLOCKED_GOKU_TRANSFORM_COUNT + i, unlockedGokuTransform[i]);
        }
        */
        PlayerPrefs.SetInt(UNLOCKED_GOKU_TRANSFORM_COUNT, unlockedGokuTransform.Count);
    }
    public void SaveUnlockedVegetaTransformList()
    {
        /*
        for (int i = 0; i < unlockedVegetaTransform.Count; i++)
        {
            PlayerPrefs.SetInt(UNLOCKED_VEGETA_TRANSFORM_COUNT + i, unlockedVegetaTransform[i]);
        }
        */
        PlayerPrefs.SetInt(UNLOCKED_VEGETA_TRANSFORM_COUNT, unlockedVegetaTransform.Count);
    }
    public void SaveUnlockedTrunkTransformList()
    {
        /*
        for (int i = 0; i < unlockedTrunkTransform.Count; i++)
        {
            PlayerPrefs.SetInt(UNLOCKED_TRUNK_TRANSFORM_COUNT + i, unlockedTrunkTransform[i]);
        }
        */
        PlayerPrefs.SetInt(UNLOCKED_TRUNK_TRANSFORM_COUNT, unlockedTrunkTransform.Count);
    }
    public void SaveUnlockedGohanTransformList()
    {
        /*
        for (int i = 0; i < unlockedGohanTransform.Count; i++)
        {
            PlayerPrefs.SetInt(UNLOCKED_GOHAN_TRANSFORM_COUNT + i, unlockedGohanTransform[i]);
        }
        */
        PlayerPrefs.SetInt(UNLOCKED_GOHAN_TRANSFORM_COUNT, unlockedGohanTransform.Count);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(GOLD_AMOUNT, goldAmount);
        PlayerPrefs.SetInt(BEAN_AMOUNT, beanAmount);
        PlayerPrefs.SetString(CURRENT_CHARACTER, currentCharacter);
        SaveUnlockedGokuTransformList();
        SaveUnlockedVegetaTransformList();
        SaveUnlockedTrunkTransformList();
        SaveUnlockedGohanTransformList();
        PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
        PlayerPrefs.SetInt(PREVIOUS_LEVEL, preLevel);
    }
    #endregion

    #region Load Methods
    public void LoadUnlockedGokuTransformList()
    {
        unlockedGokuTransform.Clear();
        int count = PlayerPrefs.GetInt(UNLOCKED_GOKU_TRANSFORM_COUNT);
        for (int i = 0; i < count; i++)
        {
            unlockedGokuTransform.Add(i);
        }
    }
    public void LoadUnlockedVegetaTransformList()
    {
        unlockedVegetaTransform.Clear();
        int count = PlayerPrefs.GetInt(UNLOCKED_VEGETA_TRANSFORM_COUNT);
        for (int i = 0; i < count; i++)
        {
            unlockedVegetaTransform.Add(i);
        }
    }
    public void LoadUnlockedTrunkTransformList()
    {
        unlockedTrunkTransform.Clear();
        int count = PlayerPrefs.GetInt(UNLOCKED_TRUNK_TRANSFORM_COUNT);
        for (int i = 0; i < count; i++)
        {
            unlockedTrunkTransform.Add(i);
        }
    }
    public void LoadUnlockedGohanTransformList()
    {
        unlockedGohanTransform.Clear();
        int count = PlayerPrefs.GetInt(UNLOCKED_GOHAN_TRANSFORM_COUNT);
        for (int i = 0; i < count; i++)
        {
            unlockedGohanTransform.Add(i);
        }
    }

    private void LoadDatas()
    {
        if (PlayerPrefs.HasKey(GOLD_AMOUNT))
        {
            goldAmount = PlayerPrefs.GetInt(GOLD_AMOUNT);
        }
        else
        {
            PlayerPrefs.SetInt(GOLD_AMOUNT, goldAmount);
        }

        if (PlayerPrefs.HasKey(BEAN_AMOUNT))
        {
            beanAmount = PlayerPrefs.GetInt(BEAN_AMOUNT);
        }
        else
        {
            PlayerPrefs.SetInt(BEAN_AMOUNT, beanAmount);
        }

        if (PlayerPrefs.HasKey(CURRENT_CHARACTER))
        {
            currentCharacter = PlayerPrefs.GetString(CURRENT_CHARACTER);
        }
        else
        {
            PlayerPrefs.SetString(CURRENT_CHARACTER, currentCharacter);
        }

        if (PlayerPrefs.HasKey(UNLOCKED_GOKU_TRANSFORM_COUNT))
        {
            LoadUnlockedGokuTransformList();
        } else
        {
            SaveUnlockedGokuTransformList();
        }

        if (PlayerPrefs.HasKey(UNLOCKED_VEGETA_TRANSFORM_COUNT))
        {
            LoadUnlockedVegetaTransformList();
        } else
        {
            SaveUnlockedVegetaTransformList();
        }

        if (PlayerPrefs.HasKey(UNLOCKED_TRUNK_TRANSFORM_COUNT))
        {
            LoadUnlockedTrunkTransformList();
        }
        else
        {
            SaveUnlockedTrunkTransformList();
        }

        if (PlayerPrefs.HasKey(UNLOCKED_GOHAN_TRANSFORM_COUNT))
        {
            LoadUnlockedGohanTransformList();
        }
        else
        {
            SaveUnlockedGohanTransformList();
        }

        if (PlayerPrefs.HasKey(CURRENT_LEVEL))
        {
            currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL);
        }
        else
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
        }

        if (PlayerPrefs.HasKey(PREVIOUS_LEVEL))
        {
            preLevel = PlayerPrefs.GetInt(PREVIOUS_LEVEL);
        }
        else
        {
            PlayerPrefs.SetInt(PREVIOUS_LEVEL, preLevel);
        }
    }
    #endregion
}
