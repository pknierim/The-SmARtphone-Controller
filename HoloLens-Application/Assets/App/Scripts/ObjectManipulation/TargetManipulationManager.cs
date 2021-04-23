using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Google.Protobuf.HoloLensAndroidMessaging;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using System.Globalization;

public class TargetManipulationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectManipulationButton;

    [SerializeField]
    private UIInteractionManager uiInteractionManager;

    [SerializeField]
    private Transform targetSpawnPosition;

    [SerializeField]
    private GameObject[] targetGoalCollections;

    [SerializeField]
    private GameObject[] targetCollections;

    [SerializeField]
    private GameObject[] targetGestureCollections;

    [SerializeField]
    private BluetoothManager bluetoothManager;

    [SerializeField]
    private GameObject indicatorArrow;

    [SerializeField]
    private AudioClip specialSound;

    private List<Tuple<GameObject, GameObject>> collectionsPairs = new List<Tuple<GameObject, GameObject>>();
    private int SetPairCounter = 1;
    private Tuple<GameObject, GameObject> currentCollectionPair;
    private GameObject currentTarget;
    private GameObject currentGoal;

    private ManipulationType currentMode;

    private MainManager mainManager;
    private TutorialTargetManipulation tutorialTargetManipulation;
    private bool isTutorialOn = false;

    void Start()
    {
        mainManager = GetComponent<MainManager>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!isTutorialOn)
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                SelectCurrentlyClosestTarget();
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                FinishCurrentTarget();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                var manipulation = new ObjectManipulation()
                {
                    RotationY = 1000,
                    Scale = 1,
                    IsSelecting = 1
                };

                tutorialTargetManipulation.HandleManipulationInput(manipulation);
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                var manipulation = new ObjectManipulation()
                {
                    RotationY = 1000,
                    Scale = 1,
                    IsFinished = 1
                };

                tutorialTargetManipulation.HandleManipulationInput(manipulation);
            }
        }
    }
