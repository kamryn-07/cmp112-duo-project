using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{

    GameObject cooldownBar;
    Image cdImage;

    private void Start()
    {

        cooldownBar = GameObject.Find("CooldownBar");
        cdImage = cooldownBar.GetComponent<Image>();

    }

    // controls the cooldown bar in relation to the utility activated by the player (this method is called by the player's movement script to be exact)
    IEnumerator InitiateCooldown(float cd)
    {

        cdImage.fillAmount = 0f;
        int i = 1;
        float iMax = 60f;
        while (i < iMax+1)
        {
            UnityEngine.Debug.Log(i / iMax);
            cdImage.fillAmount = i / iMax;
            i++;
            yield return new WaitForSecondsRealtime(cd / (iMax*1.04f));
        }

    }

}
