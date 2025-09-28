using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SongPlayer : MonoBehaviour
{
    protected float volume = 1f;
    public abstract void StartSong();
    public virtual void SetSongVolume(float volume)
    {
        this.volume = volume;
    }
}
