/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * FlashcardDisplay.cs | Manages Flashcard display panel
 */
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays flashcard questions and answers in a game context.
/// </summary>
public class FlashcardDisplay : MonoBehaviour {
    
    /// <summary>
    /// Represents a button for answering a flashcard question, including its associated UI text and click functionality.
    /// </summary>
    [Serializable]
    public class AnswerButton {
        public Button button;
        public TMP_Text text;
    }

    /// <summary>
    /// UI element that displays the current flashcard question text.
    /// </summary>
    public TMP_Text QuestionText;

    /// <summary>
    /// List of answer buttons displayed to the user, each representing a possible answer to the current flashcard question.
    /// </summary>
    public List<AnswerButton> AnswerButtons;

    /// <summary>
    /// Tracks the current math problem displayed on the flashcard.
    /// </summary>
    private MathProblem currentProblem;

    /// <summary>
    /// Initializes the FlashcardDisplay by subscribing to the OnProblemGenerated event.
    /// Ensures that the UI updates whenever a new problem is generated.
    /// </summary>
    private void Awake() {
        GameActions.OnProblemGenerated += UpdateAll;
    }

    /// <summary>
    /// Cleans up event subscriptions and resources associated with the FlashcardDisplay before the object is destroyed.
    /// Unsubscribes from the OnProblemGenerated action in the GameActions class to prevent memory leaks or null reference errors.
    /// </summary>
    private void OnDestroy() {
        GameActions.OnProblemGenerated -= UpdateAll;
    }

    /// <summary>
    /// Updates the question and answer UI elements of the flashcard display based on the given MathProblem.
    /// </summary>
    /// <param name="problem">The MathProblem object containing the question and answer data to update the UI.</param>
    private void UpdateAll(MathProblem problem) {
        UpdateProblem(problem);
        UpdateAnswers(problem);
    }

    /// <summary>
    /// Updates the question text displayed on the flashcard UI based on the given MathProblem.
    /// </summary>
    /// <param name="problem">The MathProblem object that provides the question data to be displayed.</param>
    private void UpdateProblem(MathProblem problem) {
        QuestionText.text = "What's " + problem.ToString() + "?";
    }

    /// <summary>
    /// Updates the answer buttons on the flashcard display with the possible answers from the given MathProblem.
    /// </summary>
    /// <param name="problem">The MathProblem object containing the answers to display on the buttons.</param>
    private void UpdateAnswers(MathProblem problem) {
        currentProblem = problem;
        var answers = problem.GetAnswerList();
        for (int i = 0; i < answers.Count; i++) {
            int answer = answers[i];
            AnswerButtons[i].text.text = answer.ToString();
            AnswerButtons[i].button.onClick.RemoveAllListeners();
            AnswerButtons[i].button.onClick.AddListener(() => OnAnswerSelected(answer));
        }
    }

    /// <summary>
    /// Handles the logic triggered when a user selects an answer, determines if the selected answer is correct, and invokes the appropriate callback.
    /// </summary>
    /// <param name="chosenAnswer">The answer selected by the user to be evaluated.</param>
    private void OnAnswerSelected(int chosenAnswer) {
        bool isCorrect = currentProblem.IsCorrect(chosenAnswer);
        AnswerCommand answerCommand = new AnswerCommand(isCorrect, chosenAnswer);
        answerCommand.Execute();
        GameActions.OnAnswerSelected?.Invoke(isCorrect);
    }
}
