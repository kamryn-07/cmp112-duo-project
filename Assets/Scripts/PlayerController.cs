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
    public BlackscreenController blackscreenController;
    public Vector3 resetOffset;
    public float respawnTime;
    public float completionTime;
    private Rigidbody playerRigidbody;
    private bool floatDown = true;
    private float cumulativeTime;
    private float killY = -65.0f;

    private void Start()
    {
        
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.position = Vector3.zero + new Vector3(0, 100, 0);
        blackscreenController.FadeBlackscreenOff(20.0f); // arbitrary time numbers for the fade in effect, too lazy dude, oh my god
        FreezePlayer();

    }

    private void Update()
    {
        
        cumulativeTime += Time.deltaTime;
        if (floatDown && cumulativeTime < 11.50f) // this one too
        {
            playerRigidbody.position -= new Vector3(0, Time.deltaTime * 4, 0);
        }
        else if (floatDown)
        {
            floatDown = false;
            timerController.StartTimer(time);
            UnfreezePlayer();
        }
        if (playerRigidbody.position.y < killY) // hey
        {
            KillPlayer();
        }

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
