using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource musicAudio;

    public void StopMusic()
    {

        musicAudio.Stop();

    }

}
