using System.Collections;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    [SerializeField] private AudioSource bang1;
    [SerializeField] private AudioSource bang2;

    private void Start()
    {
        StartCoroutine(BangRandomly(bang1));
        StartCoroutine(BangRandomly(bang2));
    }

    private IEnumerator BangRandomly(AudioSource bang)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 2f + 0.5f);
            bang.Play();
        }
    }
}