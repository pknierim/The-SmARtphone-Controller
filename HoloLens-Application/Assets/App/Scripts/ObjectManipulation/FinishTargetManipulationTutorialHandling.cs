using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTargetManipulationTutorialHandling : MonoBehaviour
{
    [SerializeField]
    private Color triggeredColor = new Color(37, 160, 210, 0.6f);

    private Color untriggeredColor;

    private bool isTriggered = false;
    public bool IsTriggered { get { return isTriggered;  } }

    void Start()
    {
        untriggeredColor = GetComponent<Renderer>().material.GetColor("_Color");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            isTriggered = true;
            GetComponent<Renderer>().material.SetColor("_Color", triggeredColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            isTriggered = false;
            GetComponent<Renderer>().material.SetColor("_Color", untriggeredColor);
        }
    }
}
