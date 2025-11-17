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
/// Represents the main game class, controlling the flow of gameplay, including initiating rounds and managing game session.
/// </summary>
public class Game : MonoBehaviour {
    
    /// <summary>
    /// Singleton instance.
    /// </summary>
    public static Game Instance { get; private set; }
    
    public StateMachine StateMachine { get; private set; }
    
    /// <summary>
    /// Determines the type of math problem used in the game, such as addition or multiplication.
    /// This variable is used to generate problems corresponding to the selected type during gameplay.
    /// </summary>
    [SerializeField] public MathProblemType SelectedProblemType = MathProblemType.MultiplicationProblem;
    
    /// <summary>
    /// Selected number of questions for the current/next game.
    /// </summary>
    public int SelectedNumberOfQuestions { get; set; } = 3;

    /// <summary>
    /// Represents the current session of the game, including information such as the number
    /// of completed rounds, correct answers, and the total number of rounds in the game.
    /// It also provides functionality to advance the game session and determine if the game has ended.
    /// </summary>
    public GameSession GameSession { get; private set; }

    private List<AchievementDefinition> achievementDefinitions;

    /// <summary>
    /// Invoked when the script instance is being loaded.
    /// Subscribes the HandleAnswerChosen method to the OnAnswerSelected event,
    /// enabling the handling of user-selected answers during gameplay.
    /// Loads all achievement definitions.
    /// </summary>
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        StateMachine = new StateMachine();
        GameActions.OnAnswerSelected += HandleAnswerChosen;
        GameActions.OnRoundTimerTick += UpdateTimeRemaining;
        GameActions.OnUnlockAchievement += OnUnlockAchievement;
        achievementDefinitions = AchievementSaves.LoadAllAchievementDefinitions();
    }

    /// <summary>
    /// Called on the initial frame when the script is active.
    /// Responsible for starting the game.
    /// Starts the countdown, updating the info display,
    /// and subscribing to necessary events.
    /// </summary>
    private void Start() {
        StateMachine.Initialize(StateMachine.MainMenuState);
    }

    /// <summary>
    /// Called when the MonoBehaviour is being destroyed.
    /// Clears event subscriptions and resets the game session.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnAnswerSelected -= HandleAnswerChosen;
        GameActions.OnRoundTimerTick -= UpdateTimeRemaining;
        GameActions.OnUnlockAchievement -= OnUnlockAchievement;
    }

    public void StartNewSession() {
        GameSession = new GameSession(SelectedNumberOfQuestions);
    }

    /// <summary>
    /// Initiates a new game round or handles game session transitions when the game is over.
    /// If the game is not over, starts the next round, generates a new math problem,
    /// updates the flashcard display, and begins the round countdown.
    /// </summary>
    public void PlayRound() {
        if (GameSession.CurrentRoundNumber == 0) {
            GameSession.AchievementsUnlockedThisRound.Clear();
            AchievementEvents.OnGameStart?.Invoke();
        }
        if (GameSession.GameIsOver) {
            AchievementEvents.OnGameComplete?.Invoke(GameSession.CorrectAnswerCount, SelectedProblemType, SelectedNumberOfQuestions);
            GameActions.OnStateDataUpdate?.Invoke(GameSession);
            
            ResultsState resultsState = new ResultsState();
            StateMachine.TransitionTo(resultsState);
            return;
        }
        GameSession.AdvanceRound();
        GameActions.OnStateDataUpdate?.Invoke(GameSession);
        GenerateQuestionCommand generateQuestionCommand = new GenerateQuestionCommand(SelectedProblemType);
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
        GameActions.OnStateDataUpdate?.Invoke(GameSession);
    }
    
    private void OnUnlockAchievement(string achievementId) {
        AchievementDefinition achievementDefinition = achievementDefinitions.Find(x => x.Id == achievementId);
        UnlockAchievementCommand unlockAchievementCommand = new UnlockAchievementCommand(achievementDefinition);
        unlockAchievementCommand.Execute();
        GameSession.AchievementsUnlockedThisRound.Add(achievementDefinition);
    }

    private void UpdateTimeRemaining(int timeRemaining) {
        GameSession.CurrentTimeRemaining = timeRemaining;
    }
    
    /// <summary>
    /// Handles the chosen answer by registering it to the game session class,
    /// ending the current round, and starting a new round.
    /// </summary>
    /// <param name="isCorrect">Indicates whether the chosen answer is correct.</param>
    private void HandleAnswerChosen(bool isCorrect) {
        GameSession.RegisterAnswer(isCorrect);
        EndRound();
        PlayRound();
    }
}
