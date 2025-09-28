using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Song")]
public class SongSO : ScriptableObject
{
    [SerializeField] GameObject songPlayer;

    public void Play()
    {
        DJ.PlaySong(this);
    }
    public virtual SongPlayer MakePlayer()
    {
        return Instantiate(songPlayer).GetComponent<SongPlayer>();
    }
}
