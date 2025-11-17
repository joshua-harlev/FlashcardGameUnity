using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class ResultsState : GameStateBase {
    public override string StateName => "Results";

    public override void Enter() {
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        base.Enter();
        SceneManager.LoadScene(SceneNames.Results);
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1) {
        SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        GameActions.OnResultsScreenLoad?.Invoke(Game.Instance.GameSession);
    }

    public void OnAchievementsClicked() {
        Game.Instance.StateMachine.TransitionTo(new AchievementsState());
    }
}