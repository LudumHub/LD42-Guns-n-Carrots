using System;
using System.Linq;
using UnityEngine;

public class GunSoundsLibrary: MonoBehaviour
{
    public Record[] Sounds;

    public AudioClip GetSound(string gun)
    {
        return Sounds
            .Where(s => s.Gun == gun)
            .Select(s => s.Sound)
            .FirstOrDefault();
    }

    [Serializable]
    public class Record
    {
        public string Gun;
        public AudioClip Sound;
    }
}