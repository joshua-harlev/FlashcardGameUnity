using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * Achievements.cs | Achievements logic class
 */

/// <summary>
/// Designed to handle achievement-related functionality within a game.
/// </summary>
public class Achievements : MonoBehaviour {
    #region Variable Declarations
    // tracks all of the achievement IDs and their progress
    private Dictionary<string, AchievementProgress> achievementProgressTable;
    
    // Mathematician Variables:
    public int NumberOfAchievements { get; private set; }
    private int numberOfUnlockedAchievements;
    
    // Math Master Variables:
    private int mathMasterProgress = 0;
    
    // A+ Variables
    private const int numberOfProblems = 3;
    private int numberOfPerfectGamesCompleted;
    private int APlusUnlock1Threshold = 1;
    private int APlusUnlock2Threshold = 10;
    private int APlusUnlock3Threshold = 30;
    
    // Perfect Achievement Variables
    private readonly string perfectDivisionID = "perfectdivider";
    private readonly string perfectAdditionID = "perfectadder";
    private readonly string perfectSubtractionID = "perfectsubtractor";
    private readonly string perfectMultiplicationID = "perfectmultiplier";
    private readonly string perfectAllID = "perfectmath";
    #endregion
    
    #region Setup
    private void Awake() {
        // Set up subscriptions
        AchievementEvents.OnGameStart += OnGameStart;
        AchievementEvents.OnGameComplete += OnGameComplete;
        AchievementEvents.OnEquationClicked += OnEquationClicked;
        AchievementEvents.OnAnswerQuestionCorrectly += OnAnswerQuestionCorrectly;
        
        // Load achievements from disk
        achievementProgressTable = AchievementSaves.LoadAchievements();
        
        PrintAchievementsFromDisk();
        
        SetUpAPlusVariables();
        SetUpMathematicianVariables();
    }
    
    private void SetUpAPlusVariables() {
        numberOfPerfectGamesCompleted = GetProgress("APlus3");
    }

    private void SetUpMathematicianVariables() {
        foreach (AchievementProgress achievementProgress in achievementProgressTable.Values) {
            if (achievementProgress.Id == "mathematician") continue;
            NumberOfAchievements++;
            if(achievementProgress.IsUnlocked) numberOfUnlockedAchievements++;
        }
    }
    
    #endregion

    #region Teardown
    private void OnDestroy() {
        AchievementEvents.OnGameStart -= OnGameStart;
        AchievementEvents.OnGameComplete -= OnGameComplete;
        AchievementEvents.OnEquationClicked -= OnEquationClicked;
        AchievementEvents.OnAnswerQuestionCorrectly -= OnAnswerQuestionCorrectly;
    }
    #endregion
    
    #region Event Delegators
    private void OnGameComplete(int correctAnswers, MathProblemType mathProblemType, int selectedNumberOfQuestions) {
        TrackAPlus(correctAnswers, selectedNumberOfQuestions);
        TrackDailyStreak();
        TrackPerfectScores(correctAnswers, mathProblemType, selectedNumberOfQuestions);
    }

