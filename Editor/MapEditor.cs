using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGeneration))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGeneration map = target as MapGeneration;
        
        if(DrawDefaultInspector())
        {
            map.GenerateMap();
        }

        if(GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }



}
