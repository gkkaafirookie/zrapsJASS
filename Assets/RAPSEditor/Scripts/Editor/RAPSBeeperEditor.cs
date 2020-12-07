using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(Beeper))]
public class RAPSBeeperEditor : Editor
{
    void OnSceneGUI()
        {
            var si = target as Beeper;
            if (si.myBeeper != null)
            {
                DrawInteraction(si);
            }
        }

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
        static void DrawConnectionGizmo(Beeper beeper, GizmoType gizmoType)
        {
            
            if (beeper.myBeeper != null)
            {
                var start = beeper.transform.position;
                var end = beeper.myBeeper.transform.position;
                if (end == start) end += beeper.myBeeper.transform.forward * 1;
                var dir = ( start - end).normalized;
                if (Application.isPlaying)
                    Handles.color = Color.Lerp(Color.white, Color.green, beeper.myBeeper.Temperature);
                else
                    Handles.color = new Color(1, 1, 1, 0.25f);
                Handles.DrawDottedLine(start, end, 5);
                Handles.ArrowHandleCap(0, end + (dir), Quaternion.LookRotation(dir), 1f, EventType.Repaint);
            }
        }

    public static void DrawInteraction(Beeper beeper)
        {
            var start = beeper.transform.position;
            var end = beeper.myBeeper.transform.position;
            var dir = (end - start).normalized;
            
            if (Application.isPlaying)
                Handles.color = Color.Lerp(Color.white, Color.green, beeper.myBeeper.Temperature);
            var steps = Mathf.FloorToInt((end - start).magnitude);
            for (var i = 0; i < steps; i++)
            {
                Handles.ArrowHandleCap(0, start + (dir * i), Quaternion.LookRotation(dir), 1f, EventType.Repaint);
            }
        }
}
