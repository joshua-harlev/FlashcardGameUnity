using UnityEngine.SceneManagement;

public class SelectingOptionState : GameStateBase {
    public override string StateName => "SelectingOptionState";

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