using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DJ : MonoBehaviour
{
    private static DJ _instance;
    public static DJ Instance
    {
        get
        {
            if (_instance == null)
            {
                var s = new GameObject("DJ");
                _instance = s.AddComponent<DJ>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    public static void PlaySong(SongSO song)
    {
        Instance.PlaySongInstance(song);
    }

    public static void SetSongVolume(float volume)
    {
        Instance.songVolume = volume;
        Instance.currentSongPlayer?.SetSongVolume(volume);

    }
    public static float SongVolume
    {
        get
        {
            return Instance.songVolume;
        }
        set
        {
            SetSongVolume(value);
        }
    }


    private SongPlayer currentSongPlayer;
    private SongSO currentSong;
    private float songVolume = 1f;


    private void PlaySongInstance(SongSO song)
    {
        SetSongVolume(1f);

        if (song == currentSong) return;

        if (currentSongPlayer)
        {
            SetSongVolume(0f);
            Destroy(currentSongPlayer.gameObject, 2f);
        }

        currentSong = song;
        //print("Setting song to " + song.name);

        if (song == null) return;

        currentSongPlayer = song.MakePlayer();
        currentSongPlayer.transform.SetParent(transform);
    }
}
