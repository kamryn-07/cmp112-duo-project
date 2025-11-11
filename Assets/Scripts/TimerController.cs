using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public PlayerController playerController;
    public GameObject timerText;
    private TextMeshProUGUI Text;

    public float timer;
    public bool tEnabled;

    void Start()
    {

        Text = timerText.GetComponent<TextMeshProUGUI>();
        StartTimer(playerController.time);

    }

    void FixedUpdate()
    {

        if (tEnabled && timer > 0.00f)
        {
            timer = Mathf.Round((timer - Time.fixedDeltaTime)*100) / 100;
        }
        else if (tEnabled && timer <= 0.00f)
        {
            timer = 0.00f;
            tEnabled = false;
            playerController.KillPlayer();
        }
        Text.text = "<mark=#000000>" + timer.ToString() + "</mark>";

    }
    public void StartTimer(float t)
    {

        timer = t;
        tEnabled = true;

    }

}
