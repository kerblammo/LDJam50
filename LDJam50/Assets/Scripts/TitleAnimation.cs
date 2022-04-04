using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    public void StartAnimation()
    {
        director.Play();
    }

    public void GoToNextScene()
    {

    }
}
