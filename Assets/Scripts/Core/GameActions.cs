using System;

/// <summary>
/// Provides static events to manage and handle various actions within the game lifecycle.
/// </summary>
public static class GameActions {
    /// <summary>
    /// Event triggered when the gameplay session ends.
    /// </summary>
    public static Action OnGameplayEnd;

    /// <summary>
    /// Event triggered when the game scene is loaded.
    /// </summary>
    public static Action OnGameSceneLoad;

    /// <summary>
    /// Event triggered when a new math problem is generated during gameplay.
    /// </summary>
    public static Action<MathProblem> OnProblemGenerated;

    /// <summary>
    /// Event triggered when a game round ends.
    /// </summary>
    public static Action OnRoundEnd;

    /// <summary>
    /// Event triggered when the game's state data is updated. Provides the updated <see cref="GameSession"/> object to any subscribed listeners.
    /// </summary>
    public static Action<GameSession> OnStateDataUpdate;

    /// <summary>
    /// Event triggered when an answer is selected during gameplay.
    /// Passes a boolean parameter indicating whether the selected answer is correct.
    /// </summary>
    public static Action<bool> OnAnswerSelected;

    /// <summary>
    /// Event triggered at the beginning of a new gameplay round.
    /// </summary>
    public static Action OnRoundStart;

    /// <summary>
    /// Event triggered on each tick of the round timer, providing the remaining time in seconds.
    /// </summary>
    public static Action<int> OnRoundTimerTick;

    /// <summary>
    /// Event triggered when an achievement is unlocked, providing the ID of the unlocked achievement.
    /// </summary>
    public static Action<string> OnUnlockAchievement;

    /// <summary>
    /// Event triggered when a command is executed in the game.
    /// </summary>
    public static Action<Command> OnCommandExecuted;

    public static Action OnStartButtonClick;
}