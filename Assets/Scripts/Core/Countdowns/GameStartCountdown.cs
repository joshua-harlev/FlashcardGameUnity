/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * GameStartCountdown.cs | Countdown for the start of the game
 */

using System;
using UnityEngine;


public class GameStartCountdown : TextUpdateCountdown
{

    [SerializeField] private GameObject gameCountdownPanel;
    
    private const int initialFontSize = 80;
    private const int finalFontSize = 600;

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
    
    protected override void OnCountdownEnd()
    {
        SetGameCountdownPanelActive(false);
        GameActions.OnCountdownComplete?.Invoke();
    }
    
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
    
    private void SetGameCountdownPanelActive(bool active)
    {
        gameCountdownPanel.SetActive(active);
    }
}
