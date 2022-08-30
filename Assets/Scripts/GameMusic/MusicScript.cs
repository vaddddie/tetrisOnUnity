using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private new AudioClip[] music;
    [SerializeField] private AudioSource source;

    void Start()
    {
        source.clip = music[PlayerPrefs.GetInt("MusicOwned", 0)];
        source.Play();
    }

}
