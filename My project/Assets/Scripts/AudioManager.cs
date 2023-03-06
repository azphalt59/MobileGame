using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    public List<GameObject> playingAudio = new List<GameObject>(100);
    public List<GameObject> notPlayingAudio = new List<GameObject>(100);
    public GameObject AudioObject;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        foreach (Sound s in sounds)
        {
            GameObject audioObject = Instantiate(AudioObject, transform);
            notPlayingAudio.Add(audioObject);
            s.source = audioObject.GetComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            audioObject.GetComponent<AudioObject>().SoundDone += () => playingAudio.Remove(audioObject);
            audioObject.GetComponent<AudioObject>().SoundDone += () => notPlayingAudio.Add(audioObject);
            audioObject.GetComponent<AudioObject>().SoundDone += () => audioObject.SetActive(false);
            audioObject.SetActive(false);

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            GameObject NewAudioObject = null;
            foreach (var audioObject in notPlayingAudio)
            {
                if (s.source.clip == audioObject.GetComponent<AudioSource>().clip && audioObject.GetComponent<AudioSource>().isPlaying == false)
                {
                    audioObject.SetActive(true);
                    notPlayingAudio.Remove(audioObject);
                    playingAudio.Add(audioObject);
                    NewAudioObject = audioObject;
                    break;
                }
            }

            if (NewAudioObject == null)
            {
                NewAudioObject = Initialize(s);
            }

            if (s == null)
            {
                Debug.LogError("Sound :" + name + " doesn't exist");
                return;
            }
            s.source.Play();
            StartCoroutine(NewAudioObject.GetComponent<AudioObject>().SoundPlayed());
        }
        else
        {
            Debug.LogError("No sound by the name '" + name + "' found");
        }
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound :" + name + " doesn't exist");
            return;
        }
        s.source.Stop();
        s.source.gameObject.GetComponent<AudioObject>().SoundDone?.Invoke();
    }

    private GameObject Initialize(Sound s)
    {
        GameObject audioObject = Instantiate(AudioObject, transform);
        notPlayingAudio.Add(audioObject);
        s.source = audioObject.GetComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;

        audioObject.GetComponent<AudioObject>().SoundDone += () => playingAudio.Remove(audioObject);
        audioObject.GetComponent<AudioObject>().SoundDone += () => notPlayingAudio.Add(audioObject);
        audioObject.GetComponent<AudioObject>().SoundDone += () => audioObject.SetActive(false);

        return audioObject;
    }

}
