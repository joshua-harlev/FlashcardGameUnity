using System;
using System.Collections.Generic;

public class StateMachine {
    public IGameState CurrentState { get; private set; }
    public readonly MainMenuState MainMenuState;
    private Stack<IGameState> gameStateHistory = new Stack<IGameState>();
    private bool trackHistory = true;

    public StateMachine() {
        MainMenuState = new MainMenuState();
    }

    public void Initialize(IGameState initialState) {
        if (initialState == null) {
            throw new ArgumentNullException(nameof(initialState));
        }
        CurrentState = initialState;
        initialState.Enter();
        DebugLogger.Log(LogChannel.Systems, $"State machine initialized to {initialState.StateName}");
    }

    public void TransitionTo(IGameState newState) {
        if (newState == null) {
            throw new ArgumentNullException(nameof(newState));
        }
        DebugLogger.Log(LogChannel.Systems, $"Transitioning to {newState.StateName} from {CurrentState.StateName}");
        if(trackHistory) gameStateHistory.Push(CurrentState);
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public bool TransitionToPrevious() {
        if (gameStateHistory.Count == 0) {
            return false;
        }

        IGameState previousState = gameStateHistory.Pop();
        trackHistory = false;
        TransitionTo(previousState);
        trackHistory = true;
        
        return true;
    }

    public void ClearHistory() {
        gameStateHistory.Clear();
    }
}