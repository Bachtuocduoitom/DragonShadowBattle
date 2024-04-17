using AirFishLab.ScrollingList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{

    public Action<int, int> OnUnlockButtonClicked;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI powerNumber;
    [SerializeField] private TextMeshProUGUI goldNumber;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI commingSoonText;
    [SerializeField] private Image goldIcon;
    [SerializeField] private RectTransform activedStatus;
    [SerializeField] private RectTransform waitingStatus;
    [SerializeField] private Button unlockButton;

    [SerializeField] private RectTransform cardName;
    [SerializeField] private RectTransform cardMainInfo;
    [SerializeField] private RectTransform cardStatus;

    private int currentTransform;
    private int currentTransformCost;

    private void Awake()
    {
        
    }

    private void Start()
    {
        currentTransform = 0;
        currentTransformCost = 0;

        unlockButton.onClick.AddListener(() =>
        {
            OnUnlockButtonClicked?.Invoke(currentTransform, currentTransformCost);
        });

    }

    public void SetCardInfo(string name, TransformSO transformSO, bool isUnlocked)
    {
        if (DataManager.Instance.HasCharacter(name))
        {
            // Show the transform info
            ShowTransformInfo();

            // Update the card info
            nameText.text = name;
            levelNumber.text = transformSO.transformName.ToString();

            powerNumber.text = Util.GetCurrencyFormat(transformSO.power);

            if (transformSO.cost == 0)
            {
                goldNumber.text = "Free";
            }
            else
            {
                goldNumber.text = Util.GetCurrencyFormat(transformSO.cost);
            }

            // Show the unlock button if the transform is locked
            activedStatus.gameObject.SetActive(!isUnlocked);
            unlockButton.gameObject.SetActive(isUnlocked);

            currentTransform = transformSO.transformName;
            currentTransformCost = transformSO.cost;
        } else 
        {
            ShowCommingSoon();
            return;
        }
        
    }

    public void unlockCurrentTransform()
    {
        activedStatus.gameObject.SetActive(true);
        unlockButton.gameObject.SetActive(false);
    }

    private void ShowCommingSoon()
    {
        levelNumber.gameObject.SetActive(false);
        powerNumber.gameObject.SetActive(false);
        goldNumber.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        powerText.gameObject.SetActive(false);
        goldIcon.gameObject.SetActive(false);

        commingSoonText.gameObject.SetActive(true);
        waitingStatus.gameObject.SetActive(true);
    }

    private void ShowTransformInfo()
    {
        levelNumber.gameObject.SetActive(true);
        powerNumber.gameObject.SetActive(true);
        goldNumber.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        powerText.gameObject.SetActive(true);
        goldIcon.gameObject.SetActive(true);

        commingSoonText.gameObject.SetActive(false);
        waitingStatus.gameObject.SetActive(false);
    }

    public void PlayMoveInTween()
    {
        // Card name move in tween
        Vector3 cardNameLocalPos = cardName.gameObject.GetComponent<RectTransform>().localPosition;
        cardName.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(cardNameLocalPos.x + 350f, cardNameLocalPos.y, cardNameLocalPos.z);
        LeanTween.moveLocalX(cardName.gameObject, cardNameLocalPos.x, 0.3f)
            .setDelay(0.5f);

        // Card main info move in tween
        Vector3 cardMainInfoLocalPos = cardMainInfo.gameObject.GetComponent<RectTransform>().localPosition;
        cardMainInfo.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(cardMainInfoLocalPos.x + 400f, cardMainInfoLocalPos.y, cardMainInfoLocalPos.z);
        LeanTween.moveLocalX(cardMainInfo.gameObject, cardMainInfoLocalPos.x, 0.3f)
            .setDelay(0.7f);

        // Card status move in tween
        Vector3 cardStatusLocalPos = cardStatus.gameObject.GetComponent<RectTransform>().localPosition;
        cardStatus.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(cardStatusLocalPos.x + 350f, cardStatusLocalPos.y, cardStatusLocalPos.z);
        LeanTween.moveLocalX(cardStatus.gameObject, cardStatusLocalPos.x, 0.3f)
            .setDelay(0.9f);

    }
}
