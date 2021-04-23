using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LoggingTimer
{
    #region static fields
    private static LoggingTimer logginTimer;
    public static LoggingTimer Instance
    {
        get
        {
            if (logginTimer == null)
            {
                logginTimer = new LoggingTimer();
            }

            return logginTimer;
        }
    }
    #endregion

    private Stopwatch stopwatch;
    private long elapsedTimeInMilliseconds = 0;
    public long ElapsedTimeInMilliseconds => elapsedTimeInMilliseconds;

    private AudioClip logSound;

    public LoggingTimer()
    {
        stopwatch = new Stopwatch();
        logSound = Resources.Load("LogSound") as AudioClip;
    }

    public void Start()
    {
        StopAndReset();
        stopwatch.Start();
    }

    public void TriggerTimestamp()
    {
        elapsedTimeInMilliseconds = stopwatch.ElapsedMilliseconds;
        PlaySoundIndicator();
    }

    private void PlaySoundIndicator()
    {
        if (UnityEngine.Debug.isDebugBuild)
        {
            GameObject.Find("MainManager").GetComponent<AudioSource>().PlayOneShot(logSound);
        }
    }

    public void StopAndReset()
    {
        stopwatch.Stop();
        stopwatch.Reset();
        elapsedTimeInMilliseconds = 0;
    }
}
