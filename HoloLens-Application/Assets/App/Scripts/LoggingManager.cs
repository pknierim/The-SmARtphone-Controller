using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggingManager
{
    #region static fields
    private static LoggingManager loggingManager;
    public static LoggingManager Instance
    {
        get
        {
            if (loggingManager == null)
            {
                loggingManager = new LoggingManager();
            }

            return loggingManager;
        }
    }
    #endregion

    private string currentFilePathGeneric;

    public LoggingManager()
    {
#if UNITY_EDITOR
        Debug.Log("Loggin Directory: " + Application.persistentDataPath);
#endif 
        StartNewLoggingDirectory();
    }

    public void StartNewLoggingDirectory()
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "MT-Logging"));
        var directoryCount = Directory.GetDirectories(Path.Combine(Application.persistentDataPath, "MT-Logging")).Length;
        var newDirectoryPath = Path.Combine(new string[] { Application.persistentDataPath, "MT-Logging/Session-" + directoryCount });
        Directory.CreateDirectory(newDirectoryPath);
        currentFilePathGeneric = Path.Combine(newDirectoryPath, "Logging-" + directoryCount + "-");

        PopupText.Instance.ShowText("MT-Logging/Session-" + directoryCount, 3);
        //CreateFiles(newDirectoryPath);
    }

    public void Log(string data, string captions, LogType logType)
    {
        try
        {
            var fullFilePath = GetFullFilePathForLogType(logType);
            bool fileExists = File.Exists(fullFilePath);
            var writer = new StreamWriter(new FileStream(fullFilePath, FileMode.Append));
            if (!fileExists)
            {
                writer.WriteLine(captions);
            }
            writer.WriteLine(data);
            writer.Flush();
            writer.Close();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private string GetFullFilePathForLogType(LogType logType)
    {
        string fullPath = "";

        switch (logType)
        {
            case LogType.ObjectManipulationGesture:
                fullPath = currentFilePathGeneric + "OMGesture.csv";
                break;
            case LogType.ObjectManipulationSmartphone:
                fullPath = currentFilePathGeneric + "OMSmartphone.csv";
                break;
            case LogType.UIManipulationGesture:
                fullPath = currentFilePathGeneric + "UIMGesture.csv";
                break;
            case LogType.UIManipulationSmartphoneImmersive:
                fullPath = currentFilePathGeneric + "UIMImmersive.csv";
                break;
            case LogType.UIManipulationSmartphoneNonImmersive:
                fullPath = currentFilePathGeneric + "UIMNonImmersive.csv";
                break;
        }

        return fullPath;
    }

    public enum LogType
    {
        ObjectManipulationGesture, ObjectManipulationSmartphone,
        UIManipulationGesture, UIManipulationSmartphoneImmersive, UIManipulationSmartphoneNonImmersive
    }
}
