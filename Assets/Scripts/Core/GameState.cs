using System.Collections.Generic;

/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * GameState.cs | Manages game state variables.
 */

/// <summary>
/// Responsible for tracking and managing the game's overall state, including the current round,
/// the number of correct answers, and the total number of rounds.
/// </summary>
public class GameState {
    /// <summary>
    /// Create a new GameState object
    /// </summary>
    /// <param name="TotalNumberOfRounds">Total number of rounds to play</param>
    public GameState(int TotalNumberOfRounds = 3) {
        TotalNumberOfRoundsInGame = TotalNumberOfRounds;
    }

    /// <summary>
    /// Tracks the current round number in the game.
    /// </summary>
    public int CurrentRoundNumber { get; private set; } = 0;
    
    public int CurrentTimeRemaining { get; set; } = 0;

    /// <summary>
    /// Tracks the total count of correctly answered questions during the game.
    /// </summary>
    public int CorrectAnswerCount { get; private set; }

    /// <summary>
    /// The total number of rounds in the game.
    /// </summary>
    public int TotalNumberOfRoundsInGame { get; private set; } = 3;
    
    public List<AchievementDefinition> achievementsUnlockedThisRound = new List<AchievementDefinition>();
    

    /// <summary>
    /// Indicates whether the game is over based on the current round count reaching the set number of rounds.
    /// </summary>
    public bool GameIsOver => (CurrentRoundNumber >= TotalNumberOfRoundsInGame);

    /// <summary>
    /// Advances the game to the next round by incrementing the round count if the game is not over.
    /// </summary>
    public void AdvanceRound() {
        if (!GameIsOver) {
            CurrentRoundNumber++;
        }
    }

    /// <summary>
    /// Registers an answer as correct or incorrect and updates the correct answer count and achievements.
    /// </summary>
    /// <param name="isCorrect">Specifies whether the provided answer is correct.</param>
    public void RegisterAnswer(bool isCorrect) {
        if (isCorrect) {
            CorrectAnswerCount++;
            AchievementEvents.OnAnswerQuestionCorrectly?.Invoke(CurrentTimeRemaining);
        }
    }

    /// <summary>
    /// Resets the game state variables.
    /// </summary>
    public void Reset() {
        CurrentRoundNumber = 0;
        CorrectAnswerCount = 0;
        achievementsUnlockedThisRound.Clear();
    }
}