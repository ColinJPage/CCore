using UnityEngine;
using System;

public class MathUtils
{
    public static float SigFigs(float v, int digits)
    {
        if (v == 0f)
            return 0f;

        float scale = Mathf.Pow(10, Mathf.Floor(Mathf.Log10(Mathf.Abs(v))) + 1);

        return (float)(scale * Math.Round(v / scale, digits));

    }
}
