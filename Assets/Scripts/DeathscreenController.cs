using UnityEngine;

public class DeathscreenController : MonoBehaviour
{

    public Canvas deathscreenCanvas;
    public void ShowDeathscreen()
    {

        deathscreenCanvas.enabled = true;

    }

    public void HideDeathscreen()
    {

        deathscreenCanvas.enabled = false;

    }

}
