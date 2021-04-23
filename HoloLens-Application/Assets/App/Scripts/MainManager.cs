using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Google.Protobuf.HoloLensAndroidMessaging;
using Google.Protobuf;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bluetoothToggleButton;

    [SerializeField]
    private TargetManipulationManager manipulationManager;

    [SerializeField]
    private UIInteractionManager uIInteractionManager;

    [SerializeField]
    private GameObject taskTypeButtons;

    [SerializeField]
    private AudioClip successSound;

    private BluetoothManager bluetoothManager;

    private TaskType taskType;
    private ManipulationType currentMode;
    private Queue<Action> taskActions;
    private Type currentMessageType = null;

    void Start()
    {
        var loggingManager = LoggingManager.Instance;
        bluetoothManager = GetComponent<BluetoothManager>();
        bluetoothManager.BluetoothConnectionEstablished += OnBluetoothConnectionEstablished;

        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
    }

    private void OnBluetoothConnectionEstablished(object sender, EventArgs eventArgs)
    {
        bluetoothToggleButton.SetActive(false);
        taskTypeButtons.SetActive(true);
        SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseClear);
    }

    public void StartObjectManipulation_Gesture()
    {
        taskActions = new Queue<Action>();

        taskActions.Enqueue(() => { manipulationManager.StartTutorial(TargetManipulationManager.ManipulationType.Gesture); PopupText.Instance.ShowText("Übung gestartet", 2.0f); currentMessageType = typeof(ObjectManipulation); });
        taskActions.Enqueue(() => { manipulationManager.StartManipulationTasks(TargetManipulationManager.ManipulationType.Gesture); PopupText.Instance.ShowText("Aufgabe gestartet", 2.0f); });
        StartNextTask();
    }

    public void StartObjectManipulation_Smartphone()
    {
        taskActions = new Queue<Action>();

        taskActions.Enqueue(() => { manipulationManager.StartTutorial(TargetManipulationManager.ManipulationType.Smartphone); PopupText.Instance.ShowText("Übung gestartet", 2.0f); currentMessageType = typeof(ObjectManipulation); });
        taskActions.Enqueue(() => { manipulationManager.StartManipulationTasks(TargetManipulationManager.ManipulationType.Smartphone); PopupText.Instance.ShowText("Aufgabe gestartet", 2.0f); });
        StartNextTask();
    }

    public void StartUIManipulation_Gesture()
    {
        taskActions = new Queue<Action>();

        taskActions.Enqueue(() => { uIInteractionManager.StartTutorial(UIInteractionManager.UIInteractionMode.Gesture); PopupText.Instance.ShowText("Übung gestartet", 2.0f); currentMessageType = typeof(UIManipulation); });
        taskActions.Enqueue(() => { uIInteractionManager.StartUIInteraction(UIInteractionManager.UIInteractionMode.Gesture); PopupText.Instance.ShowText("Aufgabe gestartet", 2.0f); });
        StartNextTask();
    }

    public void StartUIManipulation_SmartphoneImmersive()
    {
        taskActions = new Queue<Action>();

        taskActions.Enqueue(() => { uIInteractionManager.StartTutorial(UIInteractionManager.UIInteractionMode.SmartphoneImmersive); PopupText.Instance.ShowText("Übung gestartet", 2.0f); currentMessageType = typeof(UIManipulation); });
        taskActions.Enqueue(() => { uIInteractionManager.StartUIInteraction(UIInteractionManager.UIInteractionMode.SmartphoneImmersive); PopupText.Instance.ShowText("Aufgabe gestartet", 2.0f); });
        StartNextTask();
    }

    public void StartUIManipulation_SmartphoneNonImmersive()
    {
        taskActions = new Queue<Action>();

        taskActions.Enqueue(() => { uIInteractionManager.StartTutorial(UIInteractionManager.UIInteractionMode.SmartphoneNonImmersive); PopupText.Instance.ShowText("Übung gestartet", 2.0f); currentMessageType = typeof(UIManipulation); });
        taskActions.Enqueue(() => { uIInteractionManager.StartUIInteraction(UIInteractionManager.UIInteractionMode.SmartphoneNonImmersive); PopupText.Instance.ShowText("Aufgabe gestartet", 2.0f); });
        StartNextTask();
    }

    public void OnSpawnPositionSetupFinished()
    {
        bluetoothToggleButton.SetActive(true);
    }

    public void StartNextTask()
    {
        if (taskActions.Count != 0)
        {
            var task = taskActions.Dequeue();
            task.Invoke();
        }
        else
        {
            taskTypeButtons.SetActive(true);
            PopupText.Instance.ShowText("Alle Aufgaben abgeschlossen!", 3.0f);
            GetComponent<AudioSource>().PlayOneShot(successSound);
            SendMessageToSmartphone(HoloLensMessage.Types.Message.UseCaseClear);
            // reset targetManipualtionManager
            // reset UIInteractionManager
        }
    }

    public void SendMessageToSmartphone(HoloLensMessage.Types.Message message)
    {
        HoloLensMessage messageToSend = new HoloLensMessage()
        {
            Message = message
        };

        bluetoothManager.SendMessageToSmartphone(messageToSend.ToByteArray());
    }

    public void ProcessMessageFromAndroid(byte[] message)
    {
        Debug.Assert(currentMessageType != null, "Current message type should not be null when this method is called.");

        try
        {
            if (currentMessageType == typeof(ObjectManipulation))
            {
                var objectManipulation = ObjectManipulation.Parser.ParseFrom(message);
                manipulationManager.HandleManipulationInput(objectManipulation);
            }
            else if (currentMessageType == typeof(UIManipulation))
            {
                var uiManipulation = UIManipulation.Parser.ParseFrom(message);
                uIInteractionManager.HandleManipulationInput(uiManipulation);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("MessagePackSerializer Parse failed with error: " + ex.Message);
        }
    }
}

public enum TaskType
{
    ObjectManipulation, UIManipulation
}


