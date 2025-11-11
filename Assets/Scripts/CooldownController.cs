using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{

    private int maxHeight = 75;
    public GameObject cooldownBar;
    RectTransform cdRectTransform;

    void Start()
    {

        cdRectTransform = cooldownBar.GetComponent<RectTransform>();

    }

    // controls the cooldown bar in relation to the utility activated by the player (this method is called by the player's movement script to be exact)
    public IEnumerator InitiateCooldown(float cd)
    {

        float i = 1f;
        float iMax = 60f;
        cdRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.0f);
        while (i < iMax+1)
        {
            cdRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (maxHeight/iMax)*i);
            i++;
            yield return new WaitForSecondsRealtime(cd / (iMax*1.15f));
        }

    }

}
