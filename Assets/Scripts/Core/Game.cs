/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * Game.cs | Manages core game logic.
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the main game class, controlling the flow of gameplay, including initiating rounds and managing game state.
/// </summary>
public class Game : MonoBehaviour {
    
    /// <summary>
    /// Determines the type of math problem used in the game, such as addition or multiplication.
    /// This variable is used to generate problems corresponding to the selected type during gameplay.
    /// </summary>
    [SerializeField] private MathProblemType MathProblemType = MathProblemType.MultiplicationProblem;

    /// <summary>
    /// Represents the current state of the game, including information such as the number
    /// of completed rounds, correct answers, and the total number of rounds in the game.
    /// It also provides functionality to advance the game state and determine if the game has ended.
    /// </summary>
    private GameState gameState;

    private List<AchievementDefinition> achievementDefinitions;

    /// <summary>
    /// Invoked when the script instance is being loaded.
    /// Subscribes the HandleAnswerChosen method to the OnAnswerSelected event,
    /// enabling the handling of user-selected answers during gameplay.
    /// Loads all achievement definitions.
    /// </summary>
    private void Awake() {
        GameActions.OnAnswerSelected += HandleAnswerChosen;
        GameActions.OnRoundTimerTick += UpdateTimeRemaining;
        GameActions.OnUnlockAchievement += OnUnlockAchievement;
        achievementDefinitions = AchievementSaves.LoadAllAchievementDefinitions();
    }

    private void OnUnlockAchievement(string achievementId) {
        AchievementDefinition achievementDefinition = achievementDefinitions.Find(x => x.Id == achievementId);
        UnlockAchievementCommand unlockAchievementCommand = new UnlockAchievementCommand(achievementDefinition);
        unlockAchievementCommand.Execute();
        gameState.achievementsUnlockedThisRound.Add(achievementDefinition);
    }

    /// <summary>
    /// Called on the initial frame when the script is active.
    /// Responsible for starting the game.
    /// Starts the countdown, updating the info display,
    /// and subscribing to necessary events.
    /// </summary>
    private void Start() {
        gameState = new GameState();
        GameActions.OnGameSceneLoad?.Invoke();
    }

    /// <summary>
    /// Called when the MonoBehaviour is being destroyed.
    /// Clears event subscriptions and resets the game state.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnAnswerSelected -= HandleAnswerChosen;
        GameActions.OnRoundTimerTick -= UpdateTimeRemaining;
        GameActions.OnUnlockAchievement -= OnUnlockAchievement;
    }

    /// <summary>
    /// Initiates a new game round or handles game state transitions when the game is over.
    /// If the game is not over, starts the next round, generates a new math problem,
    /// updates the flashcard display, and begins the round countdown.
    /// </summary>
    public void PlayRound() {
        if (gameState.CurrentRoundNumber == 0) {
            gameState.achievementsUnlockedThisRound.Clear();
            AchievementEvents.OnGameStart?.Invoke();
        }
        if (gameState.GameIsOver) {
            AchievementEvents.OnGameComplete?.Invoke(gameState.CorrectAnswerCount);
            GameActions.OnStateDataUpdate?.Invoke(gameState);
            GameActions.OnGameplayEnd?.Invoke();
            return;
        }
        gameState.AdvanceRound();
        GameActions.OnStateDataUpdate?.Invoke(gameState);
        GenerateQuestionCommand generateQuestionCommand = new GenerateQuestionCommand(MathProblemType);
        generateQuestionCommand.Execute();
        MathProblem problem = generateQuestionCommand.Problem;
        GameActions.OnProblemGenerated?.Invoke(problem);
        GameActions.OnRoundStart?.Invoke();
    }
    
    
    /// <summary>
    /// Ends the current game round by stopping the countdown timer
    /// and updating the info display.
    /// </summary>
    public void EndRound() {
        GameActions.OnRoundEnd?.Invoke();
        GameActions.OnStateDataUpdate?.Invoke(gameState);
    }

    private void UpdateTimeRemaining(int timeRemaining) {
        gameState.CurrentTimeRemaining = timeRemaining;
    }
    

    /// <summary>
    /// Handles the chosen answer by registering it to the game state class,
    /// ending the current round, and starting a new round.
    /// </summary>
    /// <param name="isCorrect">Indicates whether the chosen answer is correct.</param>
    private void HandleAnswerChosen(bool isCorrect) {
        gameState.RegisterAnswer(isCorrect);
        EndRound();
        PlayRound();
    }
}
