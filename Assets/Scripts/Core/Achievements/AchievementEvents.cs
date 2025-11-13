using System;

/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * AchievementEvents.cs | Achievement Event Delegator
 */

/// Overview:
/// Static class for managing and invoking achievement-related events within the game.
/// Provides a centralized system to broadcast various game milestone events (e.g., game start, question answered correctly) for achievement tracking.
public static class AchievementEvents {
    public static Action<int> OnAnswerQuestionCorrectly;
    public static Action<int, MathProblemType, int> OnGameComplete;
    public static Action OnEquationClicked;
    public static Action OnGameStart;
}
