/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * Command.cs | Abstract Command Class
 */

/// <summary>
/// Represents an abstract base class for the Command pattern.
/// </summary>
public abstract class Command {
    public abstract void Execute();
    public abstract string GetDetails();
}