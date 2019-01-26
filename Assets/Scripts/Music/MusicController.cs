using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource MusicAudioSource;
    public List<AudioClip> MusicClips;

    private int currentClipId;
    
    void Start()
    {
        LoadClipsById(0);
    }

    void Update()
    {
        if (MusicAudioSource.time >= MusicClips[currentClipId].length)
        {
            currentClipId = (currentClipId + 1) % MusicClips.Count;
            LoadClipsById(currentClipId); 
        }
    }

    private void LoadClipsById(int clipId)
    {
        MusicAudioSource.clip = MusicClips[clipId];
        MusicAudioSource.Play();
    }
}