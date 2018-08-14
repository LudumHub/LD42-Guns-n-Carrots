using UnityEngine;

public static class Analytics
{
    public static void StartGame()
    {
        Application.ExternalEval("appInsights.trackEvent(\"StartGame\");");
    }

    public static void CompleteLevel(int level, float time)
    {
        Application.ExternalEval(string.Format("appInsights.trackEvent(\"CompleteLevel\", {{ Level: {0}, Time: {1}}});",
            level, time));
    }
}