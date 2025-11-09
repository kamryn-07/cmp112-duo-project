using UnityEngine;
using UnityEngine.InputSystem;

public class GameSettings : MonoBehaviour
{

    public int fpsCap = 0;
    public int qualityVSyncCount = 1;
    public float pollingFrequency = 1000.0f;

    void Start()
    {

        UpdateSettings();

    }
    public void UpdateSettings()
    {

        QualitySettings.vSyncCount = qualityVSyncCount;
        InputSystem.pollingFrequency = pollingFrequency;

        if (fpsCap > 0)
        {
            Application.targetFrameRate = fpsCap;
        }

    }

}
