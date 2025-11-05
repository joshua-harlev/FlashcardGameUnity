public class SelectingOptionState : IGameState {
    public void Enter() {
        DebugLogger.Log(LogChannel.Systems, "Entering SelectingOptionState");
    }

    public void Exit() {
        DebugLogger.Log(LogChannel.Systems, "Exiting SelectingOptionState");
    }

    public static void OnOptionsConfirmed(MathProblemType problemType, int numberOfQuestions) {
        Game.Instance.SelectedProblemType = problemType;
        Game.Instance.SelectedNumberOfQuestions = numberOfQuestions;
        PlayingState playingState = new PlayingState(problemType);
        Game.Instance.StateMachine.TransitionTo(playingState);
    }
}