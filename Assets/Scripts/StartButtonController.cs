using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{

    private string sceneName = "GameScene";
    private bool moveOffScreen = false;
    private float cumulitiveTime;
    public SfxController sfxController;
    public RectTransform elementsTransform;
    public GameObject titleScreenUI;
    public GameObject prestartUI;
    public GameObject poststartUI;
    public float waitTime;

    private void Update()
    {
        
        if (moveOffScreen)
        {
            cumulitiveTime += Time.deltaTime/10;
            elementsTransform.position -= new Vector3(cumulitiveTime, 0, 0);
        }

    }

    public void OnClick()
    {

        if (moveOffScreen) return;

        moveOffScreen = true;
        sfxController.OnLevelCompleteSfx();
        StartCoroutine(SwitchScene());
        poststartUI.SetActive(true);
        prestartUI.SetActive(false);

    }

    private IEnumerator SwitchScene()
    {

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);

    }

}
