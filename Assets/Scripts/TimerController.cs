using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public GameObject timerText;
    private TextMeshProUGUI Text;

    public float timer;
    public bool tEnabled;

    void Start()
    {

        Text = timerText.GetComponent<TextMeshProUGUI>();
        StartTimer(22.00f);

    }

    void Update()
    {

        if (tEnabled && timer > 0.00f)
        {
            timer = Mathf.Round((timer - Time.deltaTime)*100) / 100;
        }
        else if (tEnabled && timer <= 0.00f)
        {
            timer = 0.00f;
            tEnabled = false;
        }
        Text.text = "<mark=#000000>" + timer.ToString() + "</mark>";

    }
    public void StartTimer(float t)
    {

        timer = t;
        tEnabled = true;

    }

}
