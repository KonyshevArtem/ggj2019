using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource MusicAudioSource;
    public List<AudioClip> MusicClips;

    public List<float> VolumeLevels;

    private int volumeLevelId;
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

    public void IncrementVolume()
    {
        volumeLevelId = Mathf.Min(volumeLevelId + 1, VolumeLevels.Count - 1);
        MusicAudioSource.volume = VolumeLevels[volumeLevelId];
    }

    public void DecrementVolume()
    {
        volumeLevelId = Mathf.Max(volumeLevelId - 1, 0);
        MusicAudioSource.volume = VolumeLevels[volumeLevelId];
    }
}