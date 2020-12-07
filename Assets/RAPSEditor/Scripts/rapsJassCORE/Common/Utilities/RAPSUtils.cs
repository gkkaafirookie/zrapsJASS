
using UnityEngine;

public class RAPSUtils 
{
    public const int MidiNoteC4 = 60;

    public static float MidiNoteToPitch(int midiNote, int baseNote)
    {
        int semitoneOffset = midiNote - baseNote;
        return Mathf.Pow(2.0f, semitoneOffset / 12.0f);
    }
}
