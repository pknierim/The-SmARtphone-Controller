using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGridInteractionHandler : MonoBehaviour, IUIInteractionHandler
{
    [SerializeField]
    private GameObject[] colorButtons;

    private int[] currentColor = new int[2] { 0, 0 };

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
        currentColor[1] = Mathf.Clamp(currentColor[1] + 1, 0, 1);
        ToggleCurrentColor();
    }

    public void Down()
    {
        currentColor[1] = Mathf.Clamp(currentColor[1] - 1, 0, 1);
        ToggleCurrentColor();
    }

    public void Left()
    {
        currentColor[0] = Mathf.Clamp(currentColor[0] - 1, 0, 3);
        ToggleCurrentColor();
    }

    public void Right()
    {
        currentColor[0] = Mathf.Clamp(currentColor[0] + 1, 0, 3);
        ToggleCurrentColor();
    }

    private void ToggleCurrentColor()
    {
        GetCorrespondingColor(currentColor).GetComponent<Toggle>().isOn = true;
    }

    private GameObject GetCorrespondingColor(int[] position)
    {
        return colorButtons[position[1] == 0 ? position[0] : (position[0] + 4)];
    }

    public void SetSelected(bool value)
    {
        // do nothing
    }

    public void Reset()
    {
        currentColor = new int[2] { 0, 0 };
    }
}
