using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public Dictionary<string, AudioClip> soundDictionary;

    void Start()
    {
        soundDictionary = new Dictionary<string, AudioClip>();
        soundDictionary["BushDown"] = Resources.Load<AudioClip>("Audio/Bush/BushDown");
        soundDictionary["BushHit1"] = Resources.Load<AudioClip>("Audio/Bush/BushHit1");


        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Play(string soundKey)
    {
        if (soundDictionary.ContainsKey(soundKey))
        {
            audioSource.PlayOneShot(soundDictionary[soundKey]);
        }
        else
        {
            Debug.LogError("Sound key not found: " + soundKey);
        }
    }

    public void PlayRandom(List<string> soundKeys)
    {
        if (soundKeys.Count > 0)
        {
            string randomKey = soundKeys[Random.Range(0, soundKeys.Count)];
            Play(randomKey);
        }
    }
}
