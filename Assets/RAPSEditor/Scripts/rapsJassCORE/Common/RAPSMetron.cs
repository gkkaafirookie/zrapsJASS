
using System;
using UnityEngine;
/// <summary>
/// Sends a beep on each subdivision of a beat.
/// </summary>
public class RAPSMetron : Beeper
{
   
    
    


    [SerializeField, Tooltip("The tempo in beats per minute"), Range(15f, 200f)] private double tempo = 120.0;
    [SerializeField, Tooltip("The number of beeps per beat"), Range(1, 8)] private int beepsPerbeat = 4;

    // the length of a single beep in seconds
    private double beepLength;

    // the next beep time, relative to AudioSettings.dspTime
    private double nextBeepTime;

    ///<summary>
    /// Set the tempo and recalulate
    /// </summary>
    /// <param name = "tempo">the new tempo in BPM</paramname>
    public void SetTempo(double _tempo)
    {
        tempo = _tempo;
        Recalculate();

    }
    /// <summary>
    /// Recalculate the beep length and reset the next beep time
    /// </summary>
    private void Reset()
    {
        Recalculate();
        // bump the next tick time ahead the length of one beep so we don't get a double trigger
        nextBeepTime = AudioSettings.dspTime + beepLength;
    }

    /// <summary>
    /// Derive the length of a beep in seconds from the tempo and subdivisions provided
    /// </summary>
    private void Recalculate()
    {
        double beatsPerSecond = tempo / 60.0;
        double beepsPerSecond = beatsPerSecond * beepsPerbeat;
        beepLength = 1.0 / beepsPerSecond;
    }

    ///<summary>
    /// This gets called when the GameObject gets turned on.
    /// Used for initialization.
    /// </summary>
    private void OnEnable()
    {
        Reset();
    }

    /// <summary>
    /// This gets called in the editor when an inspector control changes.
    /// Recalculate the beep length here.
    /// </summary>
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Recalculate();
        }
    }

    /// <summary>
    /// This gets called once per game frame.
    /// Check to see if we should schedule any beeps here.
    /// </summary>
    private void Update()
    {
        double currentTime = AudioSettings.dspTime;

        // look ahead the length of one frame (approximately), because we'll be scheduling samples
        currentTime += Time.deltaTime;

        // there may be more than one beep within the next frame, so this will catch them all
        while (currentTime > nextBeepTime)
        {
            // if someone has subscribed to beep from the metronome, let them know we got a tick
            DoBeep(nextBeepTime);

            // increment the next tick time
            nextBeepTime += beepLength;
        }
    }
}
