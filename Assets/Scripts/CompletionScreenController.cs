using System.Collections;
using TMPro;
using UnityEngine;

public class CompletionScreenController : MonoBehaviour
{

    public string winText;
    public Canvas completionScreenCanvas;
    public SfxController sfxController;
    public PlayerController playerController;
    public TimerController timerController;
    public TextMeshProUGUI completionText;

    public void OnLevelComplete()
    {

        
        StartCoroutine(StartCompletionSequence());

    }

    private IEnumerator StartCompletionSequence()
    {

        sfxController.OnLevelCompleteSfx();
        timerController.tEnabled = false;
        completionText.text = winText + "\n[ level completed with: " + timerController.timer.ToString() + " seconds remaining ]";
        completionScreenCanvas.enabled = true;
        StartCoroutine(playerController.RepositionPlayer());
        yield return new WaitForSeconds(playerController.completionTime);
        timerController.StartTimer(playerController.time);
        completionScreenCanvas.enabled = false;
        playerController.UnfreezePlayer();

    }

}
