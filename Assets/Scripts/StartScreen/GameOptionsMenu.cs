using UnityEngine;

public class GameOptionsMenu : MonoBehaviour {
    public ButtonToggleGroup NumQuestionsToggleGroup;
    public ButtonToggleGroup ProblemTypeToggleGroup;

    private MathProblemType mathProblemType;
    private int numberOfQuestions;
    
    public void OnStartButtonClick() {
        if (string.IsNullOrWhiteSpace(NumQuestionsToggleGroup.SelectedValue)) {
            return;
        }

        if (string.IsNullOrWhiteSpace(ProblemTypeToggleGroup.SelectedValue)) {
            return;
        }
        
        SetProblemType();
        SetNumberOfQuestions();

        if (Game.Instance.StateMachine.CurrentState is SelectingOptionState selectingOptionState) {
            SelectingOptionState.OnOptionsConfirmed(mathProblemType, numberOfQuestions);
        }
        else {
            Debug.LogError("GameOptionsMenu: current state is not SelectingOptionState.");
        }
    }

    private void SetNumberOfQuestions() {
        numberOfQuestions = int.Parse(NumQuestionsToggleGroup.SelectedValue);
        if (numberOfQuestions < 3 || numberOfQuestions > 5) {
            Debug.LogWarning("Number of questions was not between 3 and 5. Constraining.");
            numberOfQuestions = Mathf.Clamp(numberOfQuestions, 3, 5);
        }
    }

    private void SetProblemType() {
        switch(ProblemTypeToggleGroup.SelectedValue)
        {
            case "+":
                mathProblemType = MathProblemType.AdditionProblem;
                break;
            case "−":
                mathProblemType = MathProblemType.SubtractionProblem;
                break;
            case "×":
                mathProblemType = MathProblemType.MultiplicationProblem;
                break;
            case "÷":
                mathProblemType = MathProblemType.DivisionProblem;
                break;
            case "?":
                mathProblemType = MathProblemType.AllProblems;
                break;
            default:
                mathProblemType = MathProblemType.Unset;
                Debug.LogWarning("Math problem type not set in GameOptionsMenu.");
                break;
        }
    }
}