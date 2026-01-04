using UnityEngine;
using System;

public class MathUtils
{
    public static float SigFigs(double v, int digits)
    {
        if (v == 0f)
            return 0f;

        double scale = Math.Pow(10, Math.Floor(Math.Log10(Abs(v))) + 1);

        return (float)(scale * Math.Round(v / scale, digits));

    }
    static double Abs(double d)
    {
        return d < 0 ? -d : d;
    }
}
