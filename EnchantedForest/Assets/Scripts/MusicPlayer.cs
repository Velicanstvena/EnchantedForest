using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        int num = FindObjectsOfType<MusicPlayer>().Length;
        if (num > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    { 
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
