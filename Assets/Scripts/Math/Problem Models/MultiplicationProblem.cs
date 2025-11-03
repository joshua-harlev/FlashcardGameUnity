/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * For: PA1
 * Overview:
 * MultiplicationProblem.cs | Models a multiplication problem.
 */

/// <summary>
/// Represents a specific implementation of a math problem for multiplication.
/// </summary>
/// <remarks>
/// The class generates multiplication problems using two numbers and calculates
/// the correct and fake answers. It extends the MathProblem class.
/// </remarks>
public class MultiplicationProblem : MathProblem {
    /// <summary>
    /// Specifies the inclusive lower bound for generating random values in a multiplication problem.
    /// </summary>
    protected override int LOWER_BOUND_INCLUSIVE { get; } = 1;

    /// <summary>
    /// Specifies the exclusive upper bound for generating random values in a multiplication problem.
    /// </summary>
    protected override int UPPER_BOUND_EXCLUSIVE { get; } = 13;

    /// <summary>
    /// Calculates and sets the correct answer for the multiplication problem by multiplying the first and second numbers.
    /// </summary>
    protected override void SetAnswer() {
        answer = FirstNumber * SecondNumber;
    }

    /// <summary>
    /// Generates a fake answer for a multiplication problem, ensuring it differs from the correct answer.
    /// </summary>
    /// <returns>An integer representing an incorrect answer for the multiplication problem.</returns>
    protected override int GenerateFakeAnswer() {
        return GetRandomMathProblemValue() * GetRandomMathProblemValue();
    }

    /// <summary>
    /// Converts the multiplication problem into its string representation in the format "FirstNumber x SecondNumber".
    /// </summary>
    /// <returns>The string representation of the multiplication problem.</returns>
    public override string ToString() {
        return FirstNumber + " x " + SecondNumber;
    }
}