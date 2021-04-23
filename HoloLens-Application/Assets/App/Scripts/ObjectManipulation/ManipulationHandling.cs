using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ManipulationHandling : MonoBehaviour
{
    [SerializeField]
    protected bool isForTutorial = false;

    [SerializeField]
    protected int id;

    [SerializeField]
    protected ManipulationType manipulationType;

    [SerializeField]
    protected GameObject indicatorArrow;

    protected TargetGoal targetGoal;
    private bool firstUpdateCalled = false;

    protected virtual void Start()
    {
        transform.hasChanged = false;
    }

    protected  virtual void Update()
    {
        if (transform.hasChanged && !isForTutorial)
        {
            LoggingTimer.Instance.TriggerTimestamp();
            transform.hasChanged = false;
        }
    }

    protected void SetupForTask()
    {
        foreach (var target in GameObject.FindObjectsOfType<TargetGoal>())
        {
            if (target.GetId() == id)
            {
                targetGoal = target;
            }
        }

        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);
        var correctColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
        correctColor.a = 0.0f;
        GetComponent<LineRenderer>().material.SetColor("_Color", correctColor);
    }

    protected IEnumerator ShowHintToGoal()
    {
        float timer = 0;
        GetComponent<LineRenderer>().material.DOFade(1.0f, 0.5f).OnComplete(() =>
        {
            GetComponent<LineRenderer>().material.DOFade(0.0f, 0.5f).SetDelay(3.0f);
        });

        while (timer < 2.5f)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, targetGoal.transform.position);
            yield return null;
            timer += Time.deltaTime;
        }

        Destroy(GetComponent<LineRenderer>());
    }

    public int GetId()
    {
        return this.id;
    }

    public virtual void NotifyAboutSelection()
    {
        StartCoroutine(ShowHintToGoal());
        var arrow = Instantiate(indicatorArrow, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
        arrow.transform.SetParent(transform);
        targetGoal.Show();

        if (!isForTutorial)
        {
            LoggingTimer.Instance.Start();
        }
    }
}
