using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource MusicAudioSource;
    public List<AudioClip> MusicClips;

    public List<float> VolumeLevels;

    private int volumeLevelId;
    private int currentClipId;

    public List<int> NeighboursPointsVolume;

    private ActionRepeater actionRepeater;
    
    void Start()
    {
        LoadClipsById(0);
        actionRepeater = new ActionRepeater(() => 1, () =>
        {
            LevelPoints.Instance.AddNeighboursPoints(NeighboursPointsVolume[volumeLevelId]);
        });
    }

    void Update()
    {
        if (MusicAudioSource.time >= MusicClips[currentClipId].length)
        {
            currentClipId = (currentClipId + 1) % MusicClips.Count;
            LoadClipsById(currentClipId); 
        }
        actionRepeater.Tick(Time.deltaTime);
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
        volumeLevelId = 0;
        MusicAudioSource.volume = VolumeLevels[volumeLevelId];
    }
}