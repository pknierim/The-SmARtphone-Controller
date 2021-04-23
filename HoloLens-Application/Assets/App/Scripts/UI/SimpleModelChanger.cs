using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SimpleModelChanger : MonoBehaviour, IModelChanger
{
    [SerializeField]
    private Tasks tasks;

    [SerializeField]
    private GameObject previewObject;

    [SerializeField]
    private TMP_Text taskInfoText;

    private ObjectQuality currentQuality = ObjectQuality.High;
    private ObjectType currentObjectType = ObjectType.Teapot;
    private Color currentColor = Color.white;

    private int scaleValue = 50;

    private float startScale;

    void Start()
    {
        startScale = transform.localScale.x;
        ResetParameters();
    }

    public void SetModel(int objectModel)
    {
        previewObject.GetComponent<MeshFilter>().mesh = tasks.meshes[objectModel].modelMeshes[(int)currentQuality];
        currentObjectType = (ObjectType)objectModel;
    }

    public void SetQuality(int quality)
    {
        currentQuality = (ObjectQuality)quality;
        previewObject.GetComponent<MeshFilter>().mesh = tasks.meshes[(int)currentObjectType].modelMeshes[(int)currentQuality];
    }

    public void SetScale(float scale)
    {
        var newScale = startScale * ((scale / 4.0f) + (1 - (0.5f / 4.0f)));
        transform.localScale = new Vector3(newScale, newScale, newScale);

        scaleValue = (int)(scale * 100.0f);
    }

    public void SetColor(int color)
    {
        currentColor = tasks.colors[color];
        GetComponent<Renderer>().material.SetColor("_Color", currentColor);
    }

    public void ResetParameters()
    {
        if (startScale == 0)
        {
            startScale = transform.localScale.x;
        }

        SetModel(0);
        SetColor(0);
        SetQuality(0);
        SetScale(0.5f);
    }

    public ObjectParameters GetCurrentObjectParameters()
    {
        var parameters = new ObjectParameters()
        {
            Type = currentObjectType,
            Quality = currentQuality,
            Size = scaleValue,
            Color = currentColor
        };

        return parameters;
    }

    public void SetTaskInfo(ObjectParameters parameters)
    {
        var text = "";
        text += "1." + tasks.objectTypeNames[(int)parameters.Type] + "\n";
        text += "2." + tasks.objectQualityNames[(int)parameters.Quality] + "\n";
        text += "3." + parameters.Size + "%" + "\n";
        text += "3." + "<color=#" + ColorUtility.ToHtmlStringRGBA(parameters.Color) + ">" + tasks.colorNames[Array.IndexOf(tasks.colors, parameters.Color)] + "</color>" + "\n";
        taskInfoText.text = text;
    }

    public void SetTaskInfoEmpty()
    {
        taskInfoText.text = "Klicke auf Fertig um Tutorial zu beenden.";
    }
}
