using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource thisAudioSource;
    // Start is called before the first frame update

    private void Awake()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

        AudioClip audioclip = audioClips[Random.Range(0, audioClips.Count)];
        thisAudioSource.PlayOneShot(audioclip);
    }


}
