using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 60;

    void Awake()
    {
        DontDestroyOnLoad(this);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

   
}
