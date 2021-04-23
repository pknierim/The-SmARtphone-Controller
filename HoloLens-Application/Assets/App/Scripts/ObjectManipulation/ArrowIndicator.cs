using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowIndicator : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material.DOFade(0, 1).SetDelay(1.0f);
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().material.DOFade(0, 1).SetDelay(1.0f);
    }
}
