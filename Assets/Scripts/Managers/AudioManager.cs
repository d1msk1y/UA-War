using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [Header("Shop")]
    public EventReference withdraw;

    [Header("Clips")]
    public EventReference russianWarsip;

    [Header("SFX")]
    public EventReference rejectSFX;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundEvent(EventReference soundEvent)
    {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
