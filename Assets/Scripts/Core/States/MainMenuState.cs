using UnityEngine.SceneManagement;

public class MainMenuState : IGameState {
    public void Enter() {
        DebugLogger.Log(LogChannel.Systems, $"Entering MainMenuState");
    }

    public void Exit() {
        DebugLogger.Log(LogChannel.Systems, $"Exiting MainMenuState");
    }

    public void OnStartButtonPressed() {
        SelectingOptionState selectingOptionState = new SelectingOptionState();
        Game.Instance.StateMachine.TransitionTo(selectingOptionState);
        GameActions.OnStartButtonClick?.Invoke();
    }

    public void OnAchievementsButtonPressed() {
        AchievementsState achievementsState = new AchievementsState();
        Game.Instance.StateMachine.TransitionTo(achievementsState);
        SceneManager.LoadScene("Scenes/Achievements");
    }
}