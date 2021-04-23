using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestureUIInteractionInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Dropdown dropdown;

    [SerializeField]
    private Toggle[] toggles;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject colorGridContainer;

    private bool ignoreChangeNotification = false;

    void Start()
    {

    }

    public void NotifyAboutChange()
    {
        if (!ignoreChangeNotification)
        {
            FindObjectOfType<UIInteractionManager>().NotifyAboutUIModificationForLogging();
        }
    }

    public void TriggerFinish()
    {
        var finishSuccessful = FindObjectOfType<UIInteractionManager>().TriggerFinish();

        if (finishSuccessful)
        {
            ignoreChangeNotification = true;
            ResetUI();
            ignoreChangeNotification = false;
        }
    }

    public void ResetUI()
    {
        dropdown.value = 0;
        dropdown.RefreshShownValue();

        toggles[0].isOn = true;
        toggles[1].isOn = false;
        toggles[2].isOn = false;

        slider.value = 0.5f;

        var colorToggles = colorGridContainer.GetComponentsInChildren<Toggle>();

        foreach (var toggle in colorToggles)
        {
            toggle.isOn = false;
        }

        colorGridContainer.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
    }
}
