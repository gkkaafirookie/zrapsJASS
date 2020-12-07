using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(RAPSSampler))]
public class RAPSSamplerEditor : Editor
{
   void OnSceneGUI()
        {
            var si = target as RAPSSampler;
            if (si.myBeeper != null)
            {
                DrawInteraction(si);
            }
        }

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
        static void DrawConnectionGizmo(RAPSSampler rAPSSampler, GizmoType gizmoType)
        {
            
            if (rAPSSampler.myBeeper != null)
            {
                var start = rAPSSampler.transform.position;
                var end = rAPSSampler.myBeeper.transform.position;
                if (end == start) end += rAPSSampler.myBeeper.transform.forward * 1;
                var dir = ( start - end).normalized;
                if (Application.isPlaying)
                    Handles.color = Color.Lerp(Color.blue, Color.magenta, rAPSSampler.myBeeper.Temperature);
                else
                    Handles.color = new Color(0, 1, 1, 0.25f);
                Handles.DrawDottedLine(start, end, 5);
                Handles.ArrowHandleCap(0, end + (dir), Quaternion.LookRotation(dir), 1f, EventType.Repaint);
            }
        }

    public static void DrawInteraction(RAPSSampler rAPSSampler)
        {
            var start = rAPSSampler.transform.position;
            var end = rAPSSampler.myBeeper.transform.position;
            var dir = (end - start).normalized;
            
            if (Application.isPlaying)
                Handles.color = Color.Lerp(Color.blue, Color.magenta, rAPSSampler.myBeeper.Temperature);
            var steps = Mathf.FloorToInt((end - start).magnitude);
            for (var i = 0; i < steps; i++)
            {
                Handles.ArrowHandleCap(0, start + (dir * i), Quaternion.LookRotation(dir), 1f, EventType.Repaint);
            }
        }
}
