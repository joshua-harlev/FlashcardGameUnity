/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * DivisionProblem.cs | Models a division problem.
 */
using UnityEngine;
using Random = UnityEngine.Random;

public class DivisionProblem : MathProblem {
    /// <summary>
    /// Specifies the inclusive lower bound for generating random values in a division problem.
    /// </summary>
    protected override int LOWER_BOUND_INCLUSIVE { get; } = 1;

    /// <summary>
    /// Specifies the exclusive upper bound for generating random values in a division problem.
    /// </summary>
    protected override int UPPER_BOUND_EXCLUSIVE { get; } = 10;

    /// <summary>
    /// Calculates and sets the correct answer for the division problem by dividing the first and second numbers.
    /// </summary>
    protected override void SetAnswer() {
        int divisor = GetRandomMathProblemValue();
        int quotient = GetRandomMathProblemValue();
        if (divisor == 0) divisor = 1;

        int dividend = divisor * quotient;
        SetNumbers(dividend, divisor);
        answer = quotient;
    }

    /// <summary>
    /// Generates a fake answer for a division problem, ensuring it differs from the correct answer.
    /// </summary>
    /// <returns>An integer representing an incorrect answer for the division problem.</returns>
    protected override int GenerateFakeAnswer() {
        int answerOffset = Random.Range(-2, 3);
        int fakeAnswer = Mathf.Max(1, answer + answerOffset);
        if (fakeAnswer == answer) fakeAnswer++;
        return fakeAnswer;
    }

    /// <summary>
    /// Converts the division problem into its string representation in the format "FirstNumber / SecondNumber".
    /// </summary>
    /// <returns>The string representation of the division problem.</returns>
    public override string ToString() {
        return FirstNumber + " ÷ " + SecondNumber;
    }
}