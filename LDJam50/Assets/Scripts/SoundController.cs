using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer sfx;
    [SerializeField] TextMeshProUGUI buttonLabel;
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
            UpdateButtonLabel(true);
            volume = 0;
        }
        else
        {
            UpdateButtonLabel(false);
            volume = -80f;
        }

        sfx.SetFloat("Volume", volume);
    }

    void UpdateButtonLabel(bool soundIsOn)
    {
        // if sound is on, the label should give the player the option to turn it off
        string state = soundIsOn ? "off" : "on";
        string label = $"Turn {state} the store radio!";
        buttonLabel.text = label;
    }
}
