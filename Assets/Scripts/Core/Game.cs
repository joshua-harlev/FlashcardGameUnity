/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * Game.cs | Manages core game logic.
 */

using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public static Game Instance { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public MathProblemType SelectedProblemType;
    public int SelectedNumberOfQuestions { get; set; } = 3;
    public GameSession GameSession { get; private set; }
    private List<AchievementDefinition> achievementDefinitions;
    
    public void StartNewSession() {
        GameSession = new GameSession(SelectedNumberOfQuestions);
    }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        StateMachine = new StateMachine();
        achievementDefinitions = AchievementSaves.LoadAllAchievementDefinitions();
        
        GameActions.OnUnlockAchievement += OnUnlockAchievement;
    }
    
    private void Start() {
        StateMachine.Initialize(StateMachine.MainMenuState);
    }
    
    private void OnDestroy() {
        GameActions.OnUnlockAchievement -= OnUnlockAchievement;
    }
    
    private void OnUnlockAchievement(string achievementId) {
        AchievementDefinition achievementDefinition = achievementDefinitions.Find(x => x.Id == achievementId);
        UnlockAchievementCommand unlockAchievementCommand = new UnlockAchievementCommand(achievementDefinition);
        unlockAchievementCommand.Execute();
        GameSession.AchievementsUnlockedThisRound.Add(achievementDefinition);
    }
}