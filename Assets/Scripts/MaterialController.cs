using Unity.VisualScripting;
using UnityEngine;

public class MaterialController : MonoBehaviour
{

    public Material winpad;
    private float accumulativeTime;

    void Update()
    {

        accumulativeTime += Time.deltaTime;
        winpad.SetVector("_EmissionColor", Color.HSVToRGB(Mathf.Repeat(accumulativeTime / 2.0f, 1.0f), 1, 1 ) * 25);

    }

}
