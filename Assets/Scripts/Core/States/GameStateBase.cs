public abstract class GameStateBase : IGameState {
    public abstract string StateName { get; }
    public virtual void Enter() {
        DebugLogger.Log(LogChannel.Systems, $"Entering {StateName} state");
    }

    public virtual void Exit() {
        DebugLogger.Log(LogChannel.Systems, $"Exiting {StateName} state");
    }
}