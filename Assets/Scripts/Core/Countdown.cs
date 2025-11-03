/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * Countdown.cs | Allows for a configurable countdown with onTick and onComplete actions.
 */
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages a countdown timer with a specified duration, invoking callback actions on each tick and upon completion.
/// </summary>
public class Countdown : MonoBehaviour {
    /// <summary>
    /// Represents the initial duration of the countdown timer, measured in seconds.
    /// </summary>
    private int lengthInSeconds;

    /// <summary>
    /// Tracks the remaining time, in seconds, during the countdown sequence.
    /// </summary>
    private int secondsRemaining { get; set; }

    /// <summary>
    /// Defines a callback action to be invoked on each tick of the countdown timer,
    /// receiving the number of seconds remaining as an argument.
    /// </summary>
    private Action<int> onTick;

    /// <summary>
    /// Defines a callback action to be invoked upon the countdown timer's completion.
    /// </summary>
    private Action onComplete;

    /// <summary>
    /// Initializes the countdown timer with the specified duration, tick action, and completion action.
    /// </summary>
    /// <remarks>
    /// Init is used rather than a constructor because MonoBehaviours may not use constructors.
    /// </remarks>
    /// <param name="lengthInSeconds">The total duration of the countdown in seconds.</param>
    /// <param name="onTick">The callback action executed on every tick of the countdown, receiving the remaining seconds as an argument.</param>
    /// <param name="onComplete">The callback action executed when the countdown completes.</param>
    public void Init(int lengthInSeconds, Action<int> onTick, Action onComplete) {
        this.lengthInSeconds = lengthInSeconds;
        this.onTick = onTick;
        this.onComplete = onComplete;
    }

    /// <summary>
    /// Starts the countdown timer, executing the tick and completion actions at the appropriate intervals.
    /// </summary>
    public void StartCountdown() {
        StartCoroutine(CountdownCoroutine());
    }

    /// <summary>
    /// Stops the currently running countdown timer by halting all associated coroutines.
    /// </summary>
    public void StopCountdown() {
        StopAllCoroutines();
    }


    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// Handles the countdown logic, invoking tick and completion callbacks at the appropriate intervals.
    /// </summary>
    /// <returns>
    /// An IEnumerator used to manage the countdown process over time.
    /// </returns>
    private IEnumerator CountdownCoroutine()
    {
        secondsRemaining = lengthInSeconds;
        for (int i = 0; i < lengthInSeconds; i++) {
            onTick.Invoke(secondsRemaining);
            yield return new WaitForSeconds(1f);
            secondsRemaining--;
        }

        onComplete.Invoke();
    }
}
