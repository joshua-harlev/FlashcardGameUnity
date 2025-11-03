using UnityEngine;
/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA2
 * Overview:
 * GenerateQuestionCommand.cs | Generate Question Command Class
 */

/// This class represents a specific implementation of the Command pattern.
/// It encapsulates a math problem and the problem type.
public class GenerateQuestionCommand : Command {
    private MathProblemType problemType;
    public MathProblem Problem;
    public GenerateQuestionCommand(MathProblemType problemType) {
        this.problemType = problemType;
    }
    
    public override void Execute() {
        switch (problemType) {
            case MathProblemType.MultiplicationProblem:
                this.Problem = new MultiplicationProblem();
                break;
            case MathProblemType.AdditionProblem:
                this.Problem = new AdditionProblem();
                break;
            case MathProblemType.Unset:
                Debug.Log("Warning: No math problem type set. Defaulting to multiplication.");
                this.Problem = new MultiplicationProblem();
                break;
            default:
                Debug.Log("Warning: Unknown math problem type set in Game. Defaulting to multiplication.");
                this.Problem = new MultiplicationProblem();
                break;
        }
        GameActions.OnCommandExecuted?.Invoke(this);
    }

    public override string GetDetails() {
        var answerList = Problem.GetAnswerList();
        string answersString = "";
        for (int i = 0; i < answerList.Count; i++) {
            answersString += answerList[i];
            if (Problem.IsCorrect(answerList[i])) answersString += " (correct answer)"; 
            if (i != answerList.Count - 1)
                answersString += ", ";
        }
        return $"Generated {problemType} with question {Problem}.\n\tAnswers were {answersString}.";
    }
}