using UnityEngine;

/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * RoundTimer.cs | Game round timer.
 */

/// <summary>
/// Represents a game round timer that updates countdown text and triggers game state transitions
/// when the countdown ends.
/// </summary>
public class RoundTimer : TextUpdateCountdown {
    /// <summary>
    /// Initializes the RoundTimer instance by setting the default start color and
    /// subscribing to the OnGameStart and OnRoundEnd game events.
    /// The base class's Awake method is called to ensure proper initialization of
    /// inherited functionality.
    /// </summary>
    protected override void Awake() {
        startColor = Color.black;
        base.Awake();
        GameActions.OnRoundStart += StartCountdown;
        GameActions.OnRoundEnd += StopCountdown;
    }

    /// <summary>
    /// Cleans up the RoundTimer instance by removing it from any subscribed game events.
    /// Unsubscribes from the OnGameStart event to prevent the StartCountdown method
    /// from being called after this object is destroyed.
    /// Unsubscribes from the OnRoundEnd event to ensure the StopCountdown method
    /// is not inadvertently triggered after destruction.
    /// Calls the base class's OnDestroy method to handle default cleanup operations.
    /// </summary>
    protected override void OnDestroy() {
        base.OnDestroy();
        GameActions.OnRoundStart -= StartCountdown;
        GameActions.OnRoundEnd -= StopCountdown;
    }

    /// <summary>
    /// Invoked when the countdown reaches its end.
    /// Updates the countdown text to display "0 seconds".
    /// Invokes the EndRound() method on the game instance.
    /// Starts a new game round by invoking PlayRound() on the game instance.
    /// </summary>
    protected override void OnCountdownEnd() {
        countdownText.text = "0 seconds";
        Game.Instance.EndRound();
        Game.Instance.PlayRound();
    }

    /// <summary>
    /// Invoked periodically during the countdown to update the remaining time.
    /// Updates the countdown text with the current remaining seconds formatted as a string (with " seconds").
    /// </summary>
    /// <param name="timeRemaining">The number of seconds remaining in the countdown.</param>
    protected override void OnTick(int timeRemaining)
    {
        UpdateCountdownText(timeRemaining + " seconds");
        GameActions.OnRoundTimerTick(timeRemaining);
        AutoUpdateCountdownTextColor(timeRemaining);
    }
}
