/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * MainMenuState.cs | Manages main menu scene transitions.
 */
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