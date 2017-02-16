using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGeneration))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGeneration map = target as MapGeneration;

        map.GenerateMap();
    }



}
