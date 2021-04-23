using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderGestureHandler : BaseInputHandler, IMixedRealityGestureHandler<Vector3>, IMixedRealityGestureHandler
{
    private static IMixedRealityInputSystem inputSystem;

    protected override void Start()
    {
        base.Start();
        inputSystem = MixedRealityToolkit.InputSystem;
    }

    void IMixedRealityGestureHandler.OnGestureCanceled(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IMixedRealityGestureHandler<Vector3>.OnGestureCompleted(InputEventData<Vector3> eventData)
    {
        Debug.Log("completed vector3");
    }

   
    void IMixedRealityGestureHandler<Vector3>.OnGestureUpdated(InputEventData<Vector3> eventData)
    {
        Debug.Log("update vector3");
        transform.position += eventData.InputData;
    }

    #region IMixedRealityGestureHandler

    void IMixedRealityGestureHandler.OnGestureStarted(InputEventData eventData)
    {
        Debug.Log("started");
    }

    void IMixedRealityGestureHandler.OnGestureUpdated(InputEventData eventData)
    {
        Debug.Log("update");
    }

    void IMixedRealityGestureHandler.OnGestureCompleted(InputEventData eventData)
    {
        Debug.Log("completed");
    }

    #endregion
}
