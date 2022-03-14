using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public static class PlayStateNotifier
{

    public static void AddPlayStateNotifier(Action action)
    {
        EditorApplication.playModeStateChanged += (playModeState) => { ModeChanged(playModeState, action); };
    }

    static void ModeChanged(PlayModeStateChange playModeState, Action action)
    {
        if (playModeState == PlayModeStateChange.EnteredEditMode)
        {
            action();
        }
    }
}