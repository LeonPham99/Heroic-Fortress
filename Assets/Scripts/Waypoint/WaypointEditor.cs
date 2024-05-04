using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    private GUIStyle textStyle;

    private void OnEnable()
    {
        textStyle = new GUIStyle()
        {
            fontStyle = FontStyle.Bold,
            fontSize = 16,
            normal = { textColor = Color.yellow }
        };
    }

    [System.Obsolete]
    private void OnSceneGUI()
    {
        Waypoint waypoint = target as Waypoint;
        Handles.color = Color.red;

        for (int i = 0; i < waypoint.Points.Length; i++)
        {
            Vector3 currentWaypointPoint = waypoint.CurrentPosition + waypoint.Points[i];
            DrawHandle(currentWaypointPoint, i, waypoint);
        }
    }

    [System.Obsolete]
    private void DrawHandle(Vector3 position, int index, Waypoint waypoint)
    {
        EditorGUI.BeginChangeCheck();
        Vector3 newWaypointPoint = Handles.FreeMoveHandle(position, Quaternion.identity, 0.7f,
            new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

        Handles.Label(position + (Vector3.down * 0.35f + Vector3.right * 0.35f), $"{index + 1}", textStyle);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Free Move Handle");
            waypoint.Points[index] = newWaypointPoint - waypoint.CurrentPosition;
        }
    }

    //Waypoint Waypoint => target as Waypoint;

    //private void OnSceneGUI()
    //{
    //    Handles.color = Color.red;
    //    for (int i = 0; i < Waypoint.Points.Length; i++)
    //    {
    //        EditorGUI.BeginChangeCheck();

    //        // Create Handles
    //        Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
    //        var fmh_21_17_638464381475582762 = Quaternion.identity; Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 0.7f,
    //            new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

    //        // Create Text
    //        GUIStyle textStyle = new GUIStyle();
    //        textStyle.fontStyle = FontStyle.Bold;
    //        textStyle.fontSize = 16;
    //        textStyle.normal.textColor = Color.yellow;
    //        Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
    //        Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlligment,
    //            $"{i + 1}", textStyle);

    //        EditorGUI.EndChangeCheck();

    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            // Update the last position of each point in Inspector
    //            Undo.RecordObject(target, "Free Move Handle");
    //            Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
    //        }
    //    }
    //}
}
