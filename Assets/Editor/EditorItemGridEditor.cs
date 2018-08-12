using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorItemGrid))]
public class EditorItemGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var grid = (EditorItemGrid) target;
        if (GUILayout.Button("Configure Object"))
        {
            grid.ConfigureTarget();
        }
        if (GUILayout.Button("Reset Grid"))
        {
            grid.ResetGrid();
        }
    }
}