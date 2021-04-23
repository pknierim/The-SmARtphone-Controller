using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TargetTransformationDiscrepancy
{
    public float PositionMagnitude;
    public float RotationDifferenceAngle;
    public float ScaleDifference;
}

public class TargetGoal : MonoBehaviour
{
    [SerializeField]
    private int id;

    private TargetManipulationManager demo;

    void Start()
    {
        demo = GameObject.FindObjectOfType(typeof(TargetManipulationManager)) as TargetManipulationManager;
        GetComponent<Renderer>().enabled = false;
    }

    public TargetTransformationDiscrepancy GetDiscrepancy(Transform target)
    {
        var discrepancy = new TargetTransformationDiscrepancy
        {
            PositionMagnitude = (target.position - this.transform.position).magnitude,
            RotationDifferenceAngle = Quaternion.Angle(target.transform.rotation, this.transform.rotation),
            ScaleDifference = Mathf.Abs(target.transform.localScale.x - this.transform.localScale.x)
        };

        return discrepancy;
    }

    public int GetId()
    {
        return id;
    }

    public void Show()
    {
        GetComponent<Renderer>().enabled = true;
    }
}
