    /**
     * By: J. Harlev
     * Course Info: GAME 245-02
     * For: PA1
     * Overview:
     * MathProblem.cs | Models a math problem.
     */

    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents an abstract class for modeling a math problem.
    /// </summary>
    public abstract class MathProblem {
        /// <summary>
        /// The first number of the math problem.
        /// </summary>
        protected int FirstNumber { get; private set; }

        /// <summary>
        /// The second number of the math problem.
        /// </summary>
        protected int SecondNumber { get; private set; }

        /// <summary>
        /// The computed or expected answer to the math problem.
        /// </summary>
        protected int answer { get; set; }

        /// <summary>
        /// The inclusive lower bound for generating random math problem values.
        /// </summary>
        protected abstract int LOWER_BOUND_INCLUSIVE { get;  }

        /// <summary>
        /// The exclusive upper bound for generating random values in math problems.
        /// </summary>
        protected abstract int UPPER_BOUND_EXCLUSIVE { get; }

        /// <summary>
        /// Constructor. Creates a math problem from two numbers.
        /// </summary>
        protected MathProblem(int firstNumber, int secondNumber) {
            this.FirstNumber = firstNumber;
            this.SecondNumber = secondNumber;
            SetAnswer();
        }

        /// <summary>
        /// Generates a random integer within the predefined bounds for math problems.
        /// </summary>
        /// <returns>A random integer between the inclusive lower bound and the exclusive upper bound.</returns>
        protected int GetRandomMathProblemValue() {
            return Random.Range(LOWER_BOUND_INCLUSIVE, UPPER_BOUND_EXCLUSIVE);
        }

        /// <summary>
        /// Sets the correct answer for the math problem based on the specific implementation logic.
        /// </summary>
        protected abstract void SetAnswer();

        /// <summary>
        /// Generates a fake answer for the math problem based on implementation logic.
        /// </summary>
        /// <returns>An incorrect answer for the math problem.</returns>
        protected abstract int GenerateFakeAnswer();

        /// <summary>
        /// Returns a string that represents the current math problem for display.
        /// </summary>
        /// <returns>A string representing the math problem.</returns>
        public abstract override string ToString();


        /// <summary>
        /// Constructor. Creates a completely random problem.
        /// </summary>
        protected MathProblem() {
            this.FirstNumber = GetRandomMathProblemValue();
            this.SecondNumber = GetRandomMathProblemValue();
            if (SecondNumber > FirstNumber) { 
                (FirstNumber, SecondNumber) = (SecondNumber, FirstNumber);
            }
            SetAnswer();
        }

        /// <summary>
        /// Retrieves a fake answer for the math problem that is distinct from the correct answer.
        /// </summary>
        /// <returns>A fake answer differing from the correct answer.</returns>
        private int GetFakeAnswer() {
            int fakeAnswer;
            do {
                fakeAnswer = GenerateFakeAnswer();
            } while (fakeAnswer == this.answer);
            return fakeAnswer;
        }

        /// <summary>
        /// Constructs a list of possible answers for the math problem, including the correct answer and fake answers.
        /// </summary>
        /// <returns>A shuffled list of integers containing the correct answer and two fake answers.</returns>
        public List<int> GetAnswerList() {
            List<int> answers = new List<int>();
            answers.Add(GetFakeAnswer());
            answers.Add(GetFakeAnswer());
            answers.Add(answer);
            ListTools.ShuffleList(ref answers);
            return answers;
        }

        /// <summary>
        /// Determines if the provided answer is the correct answer to the math problem.
        /// </summary>
        /// <param name="answer">The answer to be validated.</param>
        /// <returns>True if the provided answer is correct, otherwise false.</returns>
        public bool IsCorrect(int answer) {
            return answer == this.answer;
        }

        protected void SetNumbers(int firstNumber, int secondNumber) {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }
    }