#endif

    public void ActivateButton()
    {
        objectManipulationButton.SetActive(true);
    }

    public void StartTutorial(ManipulationType mode)
    {
        isTutorialOn = true;
        currentMode = mode;
        tutorialTargetManipulation = new TutorialTargetManipulation(mainManager, this, this.bluetoothManager, mode);
        if (mode == ManipulationType.Smartphone)
        {
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseObjectTranslation);
        }
    }

    public void StartManipulationTasks(ManipulationType mode)
    {
        this.currentMode = mode;

        if (mode == ManipulationType.Gesture)
        {
            for (int i = 0; i < targetGoalCollections.Length; i++)
            {
                collectionsPairs.Add(new Tuple<GameObject, GameObject>(targetGoalCollections[i], targetGestureCollections[i]));
            }

            InitializeNextTaskPair();
        }
        else if (mode == ManipulationType.Smartphone)
        {
            for (int i = 0; i < targetGoalCollections.Length; i++)
            {
                collectionsPairs.Add(new Tuple<GameObject, GameObject>(targetGoalCollections[i], targetCollections[i]));
            }

            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseObjectTranslation);
            InitializeNextTaskPair();
        }

        foreach (var goal in targetGoalCollections)
        {
            goal.SetActive(true);
        }

        SetPairCounter = 1;
    }

    private bool InitializeNextTaskPair()
    {
        SetPairCounter += 1;

        if (currentCollectionPair != null)
        {
            Destroy(currentCollectionPair.Item1);
            Destroy(currentCollectionPair.Item2);
        }

        if (collectionsPairs.Count != 0)
        {
            var goals = Instantiate(collectionsPairs[0].Item1, targetSpawnPosition.position, targetSpawnPosition.rotation);
            var targets = Instantiate(collectionsPairs[0].Item2, targetSpawnPosition.position, targetSpawnPosition.rotation);

            currentCollectionPair = new Tuple<GameObject, GameObject>(goals, targets);
            collectionsPairs.RemoveAt(0);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void HandleManipulationInput(ObjectManipulation manipulation)
    {
        if (isTutorialOn)
        {
            tutorialTargetManipulation.HandleManipulationInput(manipulation);
        }
        else
        {
            if (manipulation.IsSelecting == 1)
            {
                SelectCurrentlyClosestTarget();
                
            }
            else if (manipulation.IsFinished == 1)
            {
                FinishCurrentTarget();
            }
            else
            {
                if (currentTarget != null)
                {
                    LoggingTimer.Instance.TriggerTimestamp();
                    var manipulationHandling = currentTarget.GetComponent<CustomManipulationHandling>();

                    // Translation
                    if (manipulation.TranslationX != 0 || manipulation.TranslationY != 0 || manipulation.TranslationZ != 0)
                    {
                        var translation = new Vector3(manipulation.TranslationX * 0.01f, -manipulation.TranslationY * 0.01f, -manipulation.TranslationZ * 0.01f);
                        manipulationHandling.DoTranslation(translation, manipulation.IsFirstTouch == 1 ? true : false);
                    }

                    // Rotation
                    if (manipulation.RotationY != 1000)
                    {
                        manipulationHandling.DoRotation(manipulation.RotationY);
                    }
                    else
                    {
                        manipulationHandling.FinishRotation();
                    }

                    // scaling
                    manipulationHandling.DoScaling(manipulation.Scale);  
                }
            }

        }
    }

    public void FinishCurrentTarget()
    {
        GetComponent<AudioSource>().Play();

        if (!isTutorialOn)
        {
            TargetTransformationDiscrepancy discrepancy = currentGoal.GetComponent<TargetGoal>().GetDiscrepancy(currentTarget.transform);

            var inputModality = currentMode == ManipulationType.Smartphone ? "Smartphone" : "Gesture";
            var set = SetPairCounter;
            var completionTime = LoggingTimer.Instance.ElapsedTimeInMilliseconds;
            var positionDiff = discrepancy.PositionMagnitude;
            var rotationDiff = discrepancy.RotationDifferenceAngle;
            var scaleDiff = discrepancy.ScaleDifference;

            var logString = inputModality + "," + set + "," + completionTime + "," + positionDiff.ToString("0.#####") 
                + "," + (set > 1 ? rotationDiff.ToString("0.#####") : "NaN") + "," + (set > 2 ? scaleDiff.ToString("0.#####") : "NaN");

            var captions = "Input Modality,Set,Task Completion Time (ms),Position Difference (m),Y-Rotation Difference (°),Scale Difference Factor";

            if (currentMode == ManipulationType.Smartphone)
            {
                LoggingManager.Instance.Log(logString, captions, LoggingManager.LogType.ObjectManipulationSmartphone);
            }
            else if (currentMode == ManipulationType.Gesture)
            {
                LoggingManager.Instance.Log(logString, captions, LoggingManager.LogType.ObjectManipulationGesture);
            }
                
            Destroy(currentGoal);
            Destroy(currentTarget);
            StartCoroutine(CheckForLastGoalDone());
        }

        if (currentMode == ManipulationType.Gesture)
        {
            if (isTutorialOn)
            {
                // forward finish to tutorial handling
                tutorialTargetManipulation.FinishCurrentTarget();
            }
            else
            {
                // set remaining targets active 
                for (int i = 0; i < currentCollectionPair.Item2.transform.childCount; i++)
                {
                    if (currentCollectionPair.Item2.transform.GetChild(i).gameObject != currentTarget)
                    {
                        currentCollectionPair.Item2.transform.GetChild(i).GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
        else if (currentMode == ManipulationType.Smartphone)
        {
            if (isTutorialOn)
            {
                // won't be called I think
            }
            else
            {
                mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.ObjectManipulationFinished);
            }
        }
    }



    private IEnumerator CheckForLastGoalDone()
    {
        yield return null;

        if (currentCollectionPair.Item1.transform.childCount == 0) // TODO: error here?
        {
            // all goals done in current collection

            if (InitializeNextTaskPair() == false)
            {
                GetComponent<MainManager>().StartNextTask();
                LoggingTimer.Instance.StopAndReset();
            }
        }
    }

    private void SelectCurrentlyClosestTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag("Target");
        currentTarget = targets.OrderBy((target) => Vector3.Angle(target.transform.position - Camera.main.transform.position, Camera.main.transform.forward)).First();
        var id = currentTarget.GetComponent<IManipulationHandling>().GetId();

        for (int i = 0; i < currentCollectionPair.Item2.transform.childCount; i++)
        {
            if (currentCollectionPair.Item1.transform.GetChild(i).GetComponent<TargetGoal>().GetId() == id)
            {
                currentGoal = currentCollectionPair.Item1.transform.GetChild(i).gameObject;
            }
        }

        currentTarget.GetComponent<IManipulationHandling>().NotifyAboutSelection();
        currentGoal.GetComponent<TargetGoal>().Show();
        mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.ObjectForManipulationSelected);

        LoggingTimer.Instance.Start();
    }

    public void SetSelectedTarget(GameObject target, GameObject goal)
    {
        if (!isTutorialOn)
        {
            currentTarget = target;
            currentGoal = goal;

            for (int i = 0; i < currentCollectionPair.Item2.transform.childCount; i++)
            {
                if (currentCollectionPair.Item2.transform.GetChild(i).gameObject != currentTarget)
                {
                    currentCollectionPair.Item2.transform.GetChild(i).GetComponent<Collider>().enabled = false;
                }
            }
        }
    }

    public CustomManipulationHandling GetSelectedTarget()
    {
        return currentTarget?.GetComponent<CustomManipulationHandling>();
    }

    public void FinishTutorial()
    {
        tutorialTargetManipulation = null;
        isTutorialOn = false;
        GetComponent<AudioSource>().PlayOneShot(specialSound);
    }

    public enum ManipulationType
    {
        Gesture, Smartphone
    }
}
