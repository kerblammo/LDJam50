using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer sfx;
    private void Awake()
    {
        int numSoundControllers = FindObjectsOfType<SoundController>().Length;
        if (numSoundControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PauseMusic()
    {
        float volume;
        sfx.GetFloat("Volume", out volume);

        if (volume <= -80f)
        {
            volume = 0;
        }
        else
        {
            volume = -80f;
        }

        sfx.SetFloat("Volume", volume);
    }
}
