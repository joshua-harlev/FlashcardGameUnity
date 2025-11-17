/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * PlayingState.cs | Manages round / gameplay logic.
 */

using UnityEngine.SceneManagement;

public class PlayingState : GameStateBase {
    public override string StateName => "Gameplay";
    
    private readonly MathProblemType problemType;
    private GameSession session;
    
    public PlayingState(MathProblemType problemType) {
        this.problemType = problemType;
    }
    
    public override void Enter() {
        base.Enter();
        GameActions.OnGameSceneLoad += OnSceneLoaded;
        GameActions.OnAnswerSelected += OnAnswerSelected;
        GameActions.OnCountdownComplete += OnCountdownComplete;
        GameActions.OnRoundTimerEnd += OnRoundTimerEnd;
        GameActions.OnRoundTimerTick += UpdateTimeRemaining;
        SceneManager.LoadScene(SceneNames.FlashcardGame);
    }

    private void OnRoundTimerEnd() {
        session.RegisterAnswer(false);
        GameActions.OnRoundEnd?.Invoke();
        GameActions.OnStateDataUpdate?.Invoke(session);
        if (session.GameIsOver) {
            EndGame();
        }
        else {
            PlayRound();
        }
    }

    private void OnCountdownComplete() {
        PlayRound();
    }

    private void OnAnswerSelected(bool isCorrect) {
        session.RegisterAnswer(isCorrect);
        GameActions.OnRoundEnd?.Invoke();
        GameActions.OnStateDataUpdate?.Invoke(session);
        
        if (session.GameIsOver) {
            EndGame();
        }
        else {
            PlayRound();
        }
    }

    private void EndGame() {
        AchievementEvents.OnGameComplete?.Invoke(session.CorrectAnswerCount, Game.Instance.SelectedProblemType, Game.Instance.SelectedNumberOfQuestions);
        GameActions.OnStateDataUpdate?.Invoke(session);
        Game.Instance.StateMachine.TransitionTo(new ResultsState());
    }

    private void OnSceneLoaded() {
        Game.Instance.StartNewSession();
        session = Game.Instance.GameSession;
    }

    private void PlayRound() {
        if (session.CurrentRoundNumber == 0) {
            session.AchievementsUnlockedThisRound.Clear();
            AchievementEvents.OnGameStart?.Invoke();
        }
        session.AdvanceRound();
        GameActions.OnStateDataUpdate?.Invoke(session);
        
        GenerateNewProblem();
        GameActions.OnRoundStart?.Invoke();
    }
    
    private void GenerateNewProblem() {
        GenerateQuestionCommand generateQuestionCommand = new GenerateQuestionCommand(problemType);
        generateQuestionCommand.Execute();
        MathProblem problem = generateQuestionCommand.Problem;
        GameActions.OnProblemGenerated?.Invoke(problem);
    }
    
    private void UpdateTimeRemaining(int timeRemaining) {
        session.CurrentTimeRemaining = timeRemaining;
    }

    public override void Exit() {
        base.Exit();

        GameActions.OnGameSceneLoad -= OnSceneLoaded;
        GameActions.OnAnswerSelected -= OnAnswerSelected;
        GameActions.OnCountdownComplete -= OnCountdownComplete;
        GameActions.OnRoundTimerEnd -= OnRoundTimerEnd;
        GameActions.OnRoundTimerTick -= UpdateTimeRemaining;
    }
}