/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * UnlockAchievementCommand.cs | Unlock Achievement Command Class
 */

/// This class represents a specific implementation of the Command pattern.
/// It encapsulates an unlocked achievement.
public class UnlockAchievementCommand : Command {
    private readonly AchievementDefinition achievementDefinition;
    public UnlockAchievementCommand(AchievementDefinition achievementDefinition) {
        this.achievementDefinition = achievementDefinition;
    }
    public override void Execute() {
        GameActions.OnCommandExecuted?.Invoke(this);
    }

    public override string GetDetails() {
        return achievementDefinition.Name + " unlocked.";
    }
}