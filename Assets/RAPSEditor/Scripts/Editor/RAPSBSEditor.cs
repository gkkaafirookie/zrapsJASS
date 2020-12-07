using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom inspector for the StepSequencer.
/// Makes sequence editing a little prettier/faster.
/// </summary>
[CustomEditor(typeof(RAPSBS))]
public class RAPSBSEditor : Editor
{



    /// <summary>
    /// This gets called whenever a GameObject with the StepSequencer script
    /// attached has its inspector refreshed. Draw the controls in here.
    /// </summary>
    public override void OnInspectorGUI()
    {

        // get the StepSequencer instance
        RAPSBS sequencer = (RAPSBS)target;

        // start listening for changes
        EditorGUI.BeginChangeCheck();

        // Draw the controls we aren't hiding with [HideInInspector]
        DrawDefaultInspector();

        List<RAPSBS.Beat> beats = sequencer.GetBeats();

        // Set the number of steps in the sequence
        int numSteps = EditorGUILayout.IntSlider("# steps", beats.Count, 1, 32);

        // Add or remove steps based on the above slider's value
        while (numSteps > beats.Count)
        {
            beats.Add(new RAPSBS.Beat());
        }
        while (numSteps < beats.Count)
        {
            beats.RemoveAt(beats.Count - 1);
        }
        EditorGUILayout.LabelField(sequencer.myName, GUILayout.Width(60));
        // Draw the steps
        for (int i = 0; i < beats.Count; ++i)
        {
            RAPSBS.Beat beat = beats[i];

            // Draw all the step fields on one line
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 60;
           
            EditorGUILayout.LabelField("Step " + (i + 1), GUILayout.Width(60));
            beat.Active = EditorGUILayout.Toggle("Active", beat.Active, GUILayout.Width(80));
            beat.Octave = EditorGUILayout.IntField("Octave", beat.Octave);
            beat.ScaleTone = EditorGUILayout.IntField("Sace Tone", beat.ScaleTone);
            beat.Volume = EditorGUILayout.FloatField("Volume", (float)beat.Volume);
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();
        }

        // if there were changes, mark the StepSequencer dirty,
        // that is, let Unity know it should be re-saved when saving the scene/project.
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }
}
