using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // THE NUMBER
    const float time = 22.0f;
    // THE NUMBER

    public GameObject player;
    public SfxController sfxController;
    public TimerController timerController;
    public DeathscreenController deathscreenController;
    public float respawnTime;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        
        playerRigidbody = player.GetComponent<Rigidbody>();

    }

    public void KillPlayer()
    {

        timerController.tEnabled = false;
        player.transform.position = Vector3.zero + new Vector3(0, 10, 0);
        playerRigidbody.isKinematic = true;
        sfxController.OnDeathSfx();
        deathscreenController.ShowDeathscreen();
        StartCoroutine(RespawnPlayer());

    }

    private IEnumerator RespawnPlayer()
    {

        yield return new WaitForSeconds(respawnTime);
        playerRigidbody.isKinematic = false;
        deathscreenController.HideDeathscreen();
        timerController.StartTimer(time);

    }

}
