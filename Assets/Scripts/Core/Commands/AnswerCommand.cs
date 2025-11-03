/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * AnswerCommand.cs | Answer Command Class
 */

/// This class represents a specific implementation of the Command pattern.
/// It encapsulates the user's answer and whether it was correct.
public class AnswerCommand : Command {
    private readonly bool wasCorrectAnswer = false;
    private readonly int answer = -1;

    public AnswerCommand(bool wasCorrectAnswer, int answer) {
        this.wasCorrectAnswer = wasCorrectAnswer;
        this.answer = answer;
    }

    public override void Execute() {
        GameActions.OnCommandExecuted?.Invoke(this);
    }
    
    public override string GetDetails() {
        string wasCorrectAnswerAsString = wasCorrectAnswer ? "correct" : "incorrect";
        return $"User answered {answer}, which was {wasCorrectAnswerAsString}.";
    }
}