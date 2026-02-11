using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsInAnimationEvent : MonoBehaviour
{
    //Attach this script to an object with an animator controller
    //Call its function when you want to have animation events that emit a sound in a given frame
    //DO NOT loop sounds in animation events
    private AudioSource thisAudioSource;

    void Start()
    {
        thisAudioSource = GetComponentInChildren<AudioSource>();
        if(thisAudioSource == null)
        {
            thisAudioSource = GetComponentInParent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip soundClip)
    {
        thisAudioSource.PlayOneShot(soundClip);
    }

    // public void PlayRandomSound(AudioBankTemplate audioBank)
    // {
    //     thisAudioSource.PlayOneShot(audioBank.PickRandom());
    // }
}
