using UnityEngine;

public class LevelController : MonoBehaviour
{

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

        levelList[index].gameObject.SetActive(true);

    }

    private void DeloadLevel()
    {

        levelList[index - 1].gameObject.SetActive(false);

    }

}
