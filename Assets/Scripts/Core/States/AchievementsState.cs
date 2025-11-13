using UnityEngine.SceneManagement;

public class AchievementsState : IGameState {
    public void Enter() {
        DebugLogger.Log(LogChannel.Systems, $"Entering AchievementsState");
    }

    public void Exit() {
        DebugLogger.Log(LogChannel.Systems, $"Exiting AchievementsState");
    }

    public void OnBackButtonPressed() {
        MainMenuState mainMenuState = new MainMenuState();
        Game.Instance.StateMachine.TransitionTo(mainMenuState);
        SceneManager.LoadScene("Scenes/TitleScreen");
    }
    
    
}