using System;
using System.Collections.Generic;
using UnityEngine;

public static class Easing
{
    public static IEnumerable<float> InCubic(float from, float to, float time)
    {
        return Ease(from, to, time, fraction => Mathf.Pow(fraction, 3f));
    }

    public static IEnumerable<float> OutCubic(float from, float to, float time)
    {
        return Ease(from, to, time, fraction => Mathf.Pow(fraction, 1 / 3f));
    }

    public static IEnumerable<float> OutSine(float from, float to, float time)
    {
        return Ease(from, to, time, fraction => Mathf.Sin(fraction * Mathf.PI / 2));
    }

    private static IEnumerable<float> Ease(float from, float to, float time, Func<float, float> ease)
    {
        var elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            var elapsedFraction = elapsed / time;
            var easedFraction = ease(elapsedFraction);
            yield return from + (to - from) * easedFraction;
        }

        yield return to;
    }
}