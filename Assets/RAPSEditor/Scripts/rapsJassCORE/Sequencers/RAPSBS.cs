using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Rhapsody Audio Pattern System Beat Sequencer.
/// A step sequencer.
/// </summary>

public class RAPSBS : Beeper
{
    /// <summary>
    /// Information about a beat in the sequencer
    /// </summary>


    public string myName;
    public bool isMuted;
   

   

    // The RAPSKEYS to use for translating scale tones to notes 
    [SerializeField] private RAPSKeys rapsKeys;

    [SerializeField] private List<RAPSPattern> rapsPatterns;

    private RAPSPattern currentPattern;
    // The list of beats 
    private List<RAPSPattern.Beat> currentRapsBeats;

    [SerializeField] private List<Beat> currentBeats;
    // the current beep;
    private int currentBeep = 0;

    [System.Serializable]
    public class Beat
    {
        public bool Active;
        [Range(1, 7)]
        public int Octave = 4;
        [Range(0.0f, 1.0f)]
        public int ScaleTone = 1;
        [Range(-1, 8)]
        public float Volume;
    }


    public RAPSPattern GetcurrentPattern()
    {
        return currentPattern;
    }

    public List<Beat> GetBeats()
    {
        return currentBeats;
    }

    /// <summary>
    /// Load current Pattern
    /// </summary>
    public void LoadCPattern(List<Beat> rapsPattern)
    {
       
    
        currentBeats = rapsPattern;

    }

    public RAPSPattern GetPattern(string pname)
    {
        
        return rapsPatterns.Find(x => x.patternName == pname);
   
    }

    public RAPSPattern CreatePattern(RAPSPattern rapPattern)
    {
        RAPSPattern rAPS = ScriptableObject.CreateInstance<RAPSPattern>();
        rAPS = rapPattern;
        return rAPS;

    }


    List<Beat> ConvertRapsBeatstoBeats(RAPSPattern rp)
    {
         RAPSPattern rAPSPattern = rp;


        List<Beat> newbeats = new List<Beat>();
        List<RAPSPattern.Beat> currentBeats;
        currentBeats = rAPSPattern.Beats;


        for (int i = 0; i < currentBeats.Count; i++)
        {
            RAPSBS.Beat beat = new RAPSBS.Beat
            {
                Active = currentBeats[i].Active,
                Octave = currentBeats[i].Octave,
                ScaleTone = currentBeats[i].ScaleTone,
                Volume = currentBeats[i].Volume

            };
            newbeats.Add(beat);
        }

        return newbeats;
    }

    /// <summary>
    /// Load the beat
    /// </summary>
    public void LoadPattern(string pname)
    {
        
        RAPSPattern rAPSPattern = GetPattern(pname);
        List<Beat> newbeats = new List<Beat>();
        newbeats =  ConvertRapsBeatstoBeats(rAPSPattern);

        LoadCPattern(newbeats);
    }


    /// <summary>
	/// Subscribe to the Beeper
	/// </summary>
    private void OnEnable()
    {
        //LoadPattern(rapsPatterns[0].patternName);
        LoadPattern(rapsPatterns[0].patternName);
        if (myBeeper != null)
        {
            myBeeper.Beeped += HandleBeeped;
        }
    }
    /// <summary>
	/// Unsubscribe from the Beeper
	/// </summary>
    private void OnDisable()
    {
        if (myBeeper != null)
        {
            myBeeper.Beeped -= HandleBeeped;
        }
    }

    /// <summary>
	/// Responds to beep events from the beeper.
	/// </summary>
	/// <param name="beepTime">Beep time.</param>
	/// <param name="midiNoteNumber">Midi note number.</param>
    public void HandleBeeped(double beepTime, int midiNoteNumber, float volume)
    {
        int numSteps = currentBeats.Count;

        if (numSteps == 0)
        {
            return;
        }

        Beat beat = currentBeats[currentBeep];

        if (beat.Active && !isMuted)
        {
           if(rapsKeys != null)
            {
                midiNoteNumber = rapsKeys.GetMIDINote(beat.ScaleTone, beat.Octave);
            }

            DoBeep(beepTime, midiNoteNumber, beat.Volume);
        }

        currentBeep = (currentBeep + 1) % numSteps;
    }

}
