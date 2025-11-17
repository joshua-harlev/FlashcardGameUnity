/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * ResultsState.cs | Manages results scene transitions.
 */
using UnityEngine.SceneManagement;

public class ResultsState : GameStateBase {
    public override string StateName => "Results";

    public override void Enter() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        base.Enter();
        SceneManager.LoadScene(SceneNames.Results);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameActions.OnResultsScreenLoad?.Invoke(Game.Instance.GameSession);
    }

    public void OnAchievementsClicked() {
        Game.Instance.StateMachine.TransitionTo(new AchievementsState());
    }
}