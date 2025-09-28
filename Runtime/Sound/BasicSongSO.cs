using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Song/BasicSong")]
public class BasicSongSO : SongSO
{
    public SoundSO song;
    public override SongPlayer MakePlayer()
    {
        var player = base.MakePlayer();
        player.GetComponent<SongPlayer_Basic>().SetSongSound(song);

        return player;
    }
}
