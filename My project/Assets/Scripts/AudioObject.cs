using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public Action SoundDone;

    public IEnumerator SoundPlayed()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (audioSource.loop == false)
        {
            SoundDone?.Invoke();
        }
    }
}