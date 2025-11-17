/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * AchievementsState.cs | Manages achievement scene transitions.
 */
using UnityEngine.SceneManagement;

public class AchievementsState : GameStateBase {
    public override string StateName => "Achievements";

    public override void Enter() {
        base.Enter();
        SceneManager.LoadScene(SceneNames.Achievements);
    }
}