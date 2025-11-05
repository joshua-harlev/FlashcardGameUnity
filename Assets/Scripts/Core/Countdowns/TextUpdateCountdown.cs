/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * TextUpdateCountdown.cs | Provides a configurable countdown a TMP_Text object display.
 */

using TMPro;
using UnityEngine;

/// <summary>
/// Manages a countdown timer and updates a TextMeshPro text component with the remaining time.
/// </summary>
public class TextUpdateCountdown : MonoBehaviour
{
    /// <summary>
    /// A TextMeshPro text component used to display the countdown timer updates.
    /// </summary>
    [SerializeField] private protected TMP_Text countdownText;

    /// <summary>
    /// The duration of the countdown timer, in seconds.
    /// </summary>
    [SerializeField] protected int countdownLength = 5;

    [SerializeField]
    protected Color startColor = Color.white;
    [SerializeField]
    protected Color endColor = Color.red;

    /// <summary>
    /// An instance of the Countdown class used to manage and execute the countdown timer.
    /// </summary>
    private Countdown countdown;

    /// <summary>
    /// Initializes the Countdown component and sets it up with the countdown duration
    /// and event callbacks for tick updates and countdown completion.
    /// </summary>
    protected virtual void Awake()
    {
        countdown = this.gameObject.AddComponent<Countdown>();
        countdown.Init(countdownLength, OnTick, OnCountdownEnd);
    }

    protected virtual void OnDestroy() {
        Destroy(countdown);
    }

    /// <summary>
    /// Starts the countdown sequence.
    /// </summary>
    public void StartCountdown() {
        countdown.StartCountdown();
    }

    /// <summary>
    /// Stops the countdown timer and halts any ongoing countdown processes.
    /// </summary>
    public void StopCountdown() {
        countdown.StopCountdown();
    }

    /// <summary>
    /// Invoked periodically during the countdown to update the remaining time.
    /// </summary>
    /// <param name="timeRemaining">The number of seconds remaining in the countdown.</param>
    protected virtual void OnTick(int timeRemaining) {
        UpdateCountdownText(timeRemaining.ToString());
        AutoUpdateCountdownTextColor(timeRemaining);
    }

    protected void AutoUpdateCountdownTextColor(int timeRemaining) {
        countdownText.color = Color.Lerp(startColor, endColor, (1f - timeRemaining / (float)countdownLength));
    }

    /// <summary>
    /// Invoked when the countdown reaches its end. Writes to the console by default.
    /// </summary>
    protected virtual void OnCountdownEnd() {
        Debug.Log("Countdown Ended.");
    }

    /// <summary>
    /// Updates the countdown text displayed on the TextMeshPro text component.
    /// </summary>
    /// <param name="newText">The text to display on the countdown text component.</param>
    protected void UpdateCountdownText(string newText) {
        countdownText.text = newText;
    }
}
