using UnityEngine.SceneManagement;

public class ResultsState : IGameState {
    public void Enter() {
        DebugLogger.Log(LogChannel.Systems, "Entering ResultsState");
    }

    public void Exit() {
        DebugLogger.Log(LogChannel.Systems, "Exiting ResultsState");
    }

    public void OnRestartButtonPressed() {
        SelectingOptionState selectingOptionState = new SelectingOptionState();
        Game.Instance.StateMachine.TransitionTo(selectingOptionState);
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    public void OnAchievementsClicked() {
        AchievementsState achievementsState = new AchievementsState();
        Game.Instance.StateMachine.TransitionTo(achievementsState);
        SceneManager.LoadScene("Scenes/Achievements");
    }
}