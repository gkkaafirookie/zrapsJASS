using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RAPSDJ))]
public class RAPSDJEditor : Editor
{


  



    /// <summary>
    /// This gets called whenever a GameObject with the StepSequencer script
    /// attached has its inspector refreshed. Draw the controls in here.
    /// </summary>
    public override void OnInspectorGUI()
    {

        // get the StepSequencer instance
        RAPSDJ dj = (RAPSDJ)target;

        // start listening for changes
        EditorGUI.BeginChangeCheck();

        // Draw the controls we aren't hiding with [HideInInspector]
        DrawDefaultInspector();

        List<RAPSBS> BS = dj.GetBS();
        List<RAPSES> ES = dj.GetES();

       
       

       

        // Draw the BS
        for (int i = 0; i < BS.Count; ++i)
        {
            RAPSBS beat = BS[i];

            // Draw all the step fields on one line
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 60;
            EditorGUILayout.LabelField("BS " + (i + 1), GUILayout.Width(60));
             EditorGUILayout.LabelField( beat.myName, GUILayout.Width(80));
            beat.isMuted = EditorGUILayout.Toggle("Active", beat.isMuted, GUILayout.Width(80));
           
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();
        }

        // Draw the BS
        for (int i = 0; i < ES.Count; ++i)
        {
            RAPSES beat = ES[i];

            // Draw all the step fields on one line
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 60;
            EditorGUILayout.LabelField("ES " + (i + 1), GUILayout.Width(60));
            EditorGUILayout.LabelField( beat.myName, GUILayout.Width(80));
            beat.isMuted = EditorGUILayout.Toggle("Active", beat.isMuted, GUILayout.Width(80));

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
