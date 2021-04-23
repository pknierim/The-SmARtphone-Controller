using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IManipulationHandling
{
    int GetId();
    void NotifyAboutSelection();
}

public enum ManipulationType
{
    Translation, TranslationAndRotation, TranslationRotationScale
}

public class CustomManipulationHandling : ManipulationHandling, IManipulationHandling
{
    private Vector3 currentDirection = Vector3.forward;
    private Vector3 currentDirectionOrthogonal = Vector3.right;

    private Vector3 startScale;
    private Quaternion previousRotation;
    private float rotationBaseLine;

    private Vector3 currentTranslationTarget;
    private Vector3 currentTranslationVelocity = Vector3.zero;
    private float translationSmoothFactor = 0.05f;

    private float currentRotationTarget;
    private float currentRotationVelocity = 0;
    private float rotationSmoothFactor = 0.05f;

    private float currentScaleTarget;
    private float currentScaleVelocity = 0;
    private float scaleSmoothFactor = 0.03f;

    private bool wasRotated = false;
    private float currentRotationOffset = 0;

    protected override void Start()
    {
        base.Start();
        startScale = transform.localScale;
        currentTranslationTarget = transform.position;
        previousRotation = transform.rotation;
        currentScaleTarget = transform.localScale.x;
        currentRotationTarget = transform.rotation.eulerAngles.y;

        if (!isForTutorial)
        {
            SetupForTask();
        }
        else
        {
            SetupForTask();
        }
    }

    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.SmoothDamp(transform.position, currentTranslationTarget, ref currentTranslationVelocity, translationSmoothFactor);
        transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, currentRotationTarget, ref currentRotationVelocity, rotationSmoothFactor), 0);

        var scale = Mathf.SmoothDamp(transform.localScale.x, currentScaleTarget, ref currentScaleVelocity, scaleSmoothFactor);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void DoScaling(float scale)
    {
        if (manipulationType == ManipulationType.TranslationRotationScale)
        {
            //transform.localScale = startScale * scale;
            currentScaleTarget = startScale.x * scale;
        }
    }

    public void DoTranslation(Vector3 translation, bool resetAnchoringAxis)
    {
        //// near interaction factor
        //if (Vector2.Distance(new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < 1.0f)
        //{
        //    translation *= 0.2f;
        //}

        translation *= 0.5f;

        if (resetAnchoringAxis)
        {
            currentDirection = CalculateCameraAlignedDirection();
            currentDirectionOrthogonal = Vector3.Cross(Vector3.up, currentDirection);
        }

        Vector3 alignedTranslation = currentDirection * translation.z;
        alignedTranslation += currentDirectionOrthogonal * translation.x;
        alignedTranslation.y = translation.y;

        //transform.Translate(alignedTranslation, Space.World);
        currentTranslationTarget = currentTranslationTarget + alignedTranslation;
    }

    public void DoRotation(float rotationY)
    {
        if (manipulationType == ManipulationType.Translation)
        {
            return;
        }

        //if (wasRotated == false)
        //{
        //    previousRotation = Quaternion.Euler(0, rotationY, 0);
        //    rotationBaseLine = transform.rotation.eulerAngles.y;
        //    wasRotated = true;
        //}

        //transform.rotation = Quaternion.Euler(0, rotationY - previousRotation.eulerAngles.y, 0);
        currentRotationTarget += rotationY;
    }

    public void FinishRotation()
    {
        if (wasRotated)
        {
            wasRotated = false;
            Debug.Log("Rotation finished");
        }
    }

    private Vector3 CalculateCameraAlignedDirection()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        direction.y = 0;
        direction.Normalize();

        return direction;
    }
}
