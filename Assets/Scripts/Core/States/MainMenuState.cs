using UnityEngine.SceneManagement;

public class MainMenuState : GameStateBase {
    public override string StateName => "MainMenu";

    public override void Enter() {
        base.Enter();
        if(SceneManager.GetActiveScene().name != SceneNames.TitleScreen) {
            SceneManager.LoadScene(SceneNames.TitleScreen);
        }
    }

    public void OnStartButtonPressed() {
        Game.Instance.StateMachine.TransitionTo(new SelectingOptionState());
    }

    public void OnAchievementsButtonPressed() {
        Game.Instance.StateMachine.TransitionTo(new AchievementsState());
    }
}