using System;
using UnityEngine;

/// <summary>
/// Rhapsody Audio Pattern System Keys.
/// A scale mode controler.
/// </summary>

public class RAPSKeys : MonoBehaviour
{
    public enum Note { c = 0, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B }

    /// <summary >
    /// <mode Ionian >
    /// The Ionian mode is a simple ‘doh re mi’ major key. 
    /// It is the modern major scale. 
    /// It is composed of natural notes beginning on C.
    /// </mode>
    /// <mode Dorian>
    /// MINOR 
    /// The only difference is in the sixth note, 
    /// which is a major sixth above the first note, rather than a minor sixth.
    /// </mode>
    /// <mode Phrygian>
    /// MINOR
    /// It is also very similar to the modern natural minor scale. 
    /// The only difference is in the second note,
    /// which is a minor second not a major.
    /// </mode>
    /// <mode Lydian>
    /// MAJOR
    /// The Lydian mode has just one note changed from the Ionian, a major scale, 
    /// but with the fourth note from the bottom sharpened to give a slightly unsettling sound.
    /// </mode>
    /// <mode Mixolydian>
    /// MAJOR
    /// The single tone that differentiates this scale from the major scale is its seventh note,
    /// which is a flattened seventh rather than a major seventh.
    /// </mode>
    /// <mode Aeolian>
    /// Natural Minor 
    /// </mode>
    /// <mode Locria>
    /// The Locrian mode has five notes in its scale flattened a half-step
    /// </mode>
    /// </summary>
    public enum ScaleMode { Ionian = 0, Dorian, Phrygian, Lydian, Mixolydian, Aeolian, Locria }

    public Note RootNote;
    public ScaleMode Mode;

    private static int[] INTERVALS = { 1, 2, 2, 1, 2, 2, 2 };

    /// <summary>
	/// Converts a scale tone and octave to a note in the current key
	/// </summary>
	/// <returns>MIDI note</returns>
	/// <param name="scaleTone">Scale tone, range (1, 7)</param>
	/// <param name="octave">Octave, range (-1, 10 or so)</param>
    /// 
    public int GetMIDINote(int scaleTone, int octave)
    {
        // scaleTone is range (1,7) for readability
        // but range (0,6) is easier to work with
        scaleTone--;

        // wrap scale tone and shift octaves
        while (scaleTone < 0)
        {
            octave--;
            scaleTone += 7;
        }
        while (scaleTone >= 7)
        {
            octave++;
            scaleTone -= 7;
        }

        // C4 = middle C, so MIDI note 0 is C-1.
        // we don't want to go any lower than that
        octave = Mathf.Max(octave, -1);
        // shift to minimum of 0 for easy math
        octave++;

        // add semitones for each step through the scale,
        // using the interval key above
        int semitones = 0;
        while (scaleTone > 0)
        {
            int idx = (scaleTone + (int)Mode) % 7;
            semitones += INTERVALS[idx];
            scaleTone--;
        }

        return octave * 12 + semitones + (int)RootNote;
    }

   
}
