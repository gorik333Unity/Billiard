using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

/// <summary>
/// Custom logger solution for logs.
/// <remarks>
/// Logs in this class are dependant on <see langword="DEBUG"/> and <see langword="ENABLE_RELEASE_LOGS"/>. 
/// If <see langword="ENABLE_RELEASE_LOGS"/> defined logs will displayed in Release Build.
/// Otherwise only Editor and Developer Build will display logs.
/// For defining preprocessor open CoreManager or write down in PlayerSettings in field "Scripting Define Symbols".
/// It's fully stripped from Release Builds.
/// </remarks>
/// <seealso cref="CorePlugin.Core.CoreManager"/>
/// </summary>
public static class DebugLogger
{
    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void Log(string message, Object context)
    {
        Debug.Log(message, context);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void Log(object obj)
    {
        Debug.Log(obj);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void Log(object obj, Object context)
    {
        Debug.Log(obj, context);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogError(string message)
    {
        Debug.LogError(message);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogWarning(string message, Object context)
    {
        Debug.LogWarning(message, context);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogError(string message, Object context)
    {
        Debug.LogError(message, context);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogError(Exception exception, Object context)
    {
        Debug.LogError(exception, context);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogException(Exception exception)
    {
        Debug.LogException(exception);
    }

    [Conditional(EditorDefinition.Debug)]
    [Conditional(EditorDefinition.EnableReleaseLogs)]
    public static void LogException(Exception exception, Object context)
    {
        Debug.LogException(exception, context);
    }

    #region DRAWLINE

    [Conditional(EditorDefinition.Debug)]
    public static void DrawLine(Vector3 start, Vector3 dir)
    {
        Debug.DrawLine(start, dir, Color.magenta);
    }

    [Conditional(EditorDefinition.Debug)]
    public static void DrawLine(Vector3 start, Vector3 end, Color blue)
    {
        Debug.DrawLine(start, end, blue);
    }

    [Conditional(EditorDefinition.Debug)]
    public static void DrawLine(Vector3 start, Vector3 end, Color blue, float duration)
    {
        Debug.DrawLine(start, end, blue, duration);
    }

    #endregion

    #region DRAWRAY

    [Conditional(EditorDefinition.Debug)]
    public static void DrawRay(Vector3 start, Vector3 dir)
    {
        Debug.DrawRay(start, dir, Color.magenta);
    }

    [Conditional(EditorDefinition.Debug)]
    public static void DrawRay(Vector3 start, Vector3 dir, Color blue)
    {
        Debug.DrawRay(start, dir, blue);
    }

    [Conditional(EditorDefinition.Debug)]
    public static void DrawRay(Vector3 start, Vector3 dir, Color blue, float duration)
    {
        Debug.DrawRay(start, dir, blue, duration);
    }

    #endregion
}


public readonly struct EditorDefinition
{
    public const string UnityEditor = "UNITY_EDITOR";
    public const string Debug = "DEBUG";
    public const string EnableReleaseLogs = "ENABLE_RELEASE_LOGS";
}
