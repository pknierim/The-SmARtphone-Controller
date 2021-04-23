using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IModelChanger
{
    ObjectParameters GetCurrentObjectParameters();
    void SetTaskInfo(ObjectParameters parameters);
    void SetTaskInfoEmpty();
    void ResetParameters();
}

public class ModelChanger : MonoBehaviour, IModelChanger
{
    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private TMP_Text scaleIndicatorText;

    [SerializeField]
    private Tasks tasks;

    [SerializeField]
    private GameObject colorGrid;

    [SerializeField]
    private TMP_Dropdown objectTypeDropdown;

    [SerializeField]
    private TMP_Text taskInfoText;

    private ObjectQuality currentQuality = ObjectQuality.High;
    private ObjectType currentObjectType = ObjectType.Teapot;
    private Color currentColor = Color.white;

    private int scaleValue = 50;

    private float startScale;

    void Start()
    {
        for (int i = 0; i < colorGrid.transform.childCount; i++)
        {
            colorGrid.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = tasks.colors[i];
        }

        startScale = transform.localScale.x;
        SetObjectTypeDropdownOptions();
        ResetParameters();
    }

    public void SetModel(int objectModel)
    {
        meshFilter.mesh = tasks.meshes[objectModel].modelMeshes[(int)currentQuality];
        currentObjectType = (ObjectType)objectModel;
    }

    public void SetQuality(int quality)
    {
        currentQuality = (ObjectQuality)quality;
        meshFilter.mesh = tasks.meshes[(int)currentObjectType].modelMeshes[(int)currentQuality];
    }

    public void SetScale(float scale)
    {
        var newScale = startScale * ((scale / 4.0f) + (1 - (0.5f / 4.0f)));
        transform.localScale = new Vector3(newScale, newScale, newScale);

        scaleValue = (int)(scale * 100.0f);
        scaleIndicatorText.text = ((float)scaleValue).ToString();
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

        SetColor(0);
        SetModel(0);
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

    private void SetObjectTypeDropdownOptions()
    {
        objectTypeDropdown.ClearOptions();
        objectTypeDropdown.AddOptions(new List<String>(tasks.objectTypeNames));
    }

    public void SetTaskInfo(ObjectParameters parameters)
    {
        var text = "";
        text += "1." + tasks.objectTypeNames[(int)parameters.Type] + "\n";
        text += "2." + tasks.objectQualityNames[(int)parameters.Quality] + "\n";
        text += "3." + parameters.Size + "%" + "\n";
        text += "3." + "<color=#"+ ColorUtility.ToHtmlStringRGBA(parameters.Color) + ">" + tasks.colorNames[Array.IndexOf(tasks.colors, parameters.Color)] + "</color>" + "\n";
        taskInfoText.text = text;
    }

    public void SetTaskInfoEmpty()
    {
        taskInfoText.text = "Klicke auf Fertig um Tutorial zu beenden.";
    }
}
