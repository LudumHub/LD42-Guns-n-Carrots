using UnityEditor;
using UnityEngine;

public class EditorShortcuts : ScriptableObject
{
    [MenuItem("Edit/Start _F5")]
    private static void StartGame()
    {
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("Edit/Start _F5", true)]
    private static bool IsStartGameEnabled()
    {
        return !Application.isPlaying;
    }

    [MenuItem("Edit/Stop #F5")]
    private static void StopGame()
    {
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("Edit/Stop #F5", true)]
    private static bool IsStopGameEnabled()
    {
        return Application.isPlaying;
    }
}