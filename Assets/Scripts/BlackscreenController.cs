using UnityEngine;
using UnityEngine.UI;

public class BlackscreenController : MonoBehaviour
{

    public Canvas blackscreenCanvas;
    public RawImage blackscreenImage;
    private float fade = 0.0f;
    private float fadeValue = 0.0f;

    private void Update()
    {
        
        if (fade < fadeValue)
        {
            fade += Time.deltaTime;
            blackscreenImage.color = new Color(0, 0, 0, (fadeValue-fade)/fadeValue);
        }

    }

    public void FadeBlackscreenOff(float t)
    {

        blackscreenCanvas.enabled = true;
        fade = 0;
        fadeValue = t;

    }

    public void ShowBlackscreen()
    {

        blackscreenImage.color = new Color(0, 0, 0, 1);
        blackscreenCanvas.enabled = true;

    }

    public void HideBlackscreen()
    {

        blackscreenCanvas.enabled = true;

    }

}
