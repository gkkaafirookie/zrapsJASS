using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rhapsody Audio Pattern System Voice.
/// A voice for the Sampler.
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class RAPSVoice : MonoBehaviour
{
 
    //The audiosource.
    public AudioSource myAudioSource;


    // Called when the GameObject is created
    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    /// <summary>
    /// Plays the provided AudioClip on this voice's AudioSource 
    /// </summary>
    /// <paramname="audioClip">The audiolip we want to play</param>
    public void Play(AudioClip audioClip, float pitch, double startTime, float volume)
    {

        myAudioSource.clip = audioClip;
        myAudioSource.pitch = pitch;
        myAudioSource.volume = volume;
        myAudioSource.PlayScheduled(startTime);
    }
   /* private void OnAudioFilterRead(float[] buffer, int numChannels)
    {
        for (int sIdx = 0; sIdx < buffer.Length; sIdx += numChannels)
        {
            double volume = 0;

            if (_samplesUntilEnvelopeTrigger == 0)
            {
                volume = RAPSEnvelope.GetLevel();
            }
            else
            {
                --_samplesUntilEnvelopeTrigger;
            }

            for (int cIdx = 0; cIdx < numChannels; ++cIdx)
            {
                buffer[sIdx + cIdx] *= (float)volume;
            }
        }
    }
   */
}
