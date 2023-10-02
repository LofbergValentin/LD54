using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerCustom : MonoBehaviour
{

    [SerializeField] AudioSource sfxSource, mSource;

    [SerializeField] List<AudioClip> clipList;

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
