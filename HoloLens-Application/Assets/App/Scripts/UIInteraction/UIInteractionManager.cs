using Google.Protobuf.HoloLensAndroidMessaging;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    [SerializeField]
    private Transform targetSpawnPosition;

    [SerializeField]
    private GameObject uiPrefabSmartphoneNonImmersive;

    [SerializeField]
    private GameObject uiPrefabSmartphoneImmersive;

    [SerializeField]
    private GameObject uiPrefabGesture;

    [SerializeField]
    private Tasks tasks;

    [SerializeField]
    private AudioClip successSound;

    [SerializeField]
    private AudioClip correctSound;

    [SerializeField]
    private AudioClip failSound;

    private MainManager mainManager;

    private GameObject currentUI;
    private IModelChanger modelChanger;

    private bool isTutorialOn = false;

    private Queue<ObjectParameters> objectTaskQueue;
    private ObjectParameters currentTaskParameters;
    private UIInteractionMode currentMode;

    private GameObject hololensCursor;

    private bool firstInteractionOccured = false;

    public enum UIInteractionMode
    {
        Gesture, SmartphoneImmersive, SmartphoneNonImmersive
    }

    private void Awake()
    {
        mainManager = GetComponent<MainManager>();
    }

    void Start()
    {
        objectTaskQueue = new Queue<ObjectParameters>();
        hololensCursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
        {
             HandleManipulationInput(new UIManipulation() { IsFinished = 1 });
        }
#endif
    }

    public void NotifyAboutUIModificationForLogging()
    {
        if (!isTutorialOn)
        {
            if (!firstInteractionOccured)
            {
                LoggingTimer.Instance.Start();
            }

            firstInteractionOccured = true;

            LoggingTimer.Instance.TriggerTimestamp();
        }
    }

    public void StartTutorial(UIInteractionMode mode)
    {
        isTutorialOn = true;
        currentMode = mode;

        if (mode == UIInteractionMode.SmartphoneImmersive)
        {
            SpawnUIAndSetup(uiPrefabSmartphoneImmersive);
            hololensCursor.SetActive(false);
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseUiManipulation);
        }
        else if (mode == UIInteractionMode.SmartphoneNonImmersive)
        {
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseUiManipulationAndroidUi);
            hololensCursor.SetActive(false);
            SpawnUIAndSetup(uiPrefabSmartphoneNonImmersive);
        }
        else if (mode == UIInteractionMode.Gesture)
        {
            hololensCursor.SetActive(true);
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseClear);
            SpawnUIAndSetup(uiPrefabGesture);
        }

        modelChanger.SetTaskInfoEmpty();
    }

    public void StartUIInteraction(UIInteractionMode mode)
    {
        isTutorialOn = false;
        currentMode = mode;
        Destroy(currentUI);

        foreach (var task in tasks.tasks)
        {
            objectTaskQueue.Enqueue(task);
        }

        if (currentMode == UIInteractionMode.SmartphoneImmersive)
        {
            SpawnUIAndSetup(uiPrefabSmartphoneImmersive);
            hololensCursor.SetActive(false);
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseUiManipulation);
            StartNextTask(false);
        }
        else if (currentMode == UIInteractionMode.SmartphoneNonImmersive)
        {
            SpawnUIAndSetup(uiPrefabSmartphoneNonImmersive);
            hololensCursor.SetActive(false);
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseUiManipulationAndroidUi);
            StartNextTask(false);
        }
        else if (currentMode == UIInteractionMode.Gesture)
        {
            hololensCursor.SetActive(true);
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseClear);
            SpawnUIAndSetup(uiPrefabGesture);
            StartNextTask(false);
        }
    }

    public void HandleManipulationInput(UIManipulation uiManipulation)
    {
        if (currentMode == UIInteractionMode.SmartphoneImmersive)
        {
            if (uiManipulation.IsFinished == 1)
            {
                if (TriggerFinish())
                {
                    currentUI.GetComponent<SmartphoneUIInput>().ResetUI();
                    mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UiManipulationCorrect);
                }
                else
                {
                    mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UiManipulationFalse);
                }

                currentUI.GetComponent<SmartphoneUIInput>().SetSelectCurrentElement(false);
            }
            else
            {
                currentUI.GetComponent<SmartphoneUIInput>().ProcessInput(uiManipulation);
                
            }
        }
        else if (currentMode == UIInteractionMode.SmartphoneNonImmersive)
        {
            if (uiManipulation.IsFinished == 1)
            {
                if (TriggerFinish())
                {
                    mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UiManipulationCorrect);
                }
                else
                {
                    mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.UiManipulationFalse);
                }
            }
            else if (uiManipulation.HasAndroidUIInput == 1)
            {
                currentUI.GetComponent<SmartphoneAndroidUIInput>().ProcessInput(uiManipulation);
            }
        }
    }

    public bool TriggerFinish()
    {
        if (isTutorialOn)
        {
            FinishTutorial();
            GetComponent<AudioSource>().PlayOneShot(correctSound);
            return true;
        }
        else
        {
            if (currentTaskParameters == modelChanger.GetCurrentObjectParameters())
            {
                GetComponent<AudioSource>().PlayOneShot(correctSound);
                LogTaskPerformance();
                StartNextTask(true);

                return true;
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(failSound);
                return false;
            }
        }
    }

    private void LogTaskPerformance()
    {
        //var logString = "[Mode: " + currentMode.ToString() + ", Task: " + (tasks.tasks.Length - objectTaskQueue.Count) + "]"
        //            + "[Elapsed Time: " + LoggingTimer.Instance.ElapsedTimeInMilliseconds + "ms]";

        var captions = "Input Modality,Task,Task Completion Time (ms)";

        string inputModality = "";

        switch (currentMode)
        {
            case UIInteractionMode.Gesture:
                inputModality = "Gesture";
                break;
            case UIInteractionMode.SmartphoneImmersive:
                inputModality = "Smartphone Immersive";
                break;
            case UIInteractionMode.SmartphoneNonImmersive:
                inputModality = "Smartphone Non-Immersive";
                break;
        }

        var logString = inputModality + "," + (tasks.tasks.Length - objectTaskQueue.Count) + "," + LoggingTimer.Instance.ElapsedTimeInMilliseconds;

        LoggingManager.LogType logType = LoggingManager.LogType.UIManipulationGesture;

        switch (currentMode)
        {
            case UIInteractionMode.Gesture:
                logType = LoggingManager.LogType.UIManipulationGesture;
                break;
            case UIInteractionMode.SmartphoneImmersive:
                logType = LoggingManager.LogType.UIManipulationSmartphoneImmersive;
                break;
            case UIInteractionMode.SmartphoneNonImmersive:
                logType = LoggingManager.LogType.UIManipulationSmartphoneNonImmersive;
                break;
        }

        LoggingManager.Instance.Log(logString, captions, logType);
        LoggingTimer.Instance.StopAndReset();
        firstInteractionOccured = false;
    }

    private void StartNextTask(bool doReset)
    {
        if (objectTaskQueue.Count > 0)
        {
            currentTaskParameters = objectTaskQueue.Dequeue();

            if (doReset)
            {
                modelChanger.ResetParameters();
                Debug.Log("RESET PARAMETERS");
            }

            modelChanger.SetTaskInfo(currentTaskParameters);
        }
        else
        {
            FinishUIInteraction();
        }
    }

    private void FinishTutorial()
    {
        isTutorialOn = false;
        hololensCursor.SetActive(true);
        mainManager.StartNextTask();
    }

    private void FinishUIInteraction()
    {
        GetComponent<AudioSource>().PlayOneShot(successSound, 0.5f);
        Destroy(currentUI);
        mainManager.StartNextTask();

        if (currentMode != UIInteractionMode.Gesture)
        {
            hololensCursor.SetActive(true);
        }
    }

    private void SpawnUIAndSetup(GameObject uiToSpawn)
    {
        currentUI = Instantiate(uiToSpawn, targetSpawnPosition.position, targetSpawnPosition.rotation, null);
        modelChanger = currentUI.GetComponentInChildren<IModelChanger>();
    }
}
