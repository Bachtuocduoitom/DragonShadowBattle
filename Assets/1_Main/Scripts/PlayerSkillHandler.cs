using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillHandler : MonoBehaviour
{

    [SerializeField] private Button skillSpamButton;
    [SerializeField] private Button skillKamehaButton;
    [SerializeField] private Button skillDonButton;
    [SerializeField] private Button skillSpiritBoomButton;
    [SerializeField] private Button skillDragonButton;
    [SerializeField] private Button transformButton;

    private Image skillSpiritBoomButtonImage;
    private Image skillDragonButtonImage;
    private Image transformButtonImage;

    private const int LEVEL_TO_USE_SPIRIT_BOOM = 6;
    private const int LEVEL_TO_USE_DRAGON = 3;

    private bool canTransform = true;
    private bool canUseSkillSpiritBoom = false;
    private bool canUseSkillDragon = false;

    private void Awake()
    {
        skillSpamButton.onClick.AddListener(() =>
        {
            Player.Instance.TryUseSkill(Player.PlayerSkill.SpamSKill);
        });

        skillKamehaButton.onClick.AddListener(() =>
        {
            Player.Instance.TryUseSkill(Player.PlayerSkill.KamehaSkill);
        });

        skillDonButton.onClick.AddListener(() =>
        {
            Player.Instance.TryUseSkill(Player.PlayerSkill.DonSkill);
        });

        skillSpiritBoomButton.onClick.AddListener(() =>
        {
            if (canUseSkillSpiritBoom)
            {
                Player.Instance.TryUseSkill(Player.PlayerSkill.SpiritBoomSkill);
            }
        });

        skillDragonButton.onClick.AddListener(() =>
        {
            if (canUseSkillDragon)
            {
                Player.Instance.TryUseSkill(Player.PlayerSkill.DragonSkill);
            }
        });

        transformButton.onClick.AddListener(() =>
        {
            TransformButtonOnClick();
        });

        skillSpiritBoomButtonImage = skillSpiritBoomButton.GetComponent<Image>();
        skillDragonButtonImage = skillDragonButton.GetComponent<Image>();
        transformButtonImage = transformButton.GetComponent<Image>();

    }

    // TODO: Control Spirit Boom and Dragon Button
    private void Start()
    {
        skillSpiritBoomButtonImage.color = new Color(skillSpiritBoomButtonImage.color.r, skillSpiritBoomButtonImage.color.g, skillSpiritBoomButtonImage.color.b, 0.2f);
        skillDragonButtonImage.color = new Color(skillDragonButtonImage.color.r, skillDragonButtonImage.color.g, skillDragonButtonImage.color.b, 0.2f);
    }

    private void TransformButtonOnClick()
    {
        if (canTransform)
        {
            Player.Instance.TransformNextLevel();

            // Check can transform
            if (!DataManager.Instance.HasNextTransform(Player.Instance.GetCurrentSkin()))
            {
                transformButtonImage.color = new Color(transformButtonImage.color.r, transformButtonImage.color.g, transformButtonImage.color.b, 0.2f);
                canTransform = false;
            }


            // Check can use Spirit Boom
            if (Player.Instance.GetCurrentSkin() == LEVEL_TO_USE_SPIRIT_BOOM)
            {
                skillSpiritBoomButtonImage.color = new Color(skillSpiritBoomButtonImage.color.r, skillSpiritBoomButtonImage.color.g, skillSpiritBoomButtonImage.color.b, 1f);
                canUseSkillSpiritBoom = true;
            }

            // Check can use Dragon
            if (Player.Instance.GetCurrentSkin() == LEVEL_TO_USE_DRAGON)
            {
                skillDragonButtonImage.color = new Color(skillDragonButtonImage.color.r, skillDragonButtonImage.color.g, skillDragonButtonImage.color.b, 1f);
                canUseSkillDragon = true;
            }
        }
        
    }
}
