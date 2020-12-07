using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// These  send out beeps
/// </summary>

public class Beeper : MonoBehaviour
{

    public delegate void BeepHandler(double beepTime, int midiNoteNumber, float volume);
    [SerializeField] private Beeper mybeeper;
    public event BeepHandler Beeped;
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

    protected void DoBeep(double beepTime, int midiNoteNumber = 60, float volume = 1.0f)
    {
         lastbeepTime = Time.time;
        
        Beeped?.Invoke(beepTime, midiNoteNumber, volume);
        
    }

}
