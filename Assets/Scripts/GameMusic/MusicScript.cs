using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioSource source;

    [SerializeField] private RandomChooseScript random_;

    void Start()
    {
        int temp = PlayerPrefs.GetInt("MusicOwned", 0);

        if (temp == -1)
        {
            temp = random_.RAND_CHOOSE("Music", music.Length);
        }

        source.clip = music[temp];
        source.Play();
    }

}
