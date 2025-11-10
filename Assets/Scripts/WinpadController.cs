using UnityEngine;

public class WinpadController : MonoBehaviour
{

    private GameObject player;
    private PlayerController playerController;
    private CompletionScreenController completionScreenController;

    void Start()
    {

        completionScreenController = GameObject.Find("LevelCompleteScreenCanvas").GetComponent<CompletionScreenController>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject == player)
        {
            completionScreenController.OnLevelComplete();
        }

    }

}
