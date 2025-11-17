/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * SelectingOptionState.cs | Manages option selection state actions.
 */
using UnityEngine.SceneManagement;

public class SelectingOptionState : GameStateBase {
    public override string StateName => "SelectingOption";

    public override void Enter() {
        base.Enter();
        GameActions.OnGameOptionsSelected += OnOptionsConfirmed;
        SceneManager.LoadScene(SceneNames.OptionsScreen);
    }

    public override void Exit() {
        base.Exit();
        GameActions.OnGameOptionsSelected -= OnOptionsConfirmed;
    }

    private void OnOptionsConfirmed(MathProblemType problemType, int numberOfQuestions) {
        Game.Instance.SelectedProblemType = problemType;
        Game.Instance.SelectedNumberOfQuestions = numberOfQuestions;
        Game.Instance.StateMachine.TransitionTo(new PlayingState(problemType));
    }
}