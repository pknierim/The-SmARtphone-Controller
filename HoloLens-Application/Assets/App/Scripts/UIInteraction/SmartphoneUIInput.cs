using Google.Protobuf.HoloLensAndroidMessaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneUIInput : MonoBehaviour
{
    [SerializeField]
    private ModelChanger modelChanger;

    [SerializeField]
    private Color unselectedColor;

    [SerializeField]
    private Color hoverColor;

    [SerializeField]
    private Color selectedColor;

    [Header("Navigation")]
    [SerializeField]
    private float verticalThresholdNavigation;

    [SerializeField]
    private float horizontalThresholdNavigation;

    [SerializeField]
    private Scrollbar scrollBar;

    [SerializeField]
    private Transform[] elementsToNavigate;

    [Header("UI")]
    [SerializeField]
    private TMP_Dropdown dropdown;

    [SerializeField]
    private Toggle[] toggles;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject colorGridContainer;

    private float currentVerticalValue;
    private float currentHorizontalValue;

    private int currentElement = 0;

    private bool isInSelectedMode = false;

    void Start()
    {
        currentElement = 0;
        HoverNewElement(currentElement);
    }

    private void Update()
    {
#if UNITY_EDITOR
        var mani = new UIManipulation();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mani.ScrollY = -verticalThresholdNavigation;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mani.ScrollY = verticalThresholdNavigation;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mani.ScrollX = -horizontalThresholdNavigation;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mani.ScrollX = horizontalThresholdNavigation;
        }
        else if (Input.GetKeyDown(KeyCode.AltGr))
        {
            mani.IsSelecting = 1; 
        }

        ProcessInput(mani);
#endif
    }

    public void ProcessInput(UIManipulation uiManipulation)
    {
        FindObjectOfType<UIInteractionManager>().NotifyAboutUIModificationForLogging();

        if (uiManipulation.IsSelecting == 1)
        {
            isInSelectedMode = !isInSelectedMode;
            SetSelectCurrentElement(isInSelectedMode);
        }
        else
        {
            if (isInSelectedMode)
            {
                var factor = elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().Factor;

                if (Mathf.Abs(uiManipulation.ScrollY) > Mathf.Abs(uiManipulation.ScrollX))
                {
                    currentVerticalValue = currentVerticalValue + uiManipulation.ScrollY;

                    // vertical
                    if (currentVerticalValue <= -verticalThresholdNavigation * factor)
                    {
                        var count = (int)Mathf.Abs(currentVerticalValue / (-verticalThresholdNavigation * factor));
                        currentVerticalValue = currentVerticalValue % (-verticalThresholdNavigation * factor);

                        for (int i = 0; i < count; i++)
                        {
                            elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().Down();
                        }
                    }
                    else if (currentVerticalValue >= verticalThresholdNavigation * factor)
                    {
                        var count = (int)Mathf.Abs(currentVerticalValue / (verticalThresholdNavigation * factor));
                        currentVerticalValue = currentVerticalValue % (verticalThresholdNavigation * factor);

                        for (int i = 0; i < count; i++)
                        {
                            elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().Up();
                        }
                    }
                }
                else
                {
                    currentHorizontalValue = currentHorizontalValue + uiManipulation.ScrollX;

                    // horizontal
                    if (currentHorizontalValue <= -horizontalThresholdNavigation * factor)
                    {
                        var count = (int)Mathf.Abs(currentHorizontalValue / (-horizontalThresholdNavigation * factor));
                        currentHorizontalValue = currentHorizontalValue % (-horizontalThresholdNavigation * factor);

                        for (int i = 0; i < count; i++)
                        {
                            elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().Left();
                        }

                    }
                    else if (currentHorizontalValue >= horizontalThresholdNavigation * factor)
                    {
                        var count = (int)Mathf.Abs(currentHorizontalValue / (horizontalThresholdNavigation * factor));
                        currentHorizontalValue = currentHorizontalValue % (horizontalThresholdNavigation * factor);

                        for (int i = 0; i < count; i++)
                        {
                            elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().Right();
                        }
                    }
                }
            }
            else
            {
                currentVerticalValue = currentVerticalValue + uiManipulation.ScrollY;

                if (currentVerticalValue <= -verticalThresholdNavigation)
                {
                    var count = (int)Mathf.Abs(currentVerticalValue / -verticalThresholdNavigation);
                    currentVerticalValue = currentVerticalValue % -verticalThresholdNavigation;

                    for (int i = 0; i < count; i++)
                    {
                        SwapElement(false);
                    }
                }
                else if (currentVerticalValue >= verticalThresholdNavigation)
                {
                    var count = (int)Mathf.Abs(currentVerticalValue / verticalThresholdNavigation);
                    currentVerticalValue = currentVerticalValue % verticalThresholdNavigation;

                    for (int i = 0; i < count; i++)
                    {
                        SwapElement(true);
                    }
                }
            }
        }
    }

    private void SwapElement(bool up)
    {
        if (up)
        {
            HoverNewElement(Mathf.Clamp(currentElement + 1, 0, elementsToNavigate.Length - 1));
        }
        else
        {
            HoverNewElement(Mathf.Clamp(currentElement - 1, 0, elementsToNavigate.Length - 1));
        }
    }

    private void HoverNewElement(int newElement)
    {
        elementsToNavigate[currentElement].GetComponent<Image>().color = unselectedColor;
        elementsToNavigate[newElement].GetComponent<Image>().color = hoverColor;

        currentElement = newElement;
    }

    public void SetSelectCurrentElement(bool value)
    {
        if (value)
        {
            elementsToNavigate[currentElement].GetComponent<Image>().color = selectedColor;
        }
        else
        {
            elementsToNavigate[currentElement].GetComponent<Image>().color = hoverColor;
        }

        elementsToNavigate[currentElement].GetComponent<IUIInteractionHandler>().SetSelected(value);

        currentHorizontalValue = 0.0f;
        currentVerticalValue = 0.0f;
    }

    public void ResetUI()
    {
        foreach (var element in elementsToNavigate)
        {
            element.GetComponent<IUIInteractionHandler>().Reset();
        }

        toggles[0].isOn = true;
        toggles[0].Select();
        toggles[1].isOn = false;
        toggles[2].isOn = false;

        slider.value = 0.5f;

        var colorToggles = colorGridContainer.GetComponentsInChildren<Toggle>();

        foreach (var toggle in colorToggles)
        {
            toggle.isOn = false;
        }

        colorToggles[0].isOn = true;
        colorToggles[0].Select();

        SetSelectCurrentElement(false);
        HoverNewElement(0);

        currentHorizontalValue = 0.0f;
        currentVerticalValue = 0.0f;
    }
}
