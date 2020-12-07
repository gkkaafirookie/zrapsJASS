using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Rhapsody Audio Pattern System Sampler.
/// A audio sampler.
/// </summary>

public class RAPSSampler : MonoBehaviour
{

    [Header("Config")]
   
    //the beeper that will control this sampler
    [SerializeField] private Beeper mybeeper;

    // The audio file we want to play 
    [SerializeField] private AudioClip myaudioClip;
    [SerializeField] private AudioMixerGroup myaudioMixer;
    // The number of voices. Set this higher if you're hearing excessive voice stealing.
    [SerializeField, Range(1, 8)] private int numVoices = 2;

    // The prefab for the RAPS Voice of this sampler 
    [SerializeField]private RAPSVoice myRapsVoice;

    // The list of RAPS voices
    private RAPSVoice[] availableRapsVoices;

    // The indes of the next voice to play
    private int nextRapsVoiceIndex;


     
    float lastbeepTime;
    
    float temp;
     public float Temperature
        {
            get
            {
                temp = 1f - Mathf.Clamp01(Time.time - lastbeepTime);
                return temp;
            }
         }

  public Beeper myBeeper {
    get{
        return mybeeper;
    }
  }

    /// <summary>
    /// This gets called when the GameObject first gets created.
    /// Create our sampler voices here.
    /// </summary>
    private void Awake()
    {
        availableRapsVoices = new RAPSVoice[numVoices];

        for (int i = 0; i < numVoices; ++i)
        {
            RAPSVoice samplerVoice = Instantiate(myRapsVoice);
            samplerVoice.transform.parent = transform;
            samplerVoice.transform.localPosition = Vector3.zero;
            samplerVoice.myAudioSource.outputAudioMixerGroup = myaudioMixer;
            availableRapsVoices[i] = samplerVoice;
        }

      
    }


   

    ///<summary>
    /// This gets called when the GameObject gets turned on.
    /// Used for initialization.
    /// </summary>
    private void OnEnable()
    {
        if (myBeeper != null)
        {
            myBeeper.Beeped += HandleBeeped;
            
        }
    }
    ///<summary>
    /// This gets called when the GameObject gets turned off.
    /// Used for cleaning.
    /// </summary>
    private void OnDisable()
    {
        if (myBeeper != null)
        {
            myBeeper.Beeped -= HandleBeeped;
        }
    }
   


    private void HandleBeeped(double beepTime, int midiNoteNumber, float volume)
    {
         lastbeepTime = Time.time;
         
        float pitch = RAPSUtils.MidiNoteToPitch(midiNoteNumber, RAPSUtils.MidiNoteC4);
        availableRapsVoices[nextRapsVoiceIndex].Play(myaudioClip, pitch, beepTime, volume);
        nextRapsVoiceIndex = (nextRapsVoiceIndex + 1) % availableRapsVoices.Length;
    }
    

}
