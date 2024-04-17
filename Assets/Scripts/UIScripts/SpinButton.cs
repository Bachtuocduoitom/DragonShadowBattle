using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI spinDefaultText;
    [SerializeField] private TextMeshProUGUI spinText;
    [SerializeField] Image adsIcon;

    public void SetAdsSpin()
    {
        spinDefaultText.gameObject.SetActive(false);
        spinText.gameObject.SetActive(true);
        adsIcon.gameObject.SetActive(true);
    }

    public void SetFreeSpin()
    {
        spinDefaultText.gameObject.SetActive(true);
        spinText.gameObject.SetActive(false);
        adsIcon.gameObject.SetActive(false);
    }
}
