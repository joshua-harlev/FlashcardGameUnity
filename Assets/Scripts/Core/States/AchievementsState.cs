using UnityEngine.SceneManagement;

public class AchievementsState : GameStateBase {
    public override string StateName => "Achievements";

    public override void Enter() {
        base.Enter();
        SceneManager.LoadScene(SceneNames.Achievements);
    }
    
}