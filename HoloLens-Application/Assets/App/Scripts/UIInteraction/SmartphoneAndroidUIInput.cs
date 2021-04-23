using Google.Protobuf.HoloLensAndroidMessaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneAndroidUIInput : MonoBehaviour
{
    [SerializeField]
    private SimpleModelChanger modelChanger;

    void Start()
    {

    }

    public void ProcessInput(UIManipulation uiManipulation)
    {
        FindObjectOfType<UIInteractionManager>().NotifyAboutUIModificationForLogging();

        modelChanger.SetColor((int)uiManipulation.AndroidUIInput.Color);
        modelChanger.SetModel((int)uiManipulation.AndroidUIInput.ObjectType);
        modelChanger.SetQuality((int)uiManipulation.AndroidUIInput.Quality);
        modelChanger.SetScale(((float)uiManipulation.AndroidUIInput.Scale) / 100.0f);   
    }
}
