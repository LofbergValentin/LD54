using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerCustom : MonoBehaviour
{
    private static AudioManagerCustom instance;


    [SerializeField] AudioSource sfxSource, mSource;

    [SerializeField] List<AudioClip> clipList;

    public static AudioManagerCustom Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        mSource.Play();
    }

    public void PlayClip(string clipName)
    {
        AudioClip clipToPlay = clipList.Find(_ => _.name == clipName);
        sfxSource.clip = clipToPlay;
        sfxSource.Play();
    }
}
