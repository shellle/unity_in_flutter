using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeepScript : MonoBehaviour
{
    private AudioSource audioSource;
    public int sampleRate = 44100;
    public float frequency = 440;
    public float gain = 0.05f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(BeepAfterDelay());
    }

    private IEnumerator BeepAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Beep();
    }

    private void Beep()
    {
        AudioClip clip = MakeBeep();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip MakeBeep()
    {
        int samples = sampleRate;
        float[] data = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            data[i] = gain * Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }
        AudioClip clip = AudioClip.Create("beep", samples, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }
}
