using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public float loopSpeed;
    private float startX = 650.0f;
    private float startY = 10.0f;
    private float loopY = 100.0f;

    void Start()
    {

        textMesh.rectTransform.localPosition = new Vector3(startX, startY, 0);

    }

    void Update()
    {

        textMesh.rectTransform.localPosition += new Vector3(0, loopSpeed * Time.deltaTime, 0);
        if (textMesh.rectTransform.localPosition.y >= loopY)
        {
            textMesh.rectTransform.localPosition = new Vector3(startX, startY, 0);
        }

    }

}
