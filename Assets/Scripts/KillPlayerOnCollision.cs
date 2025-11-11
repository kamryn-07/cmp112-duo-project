using UnityEngine;

public class KillPlayerOnCollision : MonoBehaviour
{

    private GameObject player;
    private PlayerController playerController;

    void Start()
    {

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject == player)
        {
            playerController.KillPlayer();
        }

    }

}
