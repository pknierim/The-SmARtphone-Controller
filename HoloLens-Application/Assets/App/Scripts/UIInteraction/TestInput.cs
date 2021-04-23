using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class TestInput : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    [SerializeField]
    private Transform target;

    private Vector3 currentTranslationTarget;
    private Vector3 currentTranslationVelocity = Vector3.zero;
    private float smoothFactor = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        currentTranslationTarget = target.position;

        transform.position = Vector3.SmoothDamp(transform.position, currentTranslationTarget, ref currentTranslationVelocity, smoothFactor);
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Down");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Up");
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        Debug.Log("Enter");
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        Debug.Log("Exit");
    }
}
