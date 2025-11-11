using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public float loopSpeed;
    public float startX;
    public float startY;
    public float loopY;
    public float loopX;

    void Start()
    {

        textMesh.rectTransform.localPosition = new Vector3(startX, startY, 0);

    }

    void Update()
    {

        if (loopY > 0)
        {
            textMesh.rectTransform.localPosition += new Vector3(0, loopSpeed * Time.deltaTime, 0);
            if (textMesh.rectTransform.localPosition.y >= loopY)
            {
                textMesh.rectTransform.localPosition = new Vector3(startX, startY, 0);
            }
        }
        else
        {
            textMesh.rectTransform.localPosition += new Vector3(loopSpeed * Time.deltaTime, 0, 0);
            if (textMesh.rectTransform.localPosition.x >= loopX)
            {
                textMesh.rectTransform.localPosition = new Vector3(startX, startY, 0);
            }
        }

    }

}
