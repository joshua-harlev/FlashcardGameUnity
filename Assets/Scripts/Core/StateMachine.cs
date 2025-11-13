using System;

public class StateMachine {
    public IGameState CurrentState { get; private set; }
    
    public readonly MainMenuState MainMenuState;

    public StateMachine() {
        MainMenuState = new MainMenuState();
    }

    public void Initialize(IGameState initialState) {
        CurrentState = initialState;
        if (initialState == null) {
            throw new ArgumentNullException(nameof(initialState));
        }
        initialState.Enter();
    }

    public void TransitionTo(IGameState newState) {
        if (newState == null) {
            throw new ArgumentNullException(nameof(newState));
        }
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}