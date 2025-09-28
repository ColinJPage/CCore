using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    [SerializeField]
    SongSO song;

    private void Start()
    {
        DJ.PlaySong(song);
    }


}
