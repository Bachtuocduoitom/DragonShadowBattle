using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatisticBarUI : MonoBehaviour
{

    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image manaBarImage;
    [SerializeField] private Image transformImage;
    [SerializeField] private TextMeshProUGUI transformNameText;

    private Mana mana;
    private Health health;

    private void Awake()
    {
        mana = new Mana();
        health = new Health();
    }

    private void Start()
    {
        Player.Instance.OnChangeTransform += Player_OnChangeTransform;
        Player.Instance.OnFinishChangeTransform += Player_OnFinishChangeTransform;
        Player.Instance.OnHitDamage += Player_Damage;
        Player.Instance.OnTryUseSkill += Player_TryUseSkill;
        Player.Instance.OnUseBeanItem += Player_UseBeanItem;
        Player.Instance.OnCollectItem += Player_CollectItem;

        health.OnOutOfHealth += Health_OutOfHealth;
    }

    private void Player_OnChangeTransform(int currentTransform, float newMaxHealth, float newMaxMana)
    {
        // Update Health and Mana
        health.UpdateMaxHealth(newMaxHealth);
        mana.UpdateMaxMana(newMaxMana);

        // Update Transform Name Text
        transformNameText.text = $"SSJ. {currentTransform}";
    }

    private void Player_OnFinishChangeTransform(int currentTransform)
    {
        // Update transform image
        transformImage.sprite = DataManager.Instance.GetSpriteForCurrentTransform(currentTransform);
    }

    private void Player_Damage(float amount)
    {
        health.TakeDamage(amount);
    }

    private void Player_TryUseSkill(float manaCost)
    {
        if (mana.TrySpendAmount(manaCost))
        {
            Player.Instance.UseSkill();
        }
    }

    private void Player_UseBeanItem()
    {
        health.ResetHealth();
        mana.ResetMana();
    }

    private void Player_CollectItem(ItemSpawner.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemSpawner.ItemType.GreenBean:
                health.ResetHealth();
                mana.ResetMana();
                break;
            case ItemSpawner.ItemType.RedBean:
                health.ResetHealth();
                break;
            case ItemSpawner.ItemType.BlueBean:
                mana.ResetMana();
                break;
        }
    }

    private void Health_OutOfHealth()
    {
        Player.Instance.Die();
    }

    private void Update()
    {
        mana.Update();

        manaBarImage.fillAmount = mana.GetManaNormalized();
        healthBarImage.fillAmount = health.GetHealthNormalized();
    }

   
}

