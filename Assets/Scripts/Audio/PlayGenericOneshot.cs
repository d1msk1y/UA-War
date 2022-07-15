using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayGenericOneshot : MonoBehaviour
{
    [SerializeField] private EventReference soundEvent;
    [SerializeField] private bool _playOnStart;

    private void Start()
    {
        if (_playOnStart)
            PlaySoundEvent();
    }

    public void PlaySoundEvent()
    {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