    private void TrackPerfectScores(int correctAnswers, MathProblemType mathProblemType, int selectedNumberOfQuestions) {
        if (correctAnswers != selectedNumberOfQuestions) return;
        switch (mathProblemType) {
            case MathProblemType.AdditionProblem:
                if (AchievementIsUnlocked(perfectAdditionID)) return;
                UnlockAchievement(perfectAdditionID);
                break;
            case MathProblemType.SubtractionProblem:
                if (AchievementIsUnlocked(perfectSubtractionID)) return;
                UnlockAchievement(perfectSubtractionID);
                break;
            case MathProblemType.MultiplicationProblem:
                if (AchievementIsUnlocked(perfectMultiplicationID)) return;
                UnlockAchievement(perfectMultiplicationID);
                break;
            case MathProblemType.DivisionProblem:
                if (AchievementIsUnlocked(perfectDivisionID)) return;
                UnlockAchievement(perfectDivisionID);
                break;
            case MathProblemType.AllProblems:
                if (AchievementIsUnlocked(perfectAllID)) return;
                UnlockAchievement(perfectAllID);
                break;
            case MathProblemType.Unset:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mathProblemType), mathProblemType, null);
        }
    }

    private void OnAnswerQuestionCorrectly(int timeRemaining) {
        TrackThatWasClose(timeRemaining);
        TrackMathMaster(timeRemaining);
        if (!AchievementIsUnlocked("inforthelonghaul_1")) {
            TrackInForTheLongHaul("inforthelonghaul_1", 15);
        } 
        if (!AchievementIsUnlocked("inforthelonghaul_2")) {
            TrackInForTheLongHaul("inforthelonghaul_2", 30);
        } 
        if (!AchievementIsUnlocked("inforthelonghaul_3")) {
            TrackInForTheLongHaul("inforthelonghaul_3", 60);
        }
    }
    #endregion
    
    #region Achievements
    // Achievement Name: Mathematician
    // Achievement IDs: mathematician
    // Team Members: Ren Peng, Ryn Reid
    private void TrackMathematician() {
        if (AchievementIsUnlocked("mathematician")) {
            Debug.Log("Mathematician already unlocked.");
            return;
        }

        numberOfUnlockedAchievements++;
        SetProgress("mathematician", numberOfUnlockedAchievements);
        if (numberOfUnlockedAchievements == NumberOfAchievements) {
            UnlockAchievement("mathematician");
        }
    }
    

    // Achievement Name: Huh?
    // Achievement IDs: huh
    // Team Members: J. Harlev, Angela German, Carina Chan
    private void OnEquationClicked()
    {
        if (AchievementIsUnlocked("huh")) {
            Debug.Log("Equation was clicked but achievement was already unlocked.");
            return;
        }
        UnlockAchievement("huh");
    }
    
    // Achievement Name: Daily Streak
    // Achievement IDs: dailyStreak
    // Team Members: Tatiana Gasparre, Nathan Jones
    private void TrackDailyStreak() { 
        string achievementId = "dailyStreak";
        if (AchievementIsUnlocked(achievementId)) { return; }
        int progress = GetProgress(achievementId);
        System.DateTime currentDate = System.DateTime.Now.Date;
        System.DateTime? prevDate = null;
        string prevDateStr = GetCustomProgress(achievementId);
        if (prevDateStr != "")
        {
            prevDate = System.DateTime.Parse(prevDateStr);
        }

        if (prevDate == null) // first day no stored prevDate
        {
            Debug.Log("Start of streak. Incrementing Daily Streak Achievement");
            IncrementProgress(achievementId);
            prevDate = currentDate;
            SetCustomProgress(achievementId, prevDate.ToString());
        }
        else if (currentDate == prevDate.Value)// same day
        {
            //nothing
            Debug.Log("Same day no change to progress for Daily Streak Achievement");
        }
        else if ((currentDate - prevDate.Value).Days == 1) // more than a day in a row
        {
            Debug.Log("1 Day has passed. Incrementing Daily Streak Achievement");
            IncrementProgress(achievementId);
            prevDate = currentDate;
            SetCustomProgress(achievementId, prevDate.ToString());
        }
        else // starting new streak
        {
            Debug.Log("Start of streak. Incrementing Daily Streak Achievement");
            ResetProgress(achievementId);
            IncrementProgress(achievementId);
            prevDate = currentDate;
            SetCustomProgress(achievementId, prevDate.ToString());
        }

        progress = GetProgress(achievementId);
        if (progress >= 5) // progress equals 5 unlock achievement
        {
            UnlockAchievement(achievementId);
            Debug.Log("Daily Streak Achievement has been unlocked.");
        }
        Debug.Log($"Current Daily Streak Achievement Progress: {progress}"); 
    }

    // Achievement Name: A+
    // Achievement IDs: APlus1, APlus2, APlus3
    // Team Members: Brandon Wong, Kurt Lim, Savana Chou
    private void TrackAPlus(int correctAnswers, int selectedNumberOfQuestions)
    {
        string achievement1Id = "APlus1";
        string achievement2Id = "APlus2";
        string achievement3Id = "APlus3";
        if (AchievementIsUnlocked(achievement3Id)) {
            Debug.Log("A Plus 3: round was finished but achievement was already unlocked.");
            return;
        }
        if (correctAnswers == selectedNumberOfQuestions)
        {
            Debug.Log("Incrementing APlus");
            ++numberOfPerfectGamesCompleted;
            IncrementProgress(achievement3Id);

            if (!AchievementIsUnlocked(achievement1Id)) {
                IncrementProgress(achievement1Id);
                IncrementProgress(achievement2Id);
                if (numberOfPerfectGamesCompleted == APlusUnlock1Threshold)
                {
                    Debug.Log("Reached tier 1 of APlus");
                    UnlockAchievement(achievement1Id);
                }
            } else if (!AchievementIsUnlocked(achievement2Id)) {
                IncrementProgress(achievement2Id);
                if (numberOfPerfectGamesCompleted == APlusUnlock2Threshold)
                {
                    Debug.Log("Reached tier 2 of APlus");
                    UnlockAchievement(achievement2Id);
                }

            } else if (numberOfPerfectGamesCompleted == APlusUnlock3Threshold) {
                Debug.Log("UnlockAchievement APlus");
                UnlockAchievement(achievement3Id);
            }
        }
    }

    // Achievement Name: That Was Close!
    // Achievement IDs: thatwasclose
    // Team Members: Daniel Ahn, Shea Nee
    private void TrackThatWasClose(int timeRemaining) {
        if (timeRemaining > 3) return;
        if (AchievementIsUnlocked("thatwasclose"))
        {
            Debug.Log("That Was Close achievement was already unlocked.");
            return;
        }
        UnlockAchievement("thatwasclose");
        Debug.Log("That Was Close achievement was unlocked");
    }

    // Achievement Name: Math Master
    // Achievement IDs: mathmaster
    // Team Members: Laurel Latt, Nicole Citardi, Davis Wood
    private void OnGameStart() {
        mathMasterProgress = 0;
    }
    private void TrackMathMaster(int timeRemaining) {
        if (timeRemaining >= 7) {
            mathMasterProgress++;
        }

        if (mathMasterProgress == 3) {
            if (AchievementIsUnlocked("mathmaster")) {
                return;
            }
            UnlockAchievement("mathmaster");
        }    
    }

    // Achievement Name: In For The Long Haul
    // Achievement IDs: inforthelonghaul_1, inforthelonghaul_2, inforthelonghaul_3
    // Team Members: Carolina Carabello Vélez, Devon Tjong, Daniel Min
    private void TrackInForTheLongHaul(string achievementID, int requiredProgress) {
        if (AchievementIsUnlocked(achievementID)) { return; }

        IncrementProgress(achievementID);
        int progress = GetProgress(achievementID);
        Debug.Log($"{achievementID} progress: {progress}");

        if (progress >= requiredProgress) { 
            UnlockAchievement(achievementID); 
        }
    }
    #endregion
    
    #region Data Management

    /// <summary>
    /// Unlocks an achievement within the game by telling the save system to unlock the achievement.
    /// Also unlocks the event in-memory and tracks the mathematician achievement.
    /// </summary>
    /// <param name="achievementId">The unique identifier for the achievement to be unlocked.</param>
    private void UnlockAchievement(string achievementId) {
        Debug.Log("Unlocked achievement " + achievementId);
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        AchievementSaves.Unlock(achievementId);
        if(achievementId != "mathematician") {
            TrackMathematician();
        }
        if (achievementProgress == null) {
            Debug.Log($"Achievement {achievementId} was (not actually) unlocked. Please investigate.");
            return;
        }
        achievementProgress.UnlockAchievement();
        GameActions.OnUnlockAchievement?.Invoke(achievementId);
    }

    private bool AchievementIsUnlocked(string achievementId) {
        achievementProgressTable.TryGetValue(achievementId, out var value);
        if (value != null) {
            return value.IsUnlocked;
        }
        if(value == null) Debug.LogError($"Achievement {achievementId} was (not actually) unlocked; achievement progress was null.");
        return false;
    }

    private string GetCustomProgress(string achievementId) {
        achievementProgressTable.TryGetValue(achievementId, out var value);
        if (value != null) {
            return value.CustomProgress;
        }
        Debug.Log("Custom progress missing for " + achievementId);
        return "";
    }

    private void SetCustomProgress(string achievementId, string value) {
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        if(achievementProgress != null) {
            achievementProgress.SetCustomProgress(value);
        }
        else {
            Debug.LogError($"Tried to set custom progress for {achievementId}, but progress object is null. Please investigate.");
        }
        AchievementSaves.SetCustomProgress(achievementId, value);
    }

    private void IncrementProgress(string achievementId) {
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        if(achievementProgress != null) {
            achievementProgress.IncrementProgress();
        }
        else {
            Debug.LogError($"Tried to increment progress for {achievementId}, but progress object is null. Please investigate.");
        }
        AchievementSaves.IncrementProgress(achievementId);
    }
    
    private void SetProgress(string achievementId, int amount) {
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        if(achievementProgress != null) {
            achievementProgress.SetProgress(amount);
        }
        else {
            Debug.LogError($"Tried to set progress for {achievementId}, but progress object is null. Please investigate.");
        }
        AchievementSaves.SetProgress(achievementId, amount);
    }

    private int GetProgress(string achievementId) {
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        if (achievementProgress != null) {
            return achievementProgress.Progress;
        }
		Debug.LogError("Could not get progress for " + achievementId);
        return 0;
    }
    
    // from Tatiana Gasparre and Nathan Jones' code
    private void ResetProgress(string achievementId)
    {
        achievementProgressTable.TryGetValue(achievementId, out var achievementProgress);
        if (achievementProgress != null)
        {
            achievementProgress.ResetProgress();
        }
        else
        {
            Debug.LogError($"Tried to reset progress for {achievementId}, but progress object is null. Please investigate.");
        }
        AchievementSaves.ResetProgress(achievementId);
    }
    
    #endregion
    
    #region Other Helpers
    private static void PrintAchievementsFromDisk() {
        Debug.Log("-------------------");
        Debug.Log("Printing all achievements from disk:");
        AchievementSaves.PrintAllAchievementsFromDisk();
        Debug.Log("-------------------");
    }
    #endregion
    
}