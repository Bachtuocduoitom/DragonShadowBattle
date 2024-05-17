using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.Demo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class TransformScreen : MonoBehaviour, IScreen
{
    [SerializeField]
    private CircularScrollingList circularScrollingList;
    [SerializeField]
    private CardListBank cardListBank;
    [SerializeField]
    private CardTransform cardTransform;

    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private UnlockPopup unlockPopup;
    [SerializeField] private GoldAmountTouchable goldAmountTouchable;
    [SerializeField] private Image playerTransform;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private CharacterWheel characterWheel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button transparentLeftButton;
    [SerializeField] private Button transparentRightButton;


    private int _numOfBoxes = 7;
    private bool canRotateCharacterWheel = true;
    private bool canPlay = false;
    private float delayTime = 1f;


    private void Awake()
    {
        cardInfo.OnUnlockButtonClicked += CardInfo_OnUnlockButtonClicked;

        goldAmountTouchable.OnTouchGoldAmount += GoldAmountTouchable_OnTouchGoldAmount;

        playButton.onClick.AddListener(() =>
        {
            if (!canPlay) return;

            SceneController.Instance.LoadGameplayScene();

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        menuButton.onClick.AddListener(() =>
        {
            ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.MenuScreen);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        });

        transparentLeftButton.onClick.AddListener(() =>
        {
            if (canRotateCharacterWheel)
            {
                characterWheel.RotateLeft();
                canRotateCharacterWheel = false;

                LeanTween.delayedCall(delayTime - 0.2f, () =>
                {
                    UpdateCardInfoAndPlayerTransformImage(0);

                    // Update circular scrolling list
                    cardListBank.UpdateCardListBank();
                    circularScrollingList.Refresh(0);
                });

                LeanTween.delayedCall(delayTime, () =>
                {
                    canRotateCharacterWheel = true;
                    characterWheel.PlayMoveUpAndDownForCurrentTransform();
                });
            }
            
        });

        transparentRightButton.onClick.AddListener(() =>
        {         
            if (canRotateCharacterWheel)
            {
                characterWheel.RotateRight();
                canRotateCharacterWheel = false;

                LeanTween.delayedCall(delayTime - 0.2f, () =>
                {
                    UpdateCardInfoAndPlayerTransformImage(0);

                    // Update circular scrolling list
                    cardListBank.UpdateCardListBank();
                    circularScrollingList.Refresh(0);
                });

                LeanTween.delayedCall(delayTime, () =>
                {
                    canRotateCharacterWheel = true;
                    characterWheel.PlayMoveUpAndDownForCurrentTransform();
                });
            }
        });
    }

    private void Start()
    {
        // Initialize the circular scrolling list
        InitializeTheList();

        goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());
        unlockPopup.Hide();
    }

    private void CardInfo_OnUnlockButtonClicked(int currentTransform, int cost)
    {
        if (!DataManager.Instance.IsTransformLocked(currentTransform - 1))
        {
            // Unlock the transform successfully
            if (DataManager.Instance.TryDecreaseGoldAmount(cost))
            {
                DataManager.Instance.UnlockTransform(currentTransform);

                // Update card info
                cardInfo.unlockCurrentTransform();

                // Update unlock popup
                unlockPopup.Show();
                unlockPopup.SetUnlockText(true, currentTransform);

                // Update gold amount
                goldText.text = Util.GetCurrencyFormat(DataManager.Instance.GetGoldAmount());

                // Update the list
                CardTransform cardTransform = (CardTransform)circularScrollingList.GetFocusingBox();
                cardTransform.UnlockTransform();

                AudioManager.Instance.PlaySFX(AudioManager.Instance.cheer);
            } else
            {
                // Not enough gold
                unlockPopup.SetUnlockText(false, currentTransform);
            }
        } else
        {
            // Already unlocked
            unlockPopup.SetUnlockText(false, currentTransform);
        }

    }

    private void GoldAmountTouchable_OnTouchGoldAmount()
    {
        ScreenController.Instance.ShowScreenWithTransition(ScreenController.ScreenType.CoinsScreen);
    }

    public void InitializeTheList()
    {
        circularScrollingList.SetListBank(cardListBank);

        var boxSetting = circularScrollingList.BoxSetting;
        boxSetting.SetBoxPrefab(cardTransform);
        boxSetting.SetNumOfBoxes(_numOfBoxes);

        var listSetting = circularScrollingList.ListSetting;
        listSetting.SetAlignAtFocusingPosition(true);
        listSetting.SetFocusSelectedBox(true);
        listSetting.AddOnBoxSelectedCallback(OnBoxSelected);
        listSetting.AddOnFocusingBoxChangedCallback(OnFocusingBoxChanged);

        circularScrollingList.Initialize();
    }

    private void OnBoxSelected(ListBox box)
    {
        Debug.Log("Selected box: " + ((CardTransform)box).Content);
    }

    public void OnFocusingBoxChanged(ListBox preFocusesBox, ListBox newFocusedBox)
    {
        CardTransform cardTransform = (CardTransform) newFocusedBox;
        UpdateCardInfoAndPlayerTransformImage(cardTransform.Content);

    }

    private void UpdateCardInfoAndPlayerTransformImage(int currentTransform)
    {
        string currentCharacter = DataManager.Instance.GetCurrentCharacter();
        TransformSO transformSO = DataManager.Instance.GetTransformSO(currentTransform);
        bool isUnlocked = DataManager.Instance.IsTransformLocked(currentTransform);

        cardInfo.SetCardInfo(currentCharacter, transformSO, isUnlocked);
        
        // Update Character transform image
        characterWheel.UpdateCurrentCharacterImage(transformSO.transformSprite);   
    }

    public void Show()
    {
        gameObject.SetActive(true);

        UpdateCardInfoAndPlayerTransformImage(0);      

        // Animation MoveIn
        PlayAnimationMoveIn();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void PlayAnimationMoveIn()
    {
        // Character wheel Zoom in
        characterWheel.PlayMoveInTween();

        // Circular scrolling list move in
        Vector3 circularScrollLocalPos = circularScrollingList.gameObject.GetComponent<RectTransform>().localPosition;
        circularScrollingList.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(circularScrollLocalPos.x - 350f, circularScrollLocalPos.y, circularScrollLocalPos.z);
        LeanTween.moveLocalX(circularScrollingList.gameObject, circularScrollLocalPos.x, 0.3f)
            .setDelay(0.5f)
            .setOnStart(() =>
            {

                // Update circular scrolling list
                cardListBank.UpdateCardListBank();
                circularScrollingList.Refresh(0);
            });
            

        // Card info move in
        cardInfo.PlayMoveInTween();

        // Active play button
        canPlay = false;
        LeanTween.delayedCall(1.2f, () =>
        {
            canPlay = true;
        });
    }

    public bool IsShowed()
    {
        return gameObject.activeSelf;
    }
}
