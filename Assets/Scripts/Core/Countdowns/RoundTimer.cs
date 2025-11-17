/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * RoundTimer.cs | Game round timer.
 */
using UnityEngine;


public class RoundTimer : TextUpdateCountdown {
    protected override void Awake() {
        startColor = Color.black;
        base.Awake();
        GameActions.OnRoundStart += StartCountdown;
        GameActions.OnRoundEnd += StopCountdown;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        GameActions.OnRoundStart -= StartCountdown;
        GameActions.OnRoundEnd -= StopCountdown;
    }


    protected override void OnCountdownEnd() {
        countdownText.text = "0 seconds";
        GameActions.OnRoundTimerEnd?.Invoke();
    }

    protected override void OnTick(int timeRemaining)
    {
        UpdateCountdownText(timeRemaining + " seconds");
        GameActions.OnRoundTimerTick(timeRemaining);
        AutoUpdateCountdownTextColor(timeRemaining);
    }
}
