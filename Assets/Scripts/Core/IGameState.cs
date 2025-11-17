public interface IGameState {
    public string StateName { get; }
    public void Enter();
    public void Exit();
}