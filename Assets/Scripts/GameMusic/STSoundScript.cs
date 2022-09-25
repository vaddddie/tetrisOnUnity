using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STSoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] numbersSound = new AudioClip[3];
    [SerializeField] private AudioClip[] goSound;

    [SerializeField] private AudioSource source;

    public void NumbersSoundPlay(int index){
        source.clip = numbersSound[index - 1];
        source.Play();
    }

    public void GoSoundPlay(){
        source.clip = goSound[Random.Range(0, goSound.Length)];
        source.Play();
    }
}
