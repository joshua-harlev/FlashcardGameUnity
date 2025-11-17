using UnityEngine.SceneManagement;

public class PlayingState : GameStateBase {
    public override string StateName => "Gameplay";
    
    private MathProblemType problemType;
    
    public PlayingState(MathProblemType problemType) {
        this.problemType = problemType;
    }
    
    public override void Enter() {
        base.Enter();
        SceneManager.LoadScene(SceneNames.FlashcardGame);
        Game.Instance.StartNewSession();
    }

}