using UnityEngine;
using UnityEngine.InputSystem;

public class GameSettings : MonoBehaviour
{

    public int fpsCap;
    public int qualityVSyncCount;
    public float pollingFrequency;

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
