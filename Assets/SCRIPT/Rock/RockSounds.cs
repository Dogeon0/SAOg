using System.Collections.Generic;
using UnityEngine;

public class RockSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public Dictionary<string, AudioClip> soundDictionary;

    void Start()
    {
        soundDictionary = new Dictionary<string, AudioClip>();

        // Load your sounds here
        // Example: Assuming your sound files are in a Resources/Sounds foldrock
        soundDictionary["RockDown"] = Resources.Load<AudioClip>("Audio/rock/RockDown");
        soundDictionary["rockhit1"] = Resources.Load<AudioClip>("Audio/rock/rockhit1");


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
