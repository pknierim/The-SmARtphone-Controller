using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIInteractionHandler
{
    float Factor
    {
        get;
    }

    void SetSelected(bool value);
    void Up();
    void Down();
    void Left();
    void Right();
    void Reset();
}
