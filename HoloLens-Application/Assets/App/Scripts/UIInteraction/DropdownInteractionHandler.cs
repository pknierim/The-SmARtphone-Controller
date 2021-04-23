using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownInteractionHandler : MonoBehaviour, IUIInteractionHandler
{
    [SerializeField]
    private TMP_Dropdown dropdown;

    private int previousValue = 0;

    public float Factor
    {
        get
        {
            return 1;
        }
    }

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(int value)
    {
        transform.Find("ObjectDropdown/Dropdown List/Viewport/Content")?.GetChild(dropdown.value + 1).GetComponent<Toggle>().Select();
        //transform.Find("ObjectDropdown/Dropdown List/Viewport/Content").GetChild(previousValue + 1).GetComponent<Toggle>();
        previousValue = dropdown.value;
    }

    public void Down()
    {
        dropdown.value = Mathf.Clamp(dropdown.value - 1, 0, dropdown.options.Count - 1);
    }

    public void Up()
    {
        dropdown.value = Mathf.Clamp(dropdown.value + 1, 0, dropdown.options.Count - 1);
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
        if (value)
        {
            dropdown.Show();
        }
        else
        {
            dropdown.Hide();
        }
    }

    public void Reset()
    {
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }
}
