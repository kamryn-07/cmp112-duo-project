using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{

    public MusicController musicController;
    public AudioSource[] sfxArray;
    public AudioSource jumpSfx;
    public AudioSource landSfx;
    public AudioSource whooshSfx;
    public AudioSource explosionSfx;
    public AudioSource gongSfx;
    public AudioSource clickSfx;
    public AudioSource toneSfx;
    public float sfxVolume;

    void Start()
    {

        UpdateSettings();

    }
    private void UpdateSettings()
    {

        foreach (AudioSource sound in sfxArray)
        {
            sound.volume = sfxVolume;
        }

    }

    // play sfx public methods
    public void OnJumpSfx()
    {

        RandomiseAudioProperties(jumpSfx);
        jumpSfx.Play();

    }
    public void OnLandSfx()
    {

        RandomiseAudioProperties(landSfx);
        landSfx.Play();

    }

    public void OnUtilitySfx()
    {

        RandomiseAudioProperties(whooshSfx);
        whooshSfx.Play();

    }


    public void OnDeathSfx()
    {

        RandomiseAudioProperties(explosionSfx);
        explosionSfx.Play();

    }

    public void OnLevelCompleteSfx()
    {

        gongSfx.Play();

    }

    public void OnClickSfx()
    {

        clickSfx.Play();

    }

    public void OnGameCompletionSfx()
    {

        musicController.StopMusic();
        toneSfx.Play();
        gongSfx.Stop();

    }

    // randomise for variety lol
    private void RandomiseAudioProperties(AudioSource audio)
    {

        audio.pitch = Random.Range(0.65f, 1.0f);
        audio.panStereo = Random.Range(-0.25f, 0.25f);
        audio.volume = Random.Range(sfxVolume - (sfxVolume / 3.0f), sfxVolume + (sfxVolume / 3.0f));

    }

}
