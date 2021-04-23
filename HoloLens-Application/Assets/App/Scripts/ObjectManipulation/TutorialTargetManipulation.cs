using Google.Protobuf.HoloLensAndroidMessaging;
using UnityEngine;

public class TutorialTargetManipulation
{
    private MainManager mainManager;
    private TargetManipulationManager targetManipulationManager;
    private TargetManipulationManager.ManipulationType mode;

    private GameObject currentTarget;
    private GameObject targetGoal;
    private FinishTargetManipulationTutorialHandling finishTutorialHandling;
    private BluetoothManager bluetoothManager;
    private Transform targetSpawnPosition;

    public TutorialTargetManipulation(MainManager mainManager, TargetManipulationManager targetManipulationManager, BluetoothManager bluetoothManager, TargetManipulationManager.ManipulationType mode)
    {
        this.mainManager = mainManager;
        this.targetManipulationManager = targetManipulationManager;
        this.mode = mode;
        this.bluetoothManager = bluetoothManager;

        targetSpawnPosition = GameObject.Find("TargetSpawnPosition").transform;

        targetGoal = GameObject.Instantiate(Resources.Load("TutorialTargetGoal"), GameObject.Find("TargetGoalTutorialPosition").transform.position, targetSpawnPosition.rotation * Quaternion.Euler(0,180,0)) as GameObject;
        finishTutorialHandling = (GameObject.Instantiate(Resources.Load("FinishTargetTutorialHandling"), GameObject.Find("FinishTargetTutorialPosition").transform.position, targetSpawnPosition.rotation) as GameObject)
            .GetComponent<FinishTargetManipulationTutorialHandling>();
        SpawnNewTarget();
    }

    private void SpawnNewTarget()
    {
        if (currentTarget != null)
        {
            GameObject.Destroy(currentTarget);
        }

        if (mode == TargetManipulationManager.ManipulationType.Smartphone)
        {
            currentTarget = GameObject.Instantiate(Resources.Load("TutorialTargetSmartphone"), GameObject.Find("TargetSpawnPosition").transform.position, targetSpawnPosition.rotation * Quaternion.Euler(0, 180, 0)) as GameObject;

        }
        else if (mode == TargetManipulationManager.ManipulationType.Gesture)
        {
            currentTarget = GameObject.Instantiate(Resources.Load("TutorialTargetGesture"), GameObject.Find("TargetSpawnPosition").transform.position, targetSpawnPosition.rotation * Quaternion.Euler(0, 180, 0)) as GameObject;
        }
    }

    public void HandleManipulationInput(ObjectManipulation manipulation)
    {
        if (manipulation.IsSelecting == 1)
        {
            currentTarget.GetComponent<IManipulationHandling>().NotifyAboutSelection();
            targetGoal.GetComponent<TargetGoal>().Show();
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.ObjectForManipulationSelected);
        }
        else if (manipulation.IsFinished == 1)
        {
            FinishCurrentTarget();
        }
        else
        {
            if (currentTarget != null)
            {
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

    public void FinishCurrentTarget()
    {
        mainManager.GetComponent<AudioSource>().Play();

        // if finish tutorial invoked
        if (finishTutorialHandling.IsTriggered)
        {
            GameObject.Destroy(currentTarget);
            FinishTutorial();
        }
        else
        {
            SpawnNewTarget();
            mainManager.SendMessageToSmartphone(HoloLensMessage.Types.Message.ObjectManipulationFinished);
        }
    }

    private void FinishTutorial()
    {
        mainManager.StartNextTask();
        targetManipulationManager.FinishTutorial();
        GameObject.Destroy(targetGoal);
        GameObject.Destroy(finishTutorialHandling.gameObject);
    }

}
