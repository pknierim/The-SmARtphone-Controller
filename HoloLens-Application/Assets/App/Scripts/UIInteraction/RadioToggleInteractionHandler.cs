using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioToggleInteractionHandler : MonoBehaviour, IUIInteractionHandler
{
    [SerializeField]
    private Toggle[] toggles;

    private int currentToggle = 0;

    public float Factor
    {
        get
        {
            return 0.75f;
        }
    }

    void Start()
    {

    }

    public void Up()
    {
        toggles[currentToggle].isOn = false;
        currentToggle = Mathf.Clamp(currentToggle + 1, 0, toggles.Length - 1);
        toggles[currentToggle].isOn = true;
    }

    public void Down()
    {
        toggles[currentToggle].isOn = false;
        currentToggle = Mathf.Clamp(currentToggle - 1, 0, toggles.Length - 1);
        toggles[currentToggle].isOn = true;
    }

    public void Left()
    {
        // do nothing
    }

    public void Right()
    {
        // do nothing
    }

    public void SetSelected(bool value)
    {
        // do nothing
    }

    public void Reset()
    {
        currentToggle = 0;

        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
        }

        toggles[0].isOn = true;
    }
}
