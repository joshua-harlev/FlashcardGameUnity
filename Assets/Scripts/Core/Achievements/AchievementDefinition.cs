/**
 * By: J. Harlev
 * Course Info: GAME 245-02
 * Overview:
 * AchievementDefinition.cs | Achievement Definition ScriptableObject
 */
using UnityEngine;

/// <summary>
/// Represents the definition of an achievement within the game.
/// This includes properties such as the unique identifier (`Id`), display name (`Name`),
/// and a description (`Description`) that provides details about the achievement.
/// </summary>
[CreateAssetMenu(fileName = "AchievementDefinition", menuName = "Scriptable Objects/AchievementDefinition")]
public class AchievementDefinition : ScriptableObject {
    public string Id;
    public string Name;
    public string Description;
}
