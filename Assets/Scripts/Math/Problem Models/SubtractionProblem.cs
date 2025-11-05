using UnityEngine;

public class SubtractionProblem : MathProblem {
    /// <summary>
    /// Represents the inclusive lower bound value for the generation of numbers in a subtraction problem.
    /// </summary>
    protected override int LOWER_BOUND_INCLUSIVE { get; } = 1;

    /// <summary>
    /// Represents the exclusive upper bound value used for generating numbers in a subtraction problem.
    /// </summary>
    protected override int UPPER_BOUND_EXCLUSIVE { get; } = 100;

    /// <summary>
    /// Assigns the correct answer for the current math problem by calculating it using
    /// the first and second numbers of the problem.
    /// </summary>
    protected override void SetAnswer() {
        answer = FirstNumber - SecondNumber;
    }

    /// <summary>
    /// Generates a fake answer for the current math problem, based on a random perturbation
    /// of the correct answer influenced by a difficulty coefficient.
    /// </summary>
    /// <returns>
    /// An integer representing a fake, incorrect answer for the problem.
    /// </returns>
    protected override int GenerateFakeAnswer() {
        int firstValue = GetRandomMathProblemValue();
        int secondValue = GetRandomMathProblemValue();
        if (firstValue < secondValue) {
            (firstValue, secondValue) = (secondValue, firstValue);
        }
        return firstValue - secondValue;
    }

    /// <summary>
    /// Returns a string representation of the addition problem in the format "FirstNumber + SecondNumber" for display.
    /// </summary>
    /// <returns>
    /// A string representing the addition problem.
    /// </returns>
    public override string ToString() {
        return FirstNumber.ToString() + " - " + SecondNumber.ToString();
    }
}