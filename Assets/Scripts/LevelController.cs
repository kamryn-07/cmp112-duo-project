using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    private string winScene = "WinScreen";
    public float gameEndTime = 1.5f;
    public SfxController sfxController;
    public BlackscreenController blackscreenController;
    public GameObject[] levelList;
    public int index = 0;

    private void Start()
    {

        LoadLevel();

    }

    public void OnLevelComplete()
    {

        index++;
        DeloadLevel();
        LoadLevel();

    }

    private void LoadLevel()
    {

        if (levelList.Length == index)
        {
            StartCoroutine(EndGame());
        }
        else
        {
            levelList[index].gameObject.SetActive(true);
        }

    }

    private void DeloadLevel()
    {

        levelList[index - 1].gameObject.SetActive(false);

    }

    private IEnumerator EndGame()
    {

        blackscreenController.ShowBlackscreen();
        sfxController.OnGameCompletionSfx();
        yield return new WaitForSeconds(gameEndTime);
        SceneManager.LoadScene(winScene);

    }

}
