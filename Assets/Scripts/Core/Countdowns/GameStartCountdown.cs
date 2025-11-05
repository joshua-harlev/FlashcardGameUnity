/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * GameStartCountdown.cs | Countdown for the start of the game
 */

using System;
using UnityEngine;

/// <summary>
/// Manages the countdown displayed before starting the game.
/// </summary>
/// <remarks>
/// Extends the TextUpdateCountdown class to provide specific functionality
/// for initializing the game when the countdown ends.
/// </remarks>
public class GameStartCountdown : TextUpdateCountdown
{
    /// <summary>
    /// A reference to the game countdown panel UI element.
    /// </summary>
    [SerializeField] private GameObject gameCountdownPanel;

    /// <summary>
    /// The initial font size of the countdown text before any scaling is applied.
    /// </summary>
    private const int initialFontSize = 80;

    /// <summary>
    /// Specifies the final font size of the countdown text displayed
    /// when the countdown reaches its end.
    /// </summary>
    private const int finalFontSize = 600;


    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes the countdown-related components and activates
    /// the game countdown panel for the start of the game.
    /// </summary>
    private new void Awake() {
        countdownLength++;
        base.Awake();
        GameActions.OnGameSceneLoad += StartCountdown;
        SetGameCountdownPanelActive(true);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        GameActions.OnGameSceneLoad -= StartCountdown;
    }

    /// <summary>
    /// Invoked when the countdown reaches its end.
    /// Triggers the start of the game round and deactivates the
    /// game countdown panel to transition to the gameplay phase.
    /// </summary>
    /// <returns>
    /// Void.
    /// </returns>
    protected override void OnCountdownEnd()
    {
        SetGameCountdownPanelActive(false);
        Game.Instance.PlayRound();
    }

    /// <summary>
    /// Called at each tick of the countdown timer.
    /// Adjusts the font size of the countdown text dynamically based on the remaining time.
    /// </summary>
    /// <param name="timeRemaining">The time remaining in the countdown, in seconds.</param>
    protected override void OnTick(int timeRemaining) {
        try {
            base.OnTick(timeRemaining - 1);
            if (timeRemaining == 1) {
                UpdateCountdownText("Go!");
            }

            countdownText.fontSize =
                Mathf.Lerp(initialFontSize, finalFontSize, (1f - timeRemaining / (float)countdownLength));
        }
        catch (Exception e) {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// Activates or deactivates the game countdown panel based on the provided state.
    /// </summary>
    /// <param name="active">A boolean value determining whether to activate (true) or deactivate (false) the game countdown panel.</param>
    private void SetGameCountdownPanelActive(bool active)
    {
        gameCountdownPanel.SetActive(active);
    }
}
