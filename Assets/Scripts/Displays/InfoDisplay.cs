/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * InfoDisplay.cs | Manages game info display.
 */

using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the display of game information, including the current round number and score.
/// </summary>
public class InfoDisplay : MonoBehaviour {
    /// <summary>
    /// Text component used to display the current round information in the game interface.
    /// </summary>
    [SerializeField]
    private TMP_Text RoundLabelText;

    /// <summary>
    /// Text component used to display the player's current score in the game interface.
    /// </summary>
    [SerializeField]
    private TMP_Text CurrentScoreText;

    /// <summary>
    /// Initializes the instance by subscribing to the OnStateDataUpdate event, ensuring that UI components
    /// are updated whenever the game state changes.
    /// </summary>
    private void Awake() {
        GameActions.OnStateDataUpdate += UpdateAll;
    }

    /// <summary>
    /// Cleans up the instance by unsubscribing from the OnStateDataUpdate event to prevent memory leaks or unintended behavior when the object is destroyed.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnStateDataUpdate -= UpdateAll;
    }

    /// <summary>
    /// Updates all relevant UI components to represent the current session of the game.
    /// </summary>
    /// <param name="session">The current session of the game containing data such as round number, total rounds, and score.</param>
    private void UpdateAll(GameSession session) {
        UpdateCurrentScore(session.CorrectAnswerCount, session.CurrentRoundNumber);
        UpdateRoundNumber(session.CurrentRoundNumber, session.TotalNumberOfRoundsInGame);
    }

    /// <summary>
    /// Updates the UI to reflect the current round number in the game display.
    /// </summary>
    /// <param name="roundNumber">The current round number to display.</param>
    /// <param name="numberOfRounds">The maximum number of rounds.</param>
    private void UpdateRoundNumber(int roundNumber, int numberOfRounds) {
        RoundLabelText.text = "Round " + roundNumber + "/" + numberOfRounds;
    }

    /// <summary>
    /// Updates the UI to show the player's current score and the number of completed rounds.
    /// </summary>
    /// <param name="score">The player's current score.</param>
    /// <param name="numberOfCompletedRounds">The total number of rounds completed.</param>
    private void UpdateCurrentScore(int score, int numberOfCompletedRounds) {
        CurrentScoreText.text = "Score: " + score + "/" + numberOfCompletedRounds;
    }
}
