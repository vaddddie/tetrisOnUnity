using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioMixerGroup mixer;

    [SerializeField] private RandomChooseScript random_;

    private void Start()
    {
        int temp = PlayerPrefs.GetInt("MusicOwned", 0);

        if (temp == -1)
        {
            temp = random_.RAND_CHOOSE("Music", music.Length);
        }

        source.clip = music[temp];

        if (PlayerPrefs.GetInt("MusicVolume", 1) == 1)
        {
            mixer.audioMixer.SetFloat("MusicVolume", -20);
        }
        
        source.Play();
    }

    public void MusicStart()
    {
        if (PlayerPrefs.GetInt("MusicVolume", 1) == 1)
        {
            StartCoroutine(VolumeLevel());    
        }
    }

    private IEnumerator VolumeLevel()
    {
        float volumeLevel;
        mixer.audioMixer.GetFloat("MusicVolume", out volumeLevel); 

        for (int i = 1; i < 11; i++)
        {
            mixer.audioMixer.SetFloat("MusicVolume", volumeLevel + i * Mathf.Abs(volumeLevel / 10));
            
            yield return new WaitForSeconds(0.05f);
        }
    }

}
