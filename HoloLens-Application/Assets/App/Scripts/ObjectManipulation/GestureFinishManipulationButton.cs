using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureFinishManipulationButton : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 1.5f, 0.0f);

    private GameObject target;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            this.transform.position = (target.transform.position + offset);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void TriggerFinish()
    {
        FindObjectOfType<TargetManipulationManager>().FinishCurrentTarget();
        Destroy(this.gameObject);

    }
}
