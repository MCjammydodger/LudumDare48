using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Level level = (Level)target;
        DrawDefaultInspector();
        if(GUILayout.Button("Setup Boundaries"))
        {
            level.SetupBoundaries();
        }
    }
}
