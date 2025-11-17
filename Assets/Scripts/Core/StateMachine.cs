/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * StateMachine.cs | Manages overall scene transitions and tracking.
 */
using System;
using System.Collections.Generic;

public class StateMachine {
    public IGameState CurrentState { get; private set; }
    public readonly MainMenuState MainMenuState = new();
    private readonly Stack<IGameState> gameStateHistory = new Stack<IGameState>();

    public void Initialize(IGameState initialState) {
        if (initialState == null) {
            throw new ArgumentNullException(nameof(initialState));
        }
        CurrentState = initialState;
        initialState.Enter();
        DebugLogger.Log(LogChannel.Systems, $"State machine initialized to {initialState.StateName}");
    }

    public void TransitionTo(IGameState newState, bool shouldPushToHistory = true) {
        if (newState == null) {
            throw new ArgumentNullException(nameof(newState));
        }
        DebugLogger.Log(LogChannel.Systems, $"Transitioning to {newState.StateName} from {CurrentState.StateName}");
        if(shouldPushToHistory) gameStateHistory.Push(CurrentState);
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public bool TransitionToPrevious() {
        if (gameStateHistory.Count == 0) {
            return false;
        }

        IGameState previousState = gameStateHistory.Pop();
        TransitionTo(previousState, false);
        
        return true;
    }

    public void ClearHistory() {
        gameStateHistory.Clear();
    }
}