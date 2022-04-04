using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PASystem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip chime;
    [SerializeField] List<AudioClip> systemLines;
    int cachedRandom = 0;

    public void PlayRandomLine()
    {
        StartCoroutine(AwaitLinePlaying());
    }

    IEnumerator AwaitLinePlaying()
    {
        yield return new WaitForEndOfFrame();
        if (audioSource.isPlaying)
        {
            yield break;
        }
        else
        {
            audioSource.PlayOneShot(chime);
            yield return new WaitForSeconds(chime.length);
            int random;
            do
            {
                random = Random.Range(0, systemLines.Count);
            } while (cachedRandom == random);
            AudioClip clip = systemLines[random];
            audioSource.PlayOneShot(clip);
        }
    }
}
