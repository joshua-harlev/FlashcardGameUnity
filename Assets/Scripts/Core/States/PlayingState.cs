using UnityEngine.SceneManagement;

public class PlayingState : IGameState {
    private MathProblemType problemType;
    
    public PlayingState(MathProblemType problemType) {
        this.problemType = problemType;
    }
    public void Enter() {
        DebugLogger.Log(LogChannel.Systems, $"Entering PlayingState: {problemType}");
        Game.Instance.StartNewSession();
        SceneManager.LoadScene("FlashcardGame");
    }

    public void Exit() {
        DebugLogger.Log(LogChannel.Systems, $"Exiting PlayingState: {problemType}");
    }
}