using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SongPlayer_Basic : SongPlayer
{
    private AudioSource source;
    private float maxVolume = 1f;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    //void PlaySoundAsSong(SoundSO soundSO)
    //{
    //    source = SoundManager.Play(soundSO);
    //    source.transform.SetParent()
    //}
    private void OnDisable()
    {
        source.DOKill();
    }
    public override void SetSongVolume(float volume)
    {
        base.SetSongVolume(volume);
        //source.volume = maxVolume * volume;
        source.DOFade(maxVolume * volume, 2f).SetUpdate(true);
        //print("set basic song volume " + volume);
    }

    public override void StartSong()
    {
        throw new System.NotImplementedException();
    }

    public void SetSongSound(SoundSO song)
    {
        //print("set song " + song.name);
        source.clip = song.Clip;
        maxVolume = song.Volume;
        source.volume = song.Volume;
        source.Play();
        source.outputAudioMixerGroup = song.group;
    }
}
