using UnityEngine;

public class WinpadController : MonoBehaviour
{

    private GameObject LevelManager;
    private LevelController levelController;
    private GameObject player;
    private PlayerController playerController;
    private CompletionScreenController completionScreenController;

    void Start()
    {

        LevelManager = GameObject.Find("LevelManager");
        levelController = LevelManager.GetComponent<LevelController>();
        completionScreenController = GameObject.Find("LevelCompleteScreenCanvas").GetComponent<CompletionScreenController>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject == player)
        {
            completionScreenController.OnLevelComplete();
            levelController.OnLevelComplete();
        }

    }

}
