using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class PopupText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;

    private static PopupText instance;
    public static PopupText Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        instance = this;
    }

    public void ShowText(string text, float duration)
    {
        this.text.alpha = 1.0f;
        this.text.text = text;
        this.text.material.DOFade(0.0f, 0.5f)
            .SetDelay(duration)
            .OnComplete(() => { this.text.text = ""; });
    }
}
