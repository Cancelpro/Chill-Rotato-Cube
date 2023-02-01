using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] clips;
    AudioSource audioSource;
    public AudioClip win;
    AudioSource winSound;
    public AudioMixer mixer;
    float timer = 0;
    int songplaying = 0;
    bool shouldShowAudioUI = true;
    public static AudioHandler instance;
    void Awake()
    {

        if(instance != null)
        {
            Object.Destroy(gameObject);
        }
        else
        {
            instance = this;
            winSound = GetComponent<AudioSource>();
            audioSource = transform.GetChild(0).GetComponent<AudioSource>();

            DontDestroyOnLoad(this.gameObject);

        }

    }

    // Update is called once per frame
    void Update()
    {

        if(!audioSource.isPlaying)
        {
            songplaying++;
            if(songplaying >= 5)
            {
                songplaying = 0;
            }
            audioSource.PlayOneShot(clips[songplaying]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.GetChild(1).gameObject.SetActive(shouldShowAudioUI);
            transform.GetChild(1).GetComponent<Canvas>().worldCamera = Camera.main;
            shouldShowAudioUI = !shouldShowAudioUI;
        }

    }

    public void SetLevel(float volume)
    {
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void PlayFanFare()
    {
        audioSource.PlayOneShot(win);
    }

}
