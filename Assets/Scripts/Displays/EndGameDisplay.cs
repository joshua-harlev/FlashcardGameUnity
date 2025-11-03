/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * EndGameDisplay.cs | Manages end of game display panel.
 */

using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the end game display logic for showing the score and end game screen in a Unity game.
/// </summary>
public class EndGameDisplay : MonoBehaviour
{
    /// <summary>
    /// Represents the UI text component used to display the game score at the end of the game.
    /// </summary>
    [SerializeField]
    private TMP_Text ScoreText;

    /// <summary>
    /// Represents the UI group or panel used to display the end game screen in the Unity game.
    /// </summary>
    [SerializeField]
    private GameObject EndGameGroup;

    private GameState gameState;

    [SerializeField]
    private TMP_Text unlockedAchievementsText;

    /// <summary>
    /// Initializes the EndGameDisplay by subscribing to game action events related to state updates and gameplay end.
    /// </summary>
    private void Awake() {
        GameActions.OnStateDataUpdate += UpdateScore;
        GameActions.OnStateDataUpdate += UpdateDisplayedAchievements;
        GameActions.OnGameplayEnd += ShowScreen;
    }

    /// <summary>
    /// Performs cleanup by unsubscribing from the game action events related to state updates
    /// and gameplay end when the object is destroyed.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnStateDataUpdate -= UpdateScore;
        GameActions.OnStateDataUpdate -= UpdateDisplayedAchievements;
        GameActions.OnGameplayEnd -= ShowScreen;
    }

    /// <summary>
    /// Updates the displayed score with the number of correct answers and the total number of rounds completed.
    /// </summary>
    /// <param name="state">The current game state containing score and round information.</param>
    private void UpdateScore(GameState state) {
        this.gameState = state;
        ScoreText.text = "score: " + state.CorrectAnswerCount + "/" + state.TotalNumberOfRoundsInGame;
    }

    /// <summary>
    /// Displays the end game screen by activating the corresponding UI group.
    /// </summary>
    private void ShowScreen() {
        Debug.Log("---");
        Debug.Log("Unlocked Achievements:");
        foreach (var achievementDefinition in gameState.achievementsUnlockedThisRound) {
            Debug.Log(achievementDefinition.Name);
        }
        Debug.Log("---");
        EndGameGroup.SetActive(true);
    }

    /// <summary>
    /// Updates the displayed list of achievements unlocked during the current round on the end game screen.
    /// </summary>
    /// <param name="state">The current game state containing information about achievements unlocked this round.</param>
    private void UpdateDisplayedAchievements(GameState state) {
        var achievementDefinitions = state.achievementsUnlockedThisRound;
        string textToDisplay = achievementDefinitions.Aggregate("", (current, definition) => current + ", " + definition.Name);
        if(textToDisplay.Length > 0) textToDisplay = textToDisplay.Substring(2, textToDisplay.Length-2);
        if (textToDisplay == "") {
            textToDisplay = "None";
        }
        unlockedAchievementsText.text = textToDisplay;
    }
}
