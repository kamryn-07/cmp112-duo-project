using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // THE NUMBER
    public float time = 22.0f;
    // THE NUMBER

    public FirstPersonCamera firstPersonCameraController;
    public GameObject player;
    public SfxController sfxController;
    public TimerController timerController;
    public CompletionScreenController completionScreenController;
    public DeathscreenController deathscreenController;
    public Vector3 resetOffset;
    public float respawnTime;
    public float completionTime;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        
        playerRigidbody = player.GetComponent<Rigidbody>();

    }

    public void KillPlayer()
    {

        StartCoroutine(RespawnPlayer());

    }

    private IEnumerator RespawnPlayer()
    {

        timerController.tEnabled = false;
        ResetPlayerPosition();
        FreezePlayer();
        sfxController.OnDeathSfx();
        deathscreenController.ShowDeathscreen();
        yield return new WaitForSeconds(respawnTime);
        firstPersonCameraController.ResetCameraOrientation();
        UnfreezePlayer();
        deathscreenController.HideDeathscreen();
        timerController.StartTimer(time);

    }

    public IEnumerator RepositionPlayer()
    {

        FreezePlayer();
        yield return new WaitForSeconds(completionTime);
        ResetPlayerPosition();
        UnfreezePlayer();

    }

    public void FreezePlayer()
    {

        playerRigidbody.isKinematic = true;

    }

    public void UnfreezePlayer()
    {

        playerRigidbody.isKinematic = false;

    }

    public void ResetPlayerPosition()
    {

        player.transform.position = Vector3.zero + resetOffset;
        firstPersonCameraController.pitch = 0.0f;
        firstPersonCameraController.yaw = 0.0f;

    }

}
