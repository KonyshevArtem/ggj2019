using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Television : MonoBehaviour
{
    public VideoPlayer VideoPlayer;

    public List<VideoClip> VideoClips;
    public VideoClip Noise;

    private int currentChannel;

    
    public void SwitchChannel()
    {
        currentChannel = (currentChannel + 1) % VideoClips.Count;
        PlayClip(VideoClips[currentChannel]);
    }

    public void PlayNoise()
    {
        PlayClip(Noise);
    }

    public void PlayClip(VideoClip videoClip)
    {
        VideoPlayer.clip = videoClip;
        VideoPlayer.Play();
    }
}