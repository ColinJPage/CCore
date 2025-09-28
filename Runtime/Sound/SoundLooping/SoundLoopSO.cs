using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLoopSO", menuName = "SO/Sound/SoundLoop", order = 1)]
public class SoundLoopSO : ScriptableObject
{
    public AnimationCurve volumeCurve;
    [Range(0f, 1f)]
    public float startStagger = 0.5f;
}
