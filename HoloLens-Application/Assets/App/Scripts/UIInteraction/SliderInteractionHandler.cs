using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderInteractionHandler : MonoBehaviour, IUIInteractionHandler
{
    [SerializeField]
    private Slider slider;


    public float Factor
    {
        get
        {
#if UNITY_EDITOR
            return 1.0f;
#endif
            return 0.15f;
        }
    }

    void Start()
    {
        
    }

    public void Up()
    {
        // do nothing
    }

    public void Down()
    {
        // do nothing
    }

    public void Left()
    {
        slider.value -= 0.01f;
    }

    public void Right()
    {
        slider.value += 0.01f;
    }

    public void SetSelected(bool value)
    {

    }

    public void Reset()
    {
        slider.value = 0.5f;
    }
}
