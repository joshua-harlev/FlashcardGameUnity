using UnityEngine;
/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * CommandLogger.cs | Command Logger Class
 */

/// <summary>
/// Represents a logger used to track and record commands using DebugLogger.
/// </summary>
public class CommandLogger : MonoBehaviour {
    private void Start() {
        GameActions.OnCommandExecuted += OnCommandExecuted;
    }

    private void OnDestroy() {
        GameActions.OnCommandExecuted -= OnCommandExecuted;
    }

    private static void OnCommandExecuted(Command command) {
        DebugLogger.Log(LogChannel.Systems, command.GetDetails(), LogLevel.Info);
    }
}