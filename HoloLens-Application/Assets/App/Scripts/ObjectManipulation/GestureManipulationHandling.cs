using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class GestureManipulationHandling : ManipulationHandling, IManipulationHandling
{
    [SerializeField]
    private GameObject finishButton;

    private bool wasAlreadySelected = false;

    private void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();

        SetupForTask();
    }
    protected override void Update()
    {
        base.Update();
    }

    private void LateUpdate()
    {
        var clamped = Mathf.Clamp(transform.localScale.x, 0.25f, 1.0f);
        transform.localScale = new Vector3(clamped, clamped, clamped);
        transform.hasChanged = false;
    }

    public override void NotifyAboutSelection()
    {
        if (!wasAlreadySelected)
        {
            base.NotifyAboutSelection();
            wasAlreadySelected = true;
            FindObjectOfType<TargetManipulationManager>().SetSelectedTarget(this.gameObject, targetGoal.gameObject);
            StartCoroutine(SpawnButtonAfterDelay());
        }
    }

    private IEnumerator SpawnButtonAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        var button = Instantiate(finishButton) as GameObject;
        button.GetComponent<GestureFinishManipulationButton>().SetTarget(this.gameObject);
    }
}
