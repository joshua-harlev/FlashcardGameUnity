/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * IGameState.cs | Game state interface.
 */
public interface IGameState {
    public string StateName { get; }
    public void Enter();
    public void Exit();
}