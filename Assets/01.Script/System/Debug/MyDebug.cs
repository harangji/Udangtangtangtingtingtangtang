using UnityEngine;

public static class MyDebug
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private static readonly string[] RAINBOW_COLORS = 
    {
        "red",      // 빨강
        "orange",   // 주황
        "yellow",   // 노랑
        "green",    // 초록
        "blue",     // 파랑
        "indigo",   // 남색
        "violet"    // 보라
    };

    public static void Log(object message, int index = 0)
    {
        // index를 0~rainbowColors.Length-1 범위로 제한
        int colorIndex = Mathf.Abs(index) % RAINBOW_COLORS.Length;

        string color = RAINBOW_COLORS[colorIndex];

        Debug.Log($"<color={color}>{message}</color>");
    }

    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    public static void LogError(object message)
    {
        Debug.LogError(message);
    }
#else
    public static void Log(object message, int index = 0) { }
    public static void LogWarning(object message) { }
    public static void LogError(object message) { }
#endif
}